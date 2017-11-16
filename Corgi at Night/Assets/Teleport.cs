using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public GameObject teleportExit;
	private bool teleportable;
	private Doggo player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1").GetComponent<Doggo> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R) && !player.jumping && !player.barking && !player.crouching) {
			
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			teleportable = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			teleportable = false;
		}
	}
}
