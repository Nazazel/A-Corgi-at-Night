using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public GameObject teleportExit;
	private bool teleportable;
	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R) && !player.GetComponent<Doggo> ().jumping && !player.GetComponent<Doggo> ().barking && !player.GetComponent<Doggo> ().crouching && teleportable) {
			player.transform.position = new Vector3(teleportExit.transform.position.x, teleportExit.transform.position.y+0.1f, teleportExit.transform.position.z);
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
