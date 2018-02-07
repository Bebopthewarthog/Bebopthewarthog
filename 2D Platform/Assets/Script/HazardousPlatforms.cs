using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardousPlatforms : MonoBehaviour {

	private Rigidbody2D platformsrb2d;
	public float fall; 
	public Vector3 respawnPointPlatforms; 
	public float delayTime;


	// Use this for initialization
	void Start () {
		platformsrb2d = GetComponent<Rigidbody2D> (); 
		respawnPointPlatforms = transform.position;
		 
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}




	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "KillZone") 
		{
			gameObject.SetActive (false); 


		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			fallingObject (); 
		}
	}

	void OnBecameInvisible ()
	{
		
		//platformsrb2d.velocity = new Vector3 (platformsrb2d.velocity.x, fall, 0f);
	}

	public void fallingObject ()
	{
		StartCoroutine ("fallingDelayCo");
	}

	public IEnumerator fallingDelayCo()
	{
		
		yield return new WaitForSeconds (delayTime);
		platformsrb2d.velocity = new Vector3 (platformsrb2d.velocity.x, -fall, 0f);
	}





}
