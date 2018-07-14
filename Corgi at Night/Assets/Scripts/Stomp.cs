﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("QuinSpriteFinal_1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("DogCatcherJR") && player.GetComponent<Pibble>().dead == false) {
			col.gameObject.SendMessage ("Die");
			player.GetComponent<Pibble> ().StompBoop ();
			Debug.Log ("Enemy Stomped");
		}
	}
}
