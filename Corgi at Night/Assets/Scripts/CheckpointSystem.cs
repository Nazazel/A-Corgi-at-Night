﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			player.GetComponent<Doggo>().spawnpoint = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			Debug.Log (gameObject.name);
			Destroy (gameObject);
		}
	}
		
}
