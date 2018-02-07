using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour {

	public float slidingSpeed;
	public bool wallSliding;
	public float wallJumpForce;
	Rigidbody2D playerRigidBody;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	public bool onTheWall;

	PlayerController myPC;
	PlayerAttack thePlayerAttack;







	void Start ()
	{
		playerRigidBody = GetComponent <Rigidbody2D> (); 
		myPC = GetComponent <PlayerController> (); 
		thePlayerAttack = FindObjectOfType<PlayerAttack> (); 
		
	}



	void Update ()
	{
		onTheWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);


			

		if (Input.GetAxisRaw ("Horizontal") > 0f && onTheWall) 
		{
			
			wallSliding = true;
			if (wallSliding) {
				myPC.enabled = false;
				thePlayerAttack.enabled = false;
				playerRigidBody.velocity = Vector2.up * Physics2D.gravity.y * slidingSpeed * Time.deltaTime;
			}
		} else if (Input.GetAxisRaw ("Horizontal") < 0f && onTheWall) 
		{
			
			wallSliding = true;
			if (wallSliding) {
				myPC.enabled = false;
				thePlayerAttack.enabled = false;
				playerRigidBody.velocity = Vector2.up * Physics2D.gravity.y * slidingSpeed * Time.deltaTime;
			}
		} else 
		{
			myPC.enabled = true;
			thePlayerAttack.enabled = true;
			wallSliding = false;

		}

		if (onTheWall && transform.localScale.x > 0 && Input.GetButtonDown ("Jump")) {
			myPC.enabled = false;
			thePlayerAttack.enabled = false;
			myPC.smallJumpSound.Play ();

			playerRigidBody.velocity = new Vector3 (-wallJumpForce, wallJumpForce + 5, 0f);
			
		} else if (onTheWall && transform.localScale.x < 0 && Input.GetButtonDown ("Jump")) {
			myPC.enabled = false;
			thePlayerAttack.enabled = false;
			myPC.smallJumpSound.Play ();

			playerRigidBody.velocity = new Vector3 (wallJumpForce, wallJumpForce + 5, 0f);

		} else {
			myPC.enabled = true;
			thePlayerAttack.enabled = true;

		}
		
			
		

	}
		



}
