using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROOMBAKILL : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && player.GetComponent<Pibble>().dead == false)
        {
            player.GetComponent<Pibble>().dead = true;
            player.GetComponent<Pibble>().StartCoroutine("Death");
        }
    }

}
