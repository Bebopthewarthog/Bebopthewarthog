using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

	public CameraMovement theCamera;



	void Start (){

		theCamera = FindObjectOfType<CameraMovement> ();

		


	}
	

	void Update () {
		
	}



void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			theCamera.invisibleObject = transform.position;

			theCamera.cameraFixation = true;
		}

		//if (other.tag != "Player") {
			//theCamera.transform.parent = null;
		//}

	} 



} 
