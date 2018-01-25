using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenIndiv : MonoBehaviour {

    public Vector3 initSpawn;
    private Vector3 playerPos;
    private GameObject player;
    public bool falling; 

	void Start()
    {
        player = GameObject.Find("QuinSpriteFinal_1");
        falling = false;
    }

	void Update () {
        if (!falling)
        {
            playerPos = player.transform.position;
            initSpawn = gameObject.transform.position;
        }
	}


}
