using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public BoxCollider2D attackBox;
	public bool attackBD;
	public GameObject deathSign;
	public AudioSource swordSound;
	public AudioSource enemyDeathSound;


	//private Animator attackAnim;


	void Start () {
		 
		attackBox = GetComponent<BoxCollider2D> ();


		attackBox.enabled = false;
		
		
	}
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.JoystickButton0) && !attackBD) {
			attackBD = true;
			attackBox.enabled = true;
			swordSound.Play (); 

		} else {
			attackBD = false;
			attackBox.enabled = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Enemy") {
			enemyDeathSound.Play ();
			other.gameObject.SetActive (false);
			Instantiate (deathSign, other.gameObject.transform.position, other.gameObject.transform.rotation);
		}
	}

}
