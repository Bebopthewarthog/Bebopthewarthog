using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	//--------animationVar-------
	public bool touchGround = true;
	private bool attackBP;
	private Animator attackAnim;

	public Rigidbody2D playerRigidBody;  
	private Animator playerAnimator; 
	public HazardousPlatforms theFallingPlatforms; 
	//----groundCheckVars
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool onTheGround;

	//---doubleJump
	private bool doubleJumpAvail = false;

	//------jumpVars
	public float longerJumps;
	public float jumpHeight;
	public float normalJumps;
	private bool longerJumpAvail;
	public AudioSource smallJumpSound;
	public AudioSource superJumpSound;
	//------speed&accelerationVars
	public float currentSpeed;
	public float maxSpeed;
	public float normalSpeed;
	public float acceleration;
	public float decceleration;
	public bool walkEnabled;
	//--------respawnVars
	public Vector3 respawnPoint;
	private LevelManager theLevelManager;

	//------movingPlatformsVars
	public bool onPlatform;
	public float speedMultiplier; 

	//------knockBackVars
	public float knockBackForce;
	public float knockBackLenght;
	public float knockBackCounter;

	//------invincibilityVars
	public float invincibilityLength;
	public float invincibilityCounter;
	private Enemies myEnemy;
	private SpriteRenderer playerSprite;

	//------stompBoxVar





	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();
		playerRigidBody = GetComponent<Rigidbody2D> (); 
		playerAnimator = GetComponent<Animator> ();

		attackAnim = GetComponent<Animator> ();

		respawnPoint = transform.position; 

		theFallingPlatforms = FindObjectOfType <HazardousPlatforms> (); 
		myEnemy = FindObjectOfType <Enemies>();
		playerSprite = GetComponent<SpriteRenderer> (); 
	}


	void Update () 
	{
		
		
		onTheGround = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);



		if (Input.GetKeyDown (KeyCode.JoystickButton0) && !attackBP) 
		{
			attackBP = true;
		}else
			attackBP = false;

		if (onPlatform) 
		{
			currentSpeed *= speedMultiplier;

		} else 
		{
			fasterMovement ();
			longerJump ();

		}
		

		if (knockBackCounter <= 0) 
		{
			if (Input.GetAxisRaw ("Horizontal") > 0f) 
			{
			
				playerRigidBody.velocity = new Vector3 (currentSpeed, playerRigidBody.velocity.y, 0f);
				transform.localScale = Vector3.one;
				walkEnabled = true;


			} else if (Input.GetAxisRaw ("Horizontal") < 0f) 
			{
			
				playerRigidBody.velocity = new Vector3 (-currentSpeed, playerRigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				walkEnabled = true;

			} else 
			{
				playerRigidBody.velocity = new Vector3 (0f, playerRigidBody.velocity.y, 0f);
				walkEnabled = false;
			}

			//-----------------jump&DoubleJump-----------------
			if (Input.GetButtonDown ("Jump") && onTheGround) 
			{
				
				playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, jumpHeight, 0f);
				smallJumpSound.Play ();
				doubleJumpAvail = true;
			} else 
			{
				if (Input.GetButtonDown ("Jump")) 
				{
					if (doubleJumpAvail) 
					{
						smallJumpSound.Play ();
						playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, jumpHeight - 5, 0f);
						doubleJumpAvail = false;
					}
				}

			}
			//---------fasterMovement&longerJump--------------
			fasterMovement (); 
			longerJump ();
		}

		if (knockBackCounter > 0) 
		{
			
			knockBackCounter -= Time.deltaTime;

			if (transform.localScale.x > 0) {
				playerRigidBody.velocity = new Vector3 (-knockBackForce, playerRigidBody.velocity.y, 0f);
			} else {
				playerRigidBody.velocity = new Vector3 (knockBackForce, playerRigidBody.velocity.y, 0f);
			}
		}

		if (invincibilityCounter <= 0)
			theLevelManager.playerInvincible = false;
		if (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;
			StartCoroutine ("Flash"); 
			myEnemy.enemyBox2.enabled = false;
		} else
			myEnemy.enemyBox2.enabled = true;
			
		//-------------animation--------------------------
		playerAnimator.SetFloat ("Speed", Mathf.Abs (playerRigidBody.velocity.x));
		playerAnimator.SetBool ("Grounded", onTheGround);
		attackAnim.SetBool ("AttackStarted", attackBP);
		//playerAnimator.SetBool ("WallStuck", onTheWall);

		//-------------stompBox---------------

}


	//---------fasterMovement------

	 void fasterMovement ()
	{
		if (Input.GetKey (KeyCode.JoystickButton2) && Input.GetAxisRaw ("Horizontal") > 0f && transform.localScale.x > 0) 
		{
			currentSpeed += acceleration * Time.deltaTime; 
			currentSpeed = Mathf.Min (currentSpeed, maxSpeed);

		} else if (playerRigidBody.velocity.x == 0)
		{
			currentSpeed = normalSpeed; 
		}
		else if (Input.GetKey (KeyCode.JoystickButton2) && Input.GetAxisRaw ("Horizontal") < 0f && transform.localScale.x < 0) 
		{
			currentSpeed += acceleration * Time.deltaTime; 
			currentSpeed = Mathf.Min (currentSpeed, maxSpeed);
		} else 
		{
			if (currentSpeed > 0 * Time.deltaTime) 
			{
				currentSpeed -= decceleration * Time.deltaTime;
				currentSpeed = Mathf.Max (currentSpeed, normalSpeed);
			} 
			else
				currentSpeed = normalSpeed;
		}
	} 

	//-------longerJump---------

	void longerJump ()
	{

		if (currentSpeed == maxSpeed && Input.GetAxisRaw ("Horizontal") > 0f && !longerJumpAvail) {
			longerJumpAvail = true;
			jumpHeight = longerJumps;
			if (Input.GetButtonDown ("Jump"))
				superJumpSound.Play ();
		} else if (currentSpeed == maxSpeed && Input.GetAxisRaw ("Horizontal") < 0f && !longerJumpAvail) {
			longerJumpAvail = true;
			jumpHeight = longerJumps;
			if (Input.GetButtonDown ("Jump"))
				superJumpSound.Play ();
		} else {
			longerJumpAvail = false;
			jumpHeight = normalJumps;
			if (Input.GetButtonDown ("Jump"))
				smallJumpSound.Play ();
		}
	}

	public void knockBack()
	{
		knockBackCounter = knockBackLenght;
		invincibilityCounter = invincibilityLength;
		theLevelManager.playerInvincible = true;
	}





	void OnTriggerEnter2D(Collider2D other)
	{
		 
		if (other.tag == "KillZone") 

			theLevelManager.healthCount = 0;

		
		if (other.tag == "Checkpoint")
			
			respawnPoint = other.transform.position;				
	}
		

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "MovingPlatforms") {
			transform.parent = other.transform;
			onPlatform = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "MovingPlatforms") {
			transform.parent = null;
			onPlatform = false;
		}
	}

	public IEnumerator Flash () {
		for (int i = 0; i < 3; i++) {
			playerSprite.color = new Color (1f, 1f, 1f, 0.3f);
			yield return new WaitForSeconds (.1f);
			playerSprite.color = Color.white;
			yield return new WaitForSeconds (.1f);
		}
	}


}

