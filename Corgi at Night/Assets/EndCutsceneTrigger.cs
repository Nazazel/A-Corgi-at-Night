using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutsceneTrigger : MonoBehaviour {

    public GameObject vet;

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<Pibble>().outroFinished == false && col.gameObject.GetComponent<Pibble>().outroRunning == false)
            {
                col.gameObject.GetComponent<Pibble>().outroRunning = true;
                vet.GetComponent<TimeToDie>().stabby = true;
            }
        }
	}
}
