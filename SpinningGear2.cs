using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGear2 : MonoBehaviour {

	public GameObject giantSpinningGears2;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		giantSpinningGears2.transform.Rotate (0, 0, rotationSpeed);
		
	}
}
