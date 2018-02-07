using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
	private LevelManager theLevelManager;
	public int damageToGive;

	public Transform StartPoint;
	public Transform EndPoint;
	public float Speed;
	public GameObject Enemy;
	Vector3 targetPoint; 
	public BoxCollider2D [] enemyBox;
	public BoxCollider2D enemyBox2;
	public bool Isdead = false;
	private PlayerController theplayer;
	Vector2 freezingPosition; 
	bool facingright;


	//--------animationVar--------

	private Animator enemyAnim; 



	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();
		targetPoint = EndPoint.position;
		enemyBox = GetComponents<BoxCollider2D> ();
		enemyAnim = GetComponent<Animator> ();
		theplayer = FindObjectOfType<PlayerController> ();

	
		
	}


	
	// Update is called once per frame
	void Update () {
		freezingPosition = new Vector2 (Enemy.transform.position.x, Enemy.transform.position.y);
		
		if (Isdead) {
			Enemy.transform.position = freezingPosition;
			foreach (BoxCollider2D bc in enemyBox)
				bc.enabled = false;
		} else {
			foreach (BoxCollider2D bc in enemyBox)
				bc.enabled = true;
			Enemy.transform.position = Vector3.MoveTowards (Enemy.transform.position, targetPoint, Speed * Time.deltaTime);
			if (Enemy.transform.position == EndPoint.position) {
				targetPoint = StartPoint.position;
				Enemy.transform.position = Vector3.MoveTowards (Enemy.transform.position, targetPoint, Speed * Time.deltaTime);
				Enemy.transform.localScale = new Vector3 (-1f, 1f, 1f);
			}

			if (Enemy.transform.position == StartPoint.position) {
				targetPoint = EndPoint.position;
				Enemy.transform.position = Vector3.MoveTowards (Enemy.transform.position, targetPoint, Speed * Time.deltaTime);
				Enemy.transform.localScale = new Vector3 (1f, 1f, 1f);
			}
		}


	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
			theLevelManager.Damage (damageToGive);

		if (other.tag == "StompBox") {
			Isdead = true;
			enemyAnim.SetBool ("Death", true);
			StartCoroutine ("explosionAnimCo");
		}
	}
		




	public IEnumerator explosionAnimCo()
	{
		yield return new WaitForSeconds (0.3f);
		gameObject.SetActive (false);
		Isdead = false;

	}
}
