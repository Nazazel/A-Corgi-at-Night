using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCatto : MonoBehaviour {

	private Rigidbody2D NinCat;
	private GameObject player;

	private float jumpHeight;
	private bool jumping;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		NinCat = gameObject.GetComponent<Rigidbody2D> ();
		jumpHeight = 2.0f;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!jumping) {
			NinCat.velocity = new Vector2 (NinCat.velocity.x, jumpHeight*1.5f);
			jumping = true;
		}
		if ((player.transform.position.x < gameObject.transform.position.x)) {
			NinCat.velocity = new Vector2 (-1.0f, NinCat.velocity.y);
		} 
		else if ((player.transform.position.x > gameObject.transform.position.x)) {
			NinCat.velocity = new Vector2 (1.0f, NinCat.velocity.y);
		} 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Floor") && jumping == true) {
			jumping = false;
		}
	}
}
