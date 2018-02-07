using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public PlayerController thePlayer;
	public float respawnDelay;
	public GameObject deathSign;
	public int orbCount;
	public Text textOrbs;
	public Image heart1;
	public Image heart2;
	public Image heart3;
	public Sprite fullHearts;
	public Sprite halfHearts;
	public Sprite emptyHearts;
	public int playersMaxHealth;
	public int healthCount;
	private bool respawning; 
	public ResetObjects[] theResetObjects;
	public bool resetObjects;
	public bool playerInvincible; 
	public AudioSource orbSound;
	public AudioSource gettingHitSound;
	public AudioSource deathSound;
	public int startingLives;
	public int updatingLives;
	public Text livesText;
	public GameObject gameOverScreen;
	public AudioSource levelMusic;






	void Start () {
		updatingLives = startingLives;
		livesText.text = "X " + updatingLives;
		thePlayer = FindObjectOfType<PlayerController>();
		textOrbs.text ="Orbs: " + orbCount;
		healthCount = playersMaxHealth;
		theResetObjects = FindObjectsOfType<ResetObjects> ();
		gameOverScreen.SetActive (false);

	}

	void Update () {
		if (healthCount <= 0 && !respawning) 
		{
			Respawn ();
			respawning = true;
		}
			
		
		
	}
		
	public void Respawn()
	{
		updatingLives -= 1;
		livesText.text = "X " + updatingLives;
		 //To immidiately reflect that the player has lost one life
		if (updatingLives > 0) {
			StartCoroutine ("RespawnCo");
			thePlayer.knockBackCounter = 0;
		} else {
			deathSound.Play ();
			thePlayer.gameObject.SetActive (false);
			Instantiate (deathSign, thePlayer.transform.position, thePlayer.transform.rotation);
			levelMusic.Stop ();
			gameOverScreen.SetActive (true);
		}
	}

	public IEnumerator RespawnCo()
	{
		deathSound.Play ();
		thePlayer.gameObject.SetActive (false);
		Instantiate (deathSign, thePlayer.transform.position, thePlayer.transform.rotation);
		yield return new WaitForSeconds (respawnDelay);

		healthCount = playersMaxHealth;
		respawning = false;
		UpdateheartMeter ();
		orbCount = 0;
		textOrbs.text ="Orbs: " + orbCount;
		thePlayer.transform.position = thePlayer.respawnPoint;
		thePlayer.gameObject.SetActive (true); 



			for (int i = 0; i < theResetObjects.Length; i++) {
				resetObjects = true;
				theResetObjects [i].gameObject.SetActive (true); 
				theResetObjects [i].objectResetting (); 
			}
			

	}

	public void AddOrbs (int orbsToAdd)
	{
		orbCount += orbsToAdd;
		textOrbs.text ="Orbs: " + orbCount;
		orbSound.Play ();
	}

	public void Damage (int damageToTake)
	{
		if (!playerInvincible) {

			healthCount -= damageToTake;
			UpdateheartMeter ();
			gettingHitSound.Play ();
			thePlayer.knockBack ();
		}
	}

	public void UpdateheartMeter()
	{
		switch (healthCount) 
		{
		case 6:
			heart1.sprite = fullHearts;
			heart2.sprite = fullHearts;
			heart3.sprite = fullHearts;
			return;
		case 5: 
			heart1.sprite = fullHearts;
			heart2.sprite = fullHearts;
			heart3.sprite = halfHearts;
			return;
		case 4: 
			heart1.sprite = fullHearts;
			heart2.sprite = fullHearts;
			heart3.sprite = emptyHearts;
			return;
		case 3: 
			heart1.sprite = fullHearts;
			heart2.sprite = halfHearts;
			heart3.sprite = emptyHearts;
			return;
		case 2: 
			heart1.sprite = fullHearts;
			heart2.sprite = emptyHearts;
			heart3.sprite = emptyHearts;
			return;
		case 1: 
			heart1.sprite = halfHearts;
			heart2.sprite = emptyHearts;
			heart3.sprite = emptyHearts;
			return;
		default:
			heart1.sprite = emptyHearts;
			heart2.sprite = emptyHearts;
			heart3.sprite = emptyHearts;
			return;
		}
	}







		

}
