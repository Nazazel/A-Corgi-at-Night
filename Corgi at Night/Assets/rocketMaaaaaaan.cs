using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketMaaaaaaan : MonoBehaviour {

	public GameObject door;
	public GameObject doorClose;
	private GameObject fade;
	public GameObject teleportExit;
	public GameObject bgc;
	private GameObject player;
	public GameObject rocket;

	// Use this for initialization
	void Start () {
		fade = GameObject.Find("TransitionControl");
		player = GameObject.Find("QuinSpriteFinal_1");
		bgc = GameObject.Find ("BGController");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			StartCoroutine("DemoEnding");
		}       
	}

	public IEnumerator DemoEnding()
	{
		door.SetActive (false);
		doorClose.SetActive (true);
		fade.GetComponent<TransitionController>().FadeBegin();
		yield return new WaitForSeconds(1.0f);
		bgc.GetComponent<BGController> ().nosoFinish ();
		player.transform.position = new Vector3(teleportExit.transform.position.x, teleportExit.transform.position.y + 0.1f, teleportExit.transform.position.z);
		yield return new WaitForSeconds(3.0f);
		rocket.GetComponent<SpaceTheFinalFrontier> ().showtime ();
	}

}
