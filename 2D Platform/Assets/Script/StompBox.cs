using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour {
	public GameObject deathSign;
	public BoxCollider2D stompBox;
	private PlayerController myPC;
	public float bounce;
	public AudioSource stompSound;

	//--------

	public Enemies theEnemy;


	void Start () {
		stompBox = GetComponent<BoxCollider2D> ();
		stompBox.enabled = false;
		myPC = FindObjectOfType <PlayerController> ();
		theEnemy = FindObjectOfType <Enemies> ();

		
	}
	

	void Update () {

		if (myPC.playerRigidBody.velocity.y < 0 && !myPC.onTheGround)
			stompBox.enabled = true;
		else
			stompBox.enabled = false;



		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") {
			stompSound.Play ();
			//theEnemy.enemyAnim.SetBool ("Death", theEnemy.deathAnim);
			//other.gameObject.SetActive (false);

			//Instantiate (deathSign, other.gameObject.transform.position, other.gameObject.transform.rotation);
			myPC.playerRigidBody.velocity = new Vector3 (myPC.playerRigidBody.velocity.x, bounce, 0f);
			if (Input.GetKey (KeyCode.JoystickButton1))
				myPC.jumpHeight = myPC.longerJumps;
			else
				myPC.jumpHeight = myPC.normalJumps;
		}
	}
}
