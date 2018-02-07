using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGears : MonoBehaviour {
	public GameObject giantSpinningGears;
	public float rotationSpeed;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		giantSpinningGears.transform.Rotate (0, 0, rotationSpeed);

	}
}
