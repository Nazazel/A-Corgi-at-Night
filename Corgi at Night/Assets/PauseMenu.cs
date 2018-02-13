using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P) && player.GetComponent<Pibble>().paused == false)
        {
            player.GetComponent<Pibble>().paused = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (Input.GetKeyDown(KeyCode.P) && player.GetComponent<Pibble>().paused == true)
        {
            player.GetComponent<Pibble>().paused = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
