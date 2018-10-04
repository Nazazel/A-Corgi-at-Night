using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTheFinalFrontier : MonoBehaviour {

	public GameObject sky;
	public GameObject stars;
	public GameObject clouds;
	public GameObject exitScene;
	private bool RocketCutsceneStart;
	private AudioSource rs;
	private GameObject fade;
	public GameObject bgc;
	private GameObject player;


	// Use this for initialization
	void Start () {
		RocketCutsceneStart = false;
		rs = gameObject.GetComponent<AudioSource> ();
		fade = GameObject.Find("TransitionControl");
		player = GameObject.Find("QuinSpriteFinal_1");
		bgc = GameObject.Find ("BGController");
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
		yield return new WaitForSeconds (15.0f);
		fade.GetComponent<TransitionController>().FadeBegin();
		yield return new WaitForSeconds(1.0f);
		bgc.GetComponent<BGController> ().nosoFinish ();
		player.transform.position = new Vector3(exitScene.transform.position.x, exitScene.transform.position.y + 0.1f, exitScene.transform.position.z);
		yield return new WaitForSeconds(3.0f);
	}
}
