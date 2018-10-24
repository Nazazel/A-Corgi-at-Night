using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketElevator : MonoBehaviour {

	public GameObject elevatorTrigger;
	public bool inTrigger;
	public bool ascensionStarted;
	public Rigidbody2D rb;
	public GameObject corgo;
	public bool targetReached;

	void Start()
	{
		inTrigger = false;
		Debug.Log (gameObject.transform.position.y);
		targetReached = false;
		ascensionStarted = false;
		rb = gameObject.GetComponent<Rigidbody2D>();
		corgo = GameObject.Find("QuinSpriteFinal_1");
	}

	void Update()
	{
		if (ascensionStarted && gameObject.transform.position.y < 0.86f)
		{
			Debug.Log (corgo.GetComponent<Rigidbody2D> ().sharedMaterial.friction);
			rb.velocity = new Vector2(0.0f, 1.0f);
			gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		else if (ascensionStarted && !targetReached)
		{
			corgo.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0.4f;
			Debug.Log(gameObject.transform.position.y);
			Debug.Log("Target Reached");
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			rb.velocity = new Vector2(0f, 0f);
			targetReached = true;
		}

		if (corgo.transform.position.y < gameObject.transform.position.y && gameObject.transform.position.y > -1.4f && ascensionStarted) {
			Debug.Log ("whaaaaaaa");
			corgo.GetComponent<Rigidbody2D>().sharedMaterial.friction=0.4f;
			rb.gravityScale = 0.5f;
			elevatorTrigger.SetActive (true);
			targetReached = false;
			ascensionStarted = false;
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}
	}
	// Use this for initialization
	void OnCollisionStay2D(Collision2D col)
	{
		Debug.Log (col.gameObject.name);
		if (col.gameObject.CompareTag("Player") && inTrigger && !ascensionStarted)
		{
            rb.gravityScale = 0;
            col.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.friction=0;
			ascensionStarted = true;
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}
	}
		
}
