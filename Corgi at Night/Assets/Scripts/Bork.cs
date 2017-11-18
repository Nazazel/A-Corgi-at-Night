using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bork : MonoBehaviour {
	
	private GameObject player;
	private bool delete;
	private ArrayList enemies = new ArrayList ();
	private GameObject temp;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("QuinSpriteFinal_1");
		delete = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && player.GetComponent<Doggo> ().dead == false && player.GetComponent<Doggo> ().hidden == false && player.GetComponent<Rigidbody2D>().velocity.x == 0 && player.GetComponent<Doggo> ().barking == false && player.GetComponent<Doggo> ().running == false && player.GetComponent<Doggo> ().crouching == false && player.GetComponent<Doggo> ().jumping == false) {
			player.GetComponent<Doggo> ().Bark ();
			if (delete) {
				StartCoroutine ("BorkAttack");
				delete = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("NinjaCat")) {
			enemies.Add (col.gameObject);
			enemies.TrimToSize ();
			delete = true;
			Debug.Log ("Enemy Spotted");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("NinjaCat")) {
			enemies.Remove (col.gameObject);
			enemies.TrimToSize ();
			if (enemies.Count == 0) {
				delete = false;
			}
		}
	}

	private IEnumerator BorkAttack()
	{
		for (int i = 0; i < enemies.Count; i++) {
			temp = (GameObject)enemies [i];
			temp.SendMessage("Die");
		}
		yield return new WaitForSeconds (0.01f);
		enemies.Clear ();
		enemies.TrimToSize ();
	}
}
