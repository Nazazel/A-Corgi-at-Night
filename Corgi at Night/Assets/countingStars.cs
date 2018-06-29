using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countingStars : MonoBehaviour {

	private SpriteRenderer sr;
	private Animator sa;
	private bool started;
	public float intervalBetweenStars;

	// Use this for initialization
	void Start () {
		started = false;
		sa = gameObject.GetComponent<Animator> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		sr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			started = true;
			sr.enabled = true;
			StartCoroutine ("doot");
		}
	}

	public IEnumerator doot()
	{
		sa.Play ("TwinkleTwinkle", -1, 0f);
		yield return new WaitForSeconds (0.75f);
		sr.enabled = false;
		yield return new WaitForSeconds (intervalBetweenStars);
		sa.playbackTime = 0;
		started = false;

	}
}
