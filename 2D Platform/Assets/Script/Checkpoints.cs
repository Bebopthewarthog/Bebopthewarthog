using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

	public Sprite crankPulled;
	public Sprite crankNotPulled;
	private SpriteRenderer m_spriterenderer; 
	public bool checkpointActive;


	void Start () {
		m_spriterenderer = GetComponent<SpriteRenderer>();

		
	}
	

	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			m_spriterenderer.sprite = crankPulled;
			checkpointActive = true;
		}
		
	}
}
