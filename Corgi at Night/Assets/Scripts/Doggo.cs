﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {

	//General
	public bool dead;
	public bool hidden;
	public bool hideable;
	private GameObject attackBox;
	public Vector3 spawnpoint;

	//Camera
	public GameObject mainCam;
	
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
	private bool right;

	//Sprites
	private SpriteRenderer sr;
	private Animator pa;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (GameObject.Find("Corgo"));
		mainCam = GameObject.Find ("Main Camera");
		attackBox = GameObject.Find ("AttackBox");
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		pa = gameObject.GetComponent<Animator> ();
		speed = 2.0f;
		runSpeed = 4.0f;
		crouchSpeed = 1.0f;
		jumpHeight = 2.0f;
		canMove = true;
		crouching = false;
		running = false;
		jumping = false;
		dead = false;
		hidden = false;
		right = true;
	}

	// Update is called once per frame
	void Update () {
		if (!dead) {
			if (Input.GetKeyDown (KeyCode.H) && !crouching && !jumping && !hidden && hideable) {
				Debug.Log ("Hidden");
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				gameObject.GetComponent<PolygonCollider2D> ().enabled = !gameObject.GetComponent<PolygonCollider2D> ().enabled;
				hidden = true;
			}
			else if (Input.GetKeyDown (KeyCode.H) && !crouching && !jumping && hidden) {
				Debug.Log ("!");
				gameObject.GetComponent<PolygonCollider2D> ().enabled = !gameObject.GetComponent<PolygonCollider2D> ().enabled;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
				hidden = false;
			}
		}
		if (canMove && !dead && !hidden) {
			
			if (Input.GetKey (KeyCode.Space) && !crouching && !jumping) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpHeight*1.5f);
				jumping = true;
			}

			if (Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.LeftShift)) {
				crouching = true;
			} else {
				crouching = false;
			}

			if (Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.DownArrow)) {
				running = true;
			} else {
				running = false;
			}

			if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
				attackBox.transform.localPosition = new Vector3 (0.0f, 0.007f, attackBox.transform.position.z);
				if (gameObject.transform.rotation.y == 0) {
					mainCam.transform.localPosition = new Vector3 (0.0f, 0.26f, 10.0f);
					mainCam.transform.Rotate (Vector3.up, 180.0f);
					gameObject.transform.Rotate (Vector3.up, 180.0f);
				}
				if ((rb.velocity.x > 0.0f) && (rb.velocity.y != 0.0f)) {
					rb.velocity = new Vector2 (0.0f, rb.velocity.y);
				}

				if (running) {
					pa.Play("Run");
					rb.velocity = new Vector2 (-1.0f * runSpeed, rb.velocity.y);
				} else if (crouching) {
					rb.velocity = new Vector2 (-1.0f * crouchSpeed, rb.velocity.y);
				} else if (!running && !crouching) {
					if (!jumping) {
						pa.Play("Walk");
					}
					rb.velocity = new Vector2 (-1.0f * speed, rb.velocity.y);
				}
			} else if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
				attackBox.transform.localPosition = new Vector3 (0.984f, 0.007f, attackBox.transform.position.z);
				if (gameObject.transform.rotation.y != 0) {
					mainCam.transform.localPosition = new Vector3 (0.0f, 0.26f, -10.0f);
					mainCam.transform.Rotate (Vector3.up, 180.0f);
					gameObject.transform.Rotate (Vector3.up, 180.0f);
				}
				if ((rb.velocity.x < 0.0f) && (rb.velocity.y != 0.0f)) {
					rb.velocity = new Vector2 (0.0f, rb.velocity.y);

				}

				if (running) {
					pa.Play("Run");
					rb.velocity = new Vector2 (runSpeed, rb.velocity.y);
				} else if (crouching) {
					rb.velocity = new Vector2 (crouchSpeed, rb.velocity.y);
				} else if (!running && !crouching) {
					if (!jumping) {
						pa.Play("Walk");
					}
					rb.velocity = new Vector2 (speed, rb.velocity.y);
				}
			} else if (!jumping) {
				pa.Play("Stand");
				rb.velocity = new Vector2 (0.0f, rb.velocity.y);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Floor") && jumping == true) {
			jumping = false;
		}
		else if (coll.gameObject.CompareTag ("Death") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
		else if (coll.gameObject.CompareTag ("NinjaCat") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
		else if (coll.gameObject.CompareTag ("DogCatcherJR") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
	}

	public IEnumerator Death()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = spawnpoint;
		dead = false;
	}

	public void StompBoop()
	{
		rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
	}

	public void Caught()
	{
		dead = true;
		StartCoroutine("Death");
	}
}


