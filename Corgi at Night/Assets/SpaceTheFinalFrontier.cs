using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTheFinalFrontier : MonoBehaviour {

	public GameObject sky;
	public GameObject stars;
	public GameObject clouds;
	private bool RocketCutsceneStart;
	private AudioSource rs;

	// Use this for initialization
	void Start () {
		RocketCutsceneStart = false;
		rs = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (RocketCutsceneStart) {
			StartCoroutine ("cutsceneTime");
			RocketCutsceneStart = false;
		}
	}

	public void showtime()
	{
		RocketCutsceneStart = true;
	}

	public IEnumerator cutsceneTime()
	{
		rs.Play ();
		yield return new WaitForSeconds (12.0f);
		clouds.GetComponent<CantMeltSteelBeams> ().fireUpTheEngines ();
		yield return new WaitForSeconds (2.0f);
		clouds.GetComponent<CantMeltSteelBeams> ().liftoff ();
		sky.GetComponent<skyMovement> ().beginTakeoff ();
		stars.GetComponent<starMovement> ().beginTakeoff ();
		yield return new WaitForSeconds (1.0f);
		sky.GetComponent<skyMovement> ().liftoffStop ();
		stars.GetComponent<starMovement> ().liftoffStop ();
	}
}
