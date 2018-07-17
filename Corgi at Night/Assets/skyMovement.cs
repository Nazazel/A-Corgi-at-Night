using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyMovement : MonoBehaviour {

	private bool startLiftoff;

	// Use this for initialization
	void Start () {
		startLiftoff = false;
	}

	// Update is called once per frame
	void Update () {
		if (startLiftoff) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -1f);
		}
	}

	public void beginTakeoff()
	{
		startLiftoff = true;
	}

	public void liftoffStop()
	{
		startLiftoff = false;
	}
}
