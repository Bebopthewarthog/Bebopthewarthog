using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public float speed;
	public GameObject movingPlatforms;
	private Vector3 targetPoint;


	void Start () {
		targetPoint = endPoint.position; 

		
	}
	

	void Update () {
		
		movingPlatforms.transform.position = Vector3.MoveTowards (movingPlatforms.transform.position, targetPoint, speed * Time.deltaTime);

		if (movingPlatforms.transform.position == endPoint.position) {
			targetPoint = startPoint.position;
			movingPlatforms.transform.position = Vector3.MoveTowards (movingPlatforms.transform.position, targetPoint, speed * Time.deltaTime);

		}

		if (movingPlatforms.transform.position == startPoint.position) {
			targetPoint = endPoint.position;
			movingPlatforms.transform.position = Vector3.MoveTowards (movingPlatforms.transform.position, targetPoint, speed * Time.deltaTime);

		}
			
			
			
	}
}
