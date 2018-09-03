using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour {

	private Pibble corgo;
	private GameObject DC;
    private GameObject BB;
    private GameObject UFO;
    private GameObject las;

	// Use this for initialization
	void Start () {
        corgo = GameObject.Find("QuinSpriteFinal_1").GetComponent<Pibble>();
        if (!corgo.hintSystemDisable)
        {
            DC = GameObject.Find("Dog Catcher");
            UFO = GameObject.Find("UFO");
            BB = GameObject.Find("Birb Bomber");
            las = GameObject.Find("Laser");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!corgo.hintSystemDisable)
        {
            if ((Mathf.Abs(corgo.gameObject.transform.position.x - DC.transform.position.x) <= 3.0) && corgo.firstPatrol == true)
            {
                //DC.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstPatrolCatcher();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - BB.transform.position.x) <= 3.0) && corgo.firstBirb == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstBirbs();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - UFO.transform.position.x) <= 3.0) && corgo.firstUFO == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstUFOs();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - las.transform.position.x) <= 3.0) && corgo.firstLaser == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstLasers();
            }
        }
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        if (!corgo.hintSystemDisable)
        {
            if (col.gameObject.CompareTag("NinjaCat") && corgo.firstNinCat == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstNinCats();
            }
            else if (col.gameObject.CompareTag("DogCatcherJR") && corgo.firstDCJ == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstDCJs();
            }
            else if (col.gameObject.CompareTag("Doomba") && corgo.firstDoomba == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstDooms();
            }
            else if (col.gameObject.CompareTag("Rover") && corgo.firstRover == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstRovs();
            }
            else if (col.gameObject.CompareTag("Kid") && corgo.firstKid == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstKids();
            }
            else if (col.gameObject.CompareTag("Asteroid") && corgo.firstAsteroid == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstAsteroids();
            }

        }
	}
}
