using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLight : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") && player.GetComponent<Pibble>().hidden == false) {
			player.GetComponent<Pibble>().Caught();
		}
	}
}
