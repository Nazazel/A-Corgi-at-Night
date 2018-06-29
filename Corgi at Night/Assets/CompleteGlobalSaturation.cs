using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGlobalSaturation : MonoBehaviour {

	private Rigidbody2D DCJ;
	private GameObject player;
	private Animator ga;
	private float speed;
	public Vector3 initSpawn;
	public Vector3 levelStart;
	private bool waittime;
	public bool holding;
	private bool holdbreak;
	private bool switching;
	private float originPos;
	private float patrolArea;
	private bool right;
	public bool beaming;
	private bool coroutineStarted;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
		right = true;
		player = GameObject.Find("QuinSpriteFinal_1");
		ga = gameObject.GetComponent<Animator>();
		originPos = gameObject.transform.position.x;
		patrolArea = 2.0f;
		initSpawn = gameObject.transform.position;
		DCJ = gameObject.GetComponent<Rigidbody2D>();
		waittime = false;
		holdbreak = false;
		holding = false;
		switching = false;
		beaming = true;
		coroutineStarted = false;
		speed = -1.25f;
		ga.Play("RoombaFloat");

	}

	// Update is called once per frame
	void Update()
	{
		if (player.GetComponent<Pibble>().dead == true)
		{
			if (waittime == false)
			{
				waittime = true;
				StopAllCoroutines();
				StartCoroutine("Death");
			}
		}
		if (!player.GetComponent<Pibble>().dead)
		{
			if ((gameObject.transform.position.x <= originPos - patrolArea) && right == false)
			{
				//Debug.Log ("Turn Up");
				right = true;
				DCJ.velocity = new Vector2(0.5f, 0.0f);
			}
			else if ((gameObject.transform.position.x >= originPos + patrolArea) && right == true)
			{
				//Debug.Log ("Turn Down");
				right = false;
				DCJ.velocity = new Vector2(-0.5f, 0.0f);
			}
			else
			{
				if (right)
				{
					//Debug.Log ("Moving Up");
					if (!coroutineStarted) {
						coroutineStarted = true;
						StartCoroutine ("Beam");
					}
					DCJ.velocity = new Vector2(0.5f, 0.0f);
				}
				else
				{
					//Debug.Log ("Moving Down");
					if (!coroutineStarted) {
						coroutineStarted = true;
						StartCoroutine ("Beam");
					}
					DCJ.velocity = new Vector2(-0.5f, 0.0f);
				}
			}
		}
	}

	public IEnumerator Death()
	{
		StopCoroutine ("Beam");
		ga.enabled = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds(3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
		gameObject.transform.position = initSpawn;
		coroutineStarted = false;
		ga.enabled = true;
		waittime = false;
	}

	public IEnumerator Beam()
	{
		ga.Play ("Abduct");
		yield return new WaitForSeconds (0.33f);
		gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
		ga.Play("beamMeScotty");
		yield return new WaitForSeconds (5.0f);
		gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
		ga.Play("Flying");
		yield return new WaitForSeconds (10.0f);
		coroutineStarted = false;
	}

}
