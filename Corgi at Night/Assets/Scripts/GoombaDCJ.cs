using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaDCJ : MonoBehaviour {

	private Rigidbody2D DCJ;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		DCJ = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if ((player.transform.position.x < gameObject.transform.position.x)) {
			DCJ.velocity = new Vector2 (-0.75f, DCJ.velocity.y);
		} else if ((player.transform.position.x > gameObject.transform.position.x)) {
			DCJ.velocity = new Vector2 (0.75f, DCJ.velocity.y);
		} 
	}
}
