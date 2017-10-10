using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {

	//General
	private bool idling;
	private bool idlingtimerstarted;
	public bool barking;
	public bool dead;
	public bool hidden;
	public bool hideable;
	private GameObject attackBox;
	public Vector3 spawnpoint;

	//Idle Animation List
	public string[] lookingAnim;
	public string[] sniffingAnim;

	//Camera
	public GameObject mainCam;
	
	//Movement
	private bool canMove;
	private Rigidbody2D rb;
	private float runSpeed;
	private float speed;
	private float crouchSpeed;
	private float jumpHeight;
	public bool running;
	public bool crouching;
	public bool jumping;
	private bool right;
	private bool landing;

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
		lookingAnim = new string[2];
		lookingAnim [0] = "LookSR";
		lookingAnim [1] = "LookR";
		sniffingAnim = new string[2];
		sniffingAnim [0] = "SniffStand";
		sniffingAnim [1] = "SniffWalk";
		speed = 2.0f;
		runSpeed = 4.0f;
		crouchSpeed = 1.0f;
		jumpHeight = 2.0f;
		canMove = true;
		crouching = false;
		barking = false;
		running = false;
		jumping = false;
		landing = false;
		idling = false;
		idlingtimerstarted = false;
		dead = false;
		hidden = false;
		right = true;

	}

	// Update is called once per frame
	void Update () {
		if (!dead && !landing && jumping) {
			if (rb.velocity.y < 0) {
				Debug.Log ("f");
				landing = true;
				pa.Play ("Land");
			}
		}
		if (!dead) {
			if (Input.GetKeyDown (KeyCode.H) && !crouching && !jumping && !hidden && hideable) {
				Debug.Log ("Hidden");
				sr.sortingOrder = 28;
				idling = false;
				StopCoroutine ("IdleAnimate");
				idlingtimerstarted = false;
				pa.Play ("Hide");
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				gameObject.GetComponent<PolygonCollider2D> ().enabled = !gameObject.GetComponent<PolygonCollider2D> ().enabled;
				hidden = true;
			}
			else if (Input.GetKeyDown (KeyCode.H) && !crouching && !jumping && hidden) {
				Debug.Log ("!");
				sr.sortingOrder = 30;
				gameObject.GetComponent<PolygonCollider2D> ().enabled = !gameObject.GetComponent<PolygonCollider2D> ().enabled;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
				hidden = false;
			}
		}
		if (canMove && !dead && !hidden) {
			
			if (Input.GetKey (KeyCode.Space) && !crouching && !jumping) {
				idling = false;
				StopCoroutine ("IdleAnimate");
				idlingtimerstarted = false;
				rb.velocity = new Vector2 (rb.velocity.x, jumpHeight*1.5f);
				pa.Play ("Jump");
				jumping = true;
			}

			if (Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.LeftShift)) {
				idling = false;
				StopCoroutine ("IdleAnimate");
				idlingtimerstarted = false;
				crouching = true;
				if (!Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
					pa.Play("Crouch");
				}
			} else {
				crouching = false;
			}

			if (Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.DownArrow)) {
				running = true;
			} else {
				running = false;
			}

			if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
				idling = false;
				StopCoroutine ("IdleAnimate");
				idlingtimerstarted = false;
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
					if (!jumping) {
						pa.Play ("Run");
					}
					rb.velocity = new Vector2 (-1.0f * runSpeed, rb.velocity.y);
				} else if (crouching) {
					if (!jumping) {
						pa.Play("CWalkR");
					}
					rb.velocity = new Vector2 (-1.0f * crouchSpeed, rb.velocity.y);
				} else if (!running && !crouching) {
					if (!jumping) {
						pa.Play("Walk");
					}
					rb.velocity = new Vector2 (-1.0f * speed, rb.velocity.y);
				}
			} else if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
				idling = false;
				StopCoroutine ("IdleAnimate");
				idlingtimerstarted = false;
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
					if (!jumping) {
						pa.Play ("Run");
					}
					rb.velocity = new Vector2 (runSpeed, rb.velocity.y);
				} else if (crouching) {
					if (!jumping) {
						pa.Play("CWalkR");
					}
					rb.velocity = new Vector2 (crouchSpeed, rb.velocity.y);
				} else if (!running && !crouching) {
					if (!jumping) {
						pa.Play("Walk");
					}
					rb.velocity = new Vector2 (speed, rb.velocity.y);
				}
			} else if (!jumping && !barking && !crouching) {
				rb.velocity = new Vector2 (0.0f, rb.velocity.y);
				idling = true;
			}

			if (idling && !hidden && !idlingtimerstarted) {
				idlingtimerstarted = true;
				StartCoroutine ("IdleAnimate");
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Floor") && jumping == true) {
			jumping = false;
			landing = false;
			pa.Play ("Stand");
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

//  CODE FOR FALLING OFF PLATFORMS: DOESN'T WORK RIGHT NOW, BUT POSSIBLE
	void OnCollisionExit2D(Collision2D colll)
	{
		if (colll.gameObject.CompareTag ("Floor") && !jumping) {
			Debug.Log ("Fall?");
			landing = true;
			jumping = true;
			pa.Play ("Land");
		}
	}

	public IEnumerator Death()
	{
		pa.Play("DieR");
		StopCoroutine ("IdleAnimate");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = spawnpoint;
		dead = false;
		landing = false;
	}

	public void StompBoop()
	{
		rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		pa.Play ("Jump");
	}

	public void Caught()
	{
		dead = true;
		StartCoroutine("Death");
	}

	public void Bark()
	{
		StartCoroutine ("Borking");
	}

	public IEnumerator Borking()
	{
		idling = false;
		StopCoroutine ("IdleAnimate");
		idlingtimerstarted = false;
		barking = true;
		pa.Play ("BorkR");
		yield return new WaitForSeconds (0.15f);
		barking = false;
	}

	public IEnumerator IdleAnimate()
	{
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play (lookingAnim [Random.Range (0, 2)]);
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play ("SniffStand");
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play (lookingAnim [Random.Range (0, 2)]);
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play ("SitR");
	}
}


