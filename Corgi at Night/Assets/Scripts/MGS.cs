using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGS : MonoBehaviour {

	private GameObject player;
	public AudioClip bushExit1;
	public AudioClip bushExit2;
	public AudioClip bushEnter;
	public AudioSource ba;
	private bool entryCalled;
	private bool exitCalled;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		ba = gameObject.GetComponent<AudioSource> ();
	}

	void Update()
	{
		if (player.GetComponent<Doggo> ().hideable == true && player.GetComponent<Doggo> ().hidden == true && entryCalled == false) {
			entryCalled = true;
			exitCalled = false;
			if (gameObject.tag == "Bush") {
				ba.clip = bushEnter;
				ba.Play ();
			}
				
		}
		else if (player.GetComponent<Doggo> ().hideable == true && player.GetComponent<Doggo> ().hidden == false && exitCalled == false && entryCalled == true) {
			entryCalled = false;
			exitCalled = true;
			if (gameObject.tag == "Bush") {
				if (Random.Range (0, 2) == 0) {
					ba.clip = bushExit1;
					ba.Play ();
				} 
				else {
					ba.clip = bushExit2;
					ba.Play ();
				}
			}
		}
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
