using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private GameObject player;
    private GameObject pauseText;
    public GameObject controlsPage;
    public bool pauseCursor;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        player = GameObject.Find("QuinSpriteFinal_1");
        pauseText = GameObject.Find("PauseText");
        pauseText.SetActive(false);
        pauseCursor = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && player.GetComponent<Pibble>().paused == false)
        {
            pauseCursor = true;
            player.GetComponent<Pibble>().paused = true;
            pauseText.SetActive(true);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && player.GetComponent<Pibble>().paused == true)
        {
            pauseCursor = false;
            if (controlsPage.activeSelf == true)
            {
                controlsPage.SetActive(false);
            }
            player.GetComponent<Pibble>().paused = false;
            pauseText.SetActive(false);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if(pauseCursor)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
