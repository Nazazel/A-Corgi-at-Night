using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGS : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			Debug.Log ("???");
			player.GetComponent<Doggo>().hideable = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			player.GetComponent<Doggo>().hideable = false;
		}
	}
}
