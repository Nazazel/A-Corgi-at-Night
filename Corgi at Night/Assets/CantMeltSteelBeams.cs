using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMeltSteelBeams : MonoBehaviour {

	private bool startRocketEngines;
	public GameObject cloud1;
	public GameObject cloud2;
	public GameObject cloud3;
	public GameObject cloud4;
	public GameObject cloud5;
	public GameObject cloud6;

	// Use this for initialization
	void Start () {
		startRocketEngines = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startRocketEngines) {
			cloud1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
			cloud2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
			cloud3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
			cloud4.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
			cloud5.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
			cloud6.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 2.5f);
		}
	}

	public void fireUpTheEngines()
	{
		startRocketEngines = true;
	}

	public void liftoff()
	{
		startRocketEngines = false;
		StartCoroutine ("lifting");
	}

	public IEnumerator lifting()
	{
		yield return new WaitForSeconds (1.0f);
		cloud1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
		cloud2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
		cloud3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
		cloud4.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
		cloud5.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
		cloud6.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -2f);
	}
}
