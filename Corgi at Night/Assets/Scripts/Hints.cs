using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour {

	private Pibble corgo;
	private GameObject DC;
    private GameObject BB;
    private GameObject UFO;
    public GameObject las;
    private GameObject wormhole;
    private GameObject doom;
    private GameObject rock;

	// Use this for initialization
	void Start () {
        corgo = GameObject.Find("QuinSpriteFinal_1").GetComponent<Pibble>();
        if (!corgo.hintSystemDisable)
        {
            DC = GameObject.Find("Dog Catcher");
            UFO = GameObject.Find("UFO");
            BB = GameObject.Find("Birb");
            las = GameObject.Find("Laser");
            wormhole = GameObject.Find("First Wormhole");
            doom = GameObject.Find("Doobma Fly V");
            rock = GameObject.Find("Asteroid");
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
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - las.transform.position.x) <= 2.0) && corgo.firstLaser == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstLasers();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - wormhole.transform.position.x) <= 1.5) && corgo.firstWormhole == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstWormholes();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - doom.transform.position.x) <= 2.0) && corgo.firstDoomba == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstDooms();
            }
            else if ((Mathf.Abs(corgo.gameObject.transform.position.x - rock.transform.position.x) <= 3.0) && corgo.firstAsteroid == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstAsteroids();
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

            else if (col.gameObject.name == "Rover" && corgo.firstRover == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstRovs();
            }
            else if (col.gameObject.name == "EnthusiasticKid" && corgo.firstKid == true)
            {
                //col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                corgo.firstKids();
            }

        }
	}
}
