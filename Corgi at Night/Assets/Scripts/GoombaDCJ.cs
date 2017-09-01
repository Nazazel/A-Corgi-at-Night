using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaDCJ : MonoBehaviour {

	private Rigidbody2D DCJ;
	private GameObject player;
	private float speed;
	public Vector3 initSpawn;
	private bool waittime;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		initSpawn = gameObject.transform.position;
		DCJ = gameObject.GetComponent<Rigidbody2D> ();
		waittime = false;
	}

	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Doggo> ().dead == true) {
			if (waittime == false) {
				waittime = true;
				StartCoroutine ("Death");
			}
		}
		if ((player.transform.position.x < gameObject.transform.position.x) && player.GetComponent<Doggo> ().dead == false) {
			speed = -0.75f;
			DCJ.velocity = new Vector2 (speed, DCJ.velocity.y);
		} 
		else if ((player.transform.position.x > gameObject.transform.position.x) && player.GetComponent<Doggo> ().dead == false) {
			speed = 0.75f;
			DCJ.velocity = new Vector2 (speed, DCJ.velocity.y);
		} 
		else {
			DCJ.velocity = new Vector2 (-speed, DCJ.velocity.y);
		}
	}

	public IEnumerator Death()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = initSpawn;
		waittime = false;
	}
}
