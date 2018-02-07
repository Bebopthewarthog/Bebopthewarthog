using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjects : MonoBehaviour {

	public Transform positionA;
	public Transform positionB;
	public bool grabbed;
	RaycastHit2D rightDetector;
	RaycastHit2D leftDetector;
	public float distance;
	Rigidbody2D rb2d;
	PlayerController thePlayer;



	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		thePlayer = FindObjectOfType<PlayerController> ();
		rb2d.IsSleeping();
		rb2d.isKinematic = true;
		
	}


	void Update ()
	{
		if (Input.GetKey (KeyCode.JoystickButton3)) 
		{
			if (!grabbed) 
			{
				Physics2D.queriesStartInColliders = false; 
				rightDetector = Physics2D.Raycast (transform.position, Vector2.right, distance); 
				leftDetector = Physics2D.Raycast (transform.position, Vector2.left, distance);
				if (rightDetector.collider != null && rightDetector.collider.tag == "Player")
				{
					rb2d.IsAwake ();
					grabbed = true;
				} else if (leftDetector.collider != null && leftDetector.collider.tag == "Player")
				{
					rb2d.IsAwake ();
					grabbed = true;

				}
			}

		} else 
		{
			grabbed = false;
			rb2d.isKinematic = false;

		}

		if (grabbed) 
		{
			thePlayer.smallJumpSound.Stop (); 
			transform.position = positionA.position;
			StartCoroutine ("pickUpDelayCo");
			transform.position = positionB.position;
			rb2d.isKinematic = true;
		}
			
	}





	public IEnumerator pickUpDelayCo ()
	{
		yield return new WaitForSeconds (0.1f);
	}


	void OnDrawGizmos ()

	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
		Gizmos.DrawLine (transform.position, transform.position + Vector3.left * transform.localScale.x * distance);
	}
}
