using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour {

	private Doggo corgo;
	private GameObject DC;

	// Use this for initialization
	void Start () {
		corgo = GameObject.Find ("QuinSpriteFinal_1").GetComponent<Pibble> ();
		DC = GameObject.Find ("DogCatcherFirst");
	}
	
	// Update is called once per frame
	void Update () {
		if ((Mathf.Abs (corgo.gameObject.transform.position.x - DC.transform.position.x) <= 3.0) && corgo.firstPatrol == true) {
			//col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			corgo.firstPatrolCatcher ();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("NinjaCat") && corgo.firstNinCat == true) {
			//col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			corgo.firstNinCats();
		}
		else if (col.gameObject.CompareTag ("DogCatcherJR") && corgo.firstDCJ == true) {
			//col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			corgo.firstDCJs();
		}
	}
}
