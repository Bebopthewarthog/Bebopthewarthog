using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour {

	public int orbsValue;
	private LevelManager theLevelManager;
	void Start () {

		theLevelManager = FindObjectOfType <LevelManager> ();
		
	}
	

	void Update () {


		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			theLevelManager.AddOrbs (orbsValue); 
			gameObject.SetActive (false);
		}

	}
}
