using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {
	
	//Movement
	private bool canMove;
	private Rigidbody2D rb;
	private float runSpeed;
	private float speed;
	private float crouchSpeed;
	private float jumpHeight;
	private bool running;
	private bool crouching;
	private bool jumping;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (GameObject.Find("Corgo"));
		rb = gameObject.GetComponent<Rigidbody2D> ();
		canMove = true;
		crouching = false;
		running = false;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (Input.GetKey (KeyCode.Space) && !crouching) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
			}
			if (Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.LeftShift)) {
				crouching = true;
			} else if (Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.DownArrow)) {
				running = true;
			}
			if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
				if ((rb.velocity.x > 0.0f) && (rb.velocity.y != 0.0f)) {
					rb.velocity = new Vector2 (0.0f, 0.0f);
				}

				if (running) {
					rb.velocity = new Vector2 (-1.0f * runSpeed, 0.0f);
				} else if (crouching) {
					rb.velocity = new Vector2 (-1.0f * crouchSpeed, 0.0f);
				}
				if (!running && !crouching) {
					rb.velocity = new Vector2 (-1.0f * speed, 0.0f);
				}
			} else if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
				if ((rb.velocity.x < 0.0f) && (rb.velocity.y != 0.0f)) {
					rb.velocity = new Vector2 (0.0f, 0.0f);

				}

				if (running) {
					rb.velocity = new Vector2 (runSpeed, 0.0f);
				} else if (crouching) {
					rb.velocity = new Vector2 (crouchSpeed, 0.0f);
				} else if (!running && !crouching) {
					rb.velocity = new Vector2 (speed, 0.0f);
				}
			} else {
				rb.velocity = new Vector2 (0.0f, 0.0f);
			}
		}
	}
}


