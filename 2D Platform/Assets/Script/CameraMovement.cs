using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	
	public GameObject target;
	public GameObject extraBoundary;
	public float followAhead;
	public Vector3 invisibleObject;
	public float smoothingVar;
	public bool cameraFixation;


	 

	void Start () {
		
			invisibleObject = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
		
		
	}
	

	void Update () {
		invisibleObject = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);

	
			if (target.transform.localScale.x > 0 && !cameraFixation) 
				invisibleObject = new Vector3 (target.transform.position.x + followAhead, transform.position.y, transform.position.z);
				
		else if (target.transform.localScale.x < 0 && !cameraFixation)
				invisibleObject = new Vector3 (target.transform.position.x - followAhead, target.transform.position.y, transform.position.z);

			if (target.transform.localScale.x > 0 && !cameraFixation)
				invisibleObject = new Vector3 (target.transform.position.x + followAhead, transform.position.y, transform.position.z);
		
			else if (target.transform.localScale.x < 0 && !cameraFixation)
				invisibleObject = new Vector3 (target.transform.position.x - followAhead, transform.position.y, transform.position.z);

		transform.position = Vector3.Lerp (transform.position, invisibleObject, smoothingVar * Time.deltaTime); // Vector3.Lerp (the current location of the target, the location we want it to move to, how long it'll take for it to get to the new postion)


			
	}

}
