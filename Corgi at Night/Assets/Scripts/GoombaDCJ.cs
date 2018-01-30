using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaDCJ : MonoBehaviour {

	private Rigidbody2D DCJ;
	private GameObject player;
	private Animator ga;
	private float speed;
	public Vector3 initSpawn;
	private bool waittime;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		ga = gameObject.GetComponent<Animator> ();
		initSpawn = gameObject.transform.position;
		DCJ = gameObject.GetComponent<Rigidbody2D> ();
		waittime = false;
	}

	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Pibble> ().hidden == false) {
			if (player.GetComponent<Pibble> ().dead == true) {
				if (waittime == false) {
					waittime = true;
					StartCoroutine ("Death");
				}
			}
			if ((player.transform.position.x < gameObject.transform.position.x) && player.GetComponent<Doggo> ().dead == false && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7) {
				if (gameObject.GetComponent<SpriteRenderer> ().flipX == true) {
					gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				}
				speed = -0.75f;
				DCJ.velocity = new Vector2 (speed, DCJ.velocity.y);
			} else if ((player.transform.position.x > gameObject.transform.position.x) && player.GetComponent<Doggo> ().dead == false && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7) {
				if (gameObject.GetComponent<SpriteRenderer> ().flipX == false) {
					gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				}
				speed = 0.75f;
				DCJ.velocity = new Vector2 (speed, DCJ.velocity.y);
			} else if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7) {
				DCJ.velocity = new Vector2 (-speed, DCJ.velocity.y);
			}
		}
	}

	public IEnumerator Death()
	{
		ga.enabled = false;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = initSpawn;
		ga.enabled = true;
		waittime = false;
	}

	public IEnumerator Die()
	{
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		ga.Play("DCJDie");
		yield return new WaitForSeconds (0.7f);
		Destroy (gameObject);
	}
}
