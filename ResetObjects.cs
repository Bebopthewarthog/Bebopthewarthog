using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour {

	Vector3 objectPosition;
	Quaternion objectRotation;
	Vector3 objectScale;

	Rigidbody2D objectrb2d;

	// Use this for initialization
	void Start () {

		objectPosition = transform.position;
		objectRotation = transform.rotation;
		objectScale = transform.localScale;

		if (GetComponent<Rigidbody2D> () != null) {
			objectrb2d = GetComponent<Rigidbody2D> ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void objectResetting (){
		transform.position = objectPosition;
		transform.rotation = objectRotation;
		transform.localScale = objectScale;
		if (objectrb2d != null) {
			objectrb2d.velocity = Vector3.zero;
		}
	}
	
}
