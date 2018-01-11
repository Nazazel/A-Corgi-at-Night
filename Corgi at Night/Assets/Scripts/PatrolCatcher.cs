using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCatcher : MonoBehaviour {

	private float patrolArea;
	private float originPos;
	private Animator pa;
	private Rigidbody2D DC;
	private GameObject player;
	private bool right;
	public Vector3 initSpawn;
	private bool waittime;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		pa = gameObject.GetComponent<Animator> ();
		DC = gameObject.GetComponent<Rigidbody2D> ();
		originPos = gameObject.transform.position.x;
		patrolArea = 2.0f;
		initSpawn = gameObject.transform.position;
		waittime = false;
		if ((player.transform.position.x < gameObject.transform.position.x)) {
			right = false;
		} 
		else if ((player.transform.position.x > gameObject.transform.position.x)) {
			gameObject.transform.Rotate (Vector3.up, 180.0f);
			right = true;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Doggo> ().dead == true) {
			if (waittime == false) {
				waittime = true;
				StartCoroutine ("Death");
			}
		}
//		if (player.GetComponent<Doggo> ().firstPatrol && (Mathf.Abs(gameObject.transform.position.x-player.transform.position.x)<=2.52)){
//			Debug.Log (gameObject.transform.position.x);
//			Debug.Log (player.transform.position.x);
//			player.GetComponent<Doggo> ().firstPatrol = false;
//			player.GetComponent<Doggo> ().firstPatrolCatcher ();
//		}
		if ((gameObject.transform.position.x <= originPos - patrolArea) && right == false) {
			//Debug.Log ("Turn Right");
			pa.Play ("Patrol");
			right = true;
			gameObject.transform.Rotate (Vector3.up, 180.0f);
			DC.velocity = new Vector2 (0.5f, DC.velocity.y);
		} else if ((gameObject.transform.position.x >= originPos + patrolArea) && right == true) {
			//Debug.Log ("Turn Left");
			pa.Play ("Patrol");
			right = false;
			gameObject.transform.Rotate (Vector3.up, 180.0f);
			DC.velocity = new Vector2 (-0.5f, DC.velocity.y);
		} else {
			if (right) {
				//Debug.Log ("Moving Right");
				DC.velocity = new Vector2 (0.5f, DC.velocity.y);
			} else {
				//Debug.Log ("Moving Left");
				DC.velocity = new Vector2 (-0.5f, DC.velocity.y);
			}
		}
	}

	public IEnumerator Death()
	{
		pa.enabled = false;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = initSpawn;
		pa.enabled = true;
		waittime = false;
	}
}
