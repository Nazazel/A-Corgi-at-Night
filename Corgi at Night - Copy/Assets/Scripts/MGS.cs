using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGS : MonoBehaviour {

	private GameObject player;
	public AudioClip bushExit1;
	public AudioClip bushExit2;
	public AudioClip bushEnter;
	public AudioSource ba;
	public bool entryCalled;
	public bool exitCalled;
    public bool inBushRange;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		ba = gameObject.GetComponent<AudioSource> ();
        inBushRange = false;
        entryCalled = false;
        exitCalled = false;
	}

	void Update()
	{
		if (player.GetComponent<Pibble> ().hidden == true && entryCalled == false && inBushRange == false) {
			entryCalled = true;
			exitCalled = false;
			if (gameObject.tag == "Bush") {
				ba.clip = bushEnter;
				ba.Play ();
			}
            inBushRange = true;	
		}
		else if (player.GetComponent<Pibble> ().hidden == false && exitCalled == false && entryCalled == true && inBushRange == true) {
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
            inBushRange = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			Debug.Log ("???");
			player.GetComponent<Pibble>().hideable = true;
            inBushRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			player.GetComponent<Pibble>().hideable = false;
            inBushRange = false;
		}
	}
}
