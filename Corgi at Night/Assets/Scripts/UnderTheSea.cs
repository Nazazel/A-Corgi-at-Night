using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTheSea : MonoBehaviour {

	public GameObject bgc;

	// Use this for initialization
	void Start () {
		bgc = GameObject.Find ("BGController");
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			bgc.GetComponent<BGController> ().carnivalFinish ();
			Destroy (gameObject);
		}
	}

}
