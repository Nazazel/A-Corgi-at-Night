using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravityZone : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0.66f;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }
}
