using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketElevatorTrigger : MonoBehaviour {

	public GameObject elevator;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			elevator.GetComponent<rocketElevator> ().inTrigger = true;
//			gameObject.SetActive (false);
		}
	}
		
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			elevator.GetComponent<rocketElevator> ().inTrigger = false;
		}
	}

}
