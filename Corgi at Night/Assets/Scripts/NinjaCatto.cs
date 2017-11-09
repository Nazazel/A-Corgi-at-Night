using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCatto : MonoBehaviour {

	private Rigidbody2D NinCat;
	private Animator ca;
	public Vector3 initSpawn;
	private GameObject player;
	public float speed;
	private PolygonCollider2D catColl;
	public AudioSource caDS;
	public AudioSource caDB;

	private float jumpHeight;
	private bool jumping;
	private bool waittime;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		initSpawn = gameObject.transform.position;
		NinCat = gameObject.GetComponent<Rigidbody2D> ();
		catColl = gameObject.GetComponent<PolygonCollider2D> ();
		ca = gameObject.GetComponent<Animator> ();
		speed = -1.0f;
		jumpHeight = 3.0f;
		jumping = false;
		waittime = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Doggo> ().hidden == false) {
			if (player.GetComponent<Doggo> ().dead == true) {
				if (waittime == false) {
					waittime = true;
					StartCoroutine ("Death"); 
				}
			}
			if (!jumping && player.GetComponent<Doggo> ().dead == false) {
				caDB.Play ();
				ca.Play ("CatJumpUp");
				NinCat.velocity = new Vector2 (NinCat.velocity.x, jumpHeight * 1.5f);
				jumping = true;
			}
			if ((player.transform.position.x < gameObject.transform.position.x)) {
				speed = -1.0f;
				if (gameObject.GetComponent<SpriteRenderer> ().flipX == true) {
					gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				}
				NinCat.velocity = new Vector2 (speed, NinCat.velocity.y);
			} else if ((player.transform.position.x > gameObject.transform.position.x)) {
				speed = 1.0f;
				if (gameObject.GetComponent<SpriteRenderer> ().flipX == false) {
					gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				}
				NinCat.velocity = new Vector2 (speed, NinCat.velocity.y);
			} 
			if (NinCat.velocity.y < 0) {
				ca.Play ("CatJumpDown");
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Floor") && jumping == true) {
			jumping = false;
		}
		else if (coll.gameObject.CompareTag ("DogCatcherJR") && jumping == true) {
			jumping = false;
		}
		else if (coll.gameObject.CompareTag ("NinjaCat") && jumping == true) {
			speed = -speed;
			jumping = false;
		}

	}

	public IEnumerator Death()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = initSpawn;
		jumping = false;
		waittime = false;
	}

	public IEnumerator Die()
	{
		catColl.enabled = false;
		caDS.Play ();
		ca.Play ("CatPoof");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (1.05f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy (gameObject);
	}
}
