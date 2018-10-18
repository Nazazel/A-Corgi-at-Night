using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public bool laserCooldown;
    public GameObject laserBottom;
    public GameObject laserTop;
    private GameObject player;

    // Use this for initialization
    void Start () {
        laserCooldown = false;
        player = GameObject.Find("QuinSpriteFinal_1");
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Pibble>().hintActive == false && player.GetComponent<Pibble>().paused == false)
        {
            if (player.GetComponent<Pibble>().dead == false && laserTop.GetComponent<Animator>().enabled == false && laserBottom.GetComponent<Animator>().enabled == false)
            {
                laserBottom.GetComponent<Animator>().enabled = true;
                laserTop.GetComponent<Animator>().enabled = true;
                StartCoroutine("FirinMahLaser");
            }
            if (!laserCooldown)
            {
                StartCoroutine("FirinMahLaser");
                laserCooldown = true;
            }
        }
        else
        {
            laserTop.GetComponent<Animator>().enabled = false;
            laserBottom.GetComponent<Animator>().enabled = false;
            StopCoroutine("FirinMahLaser");
            laserCooldown = false;
        }
	}

    public IEnumerator FirinMahLaser()
    {
        laserBottom.SetActive(true);
        laserTop.SetActive(true);
        laserBottom.GetComponent<Animator>().Play("LaserStartup");
        laserTop.GetComponent<Animator>().Play("LaserStartup");
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<AudioSource>().Play();
        laserBottom.GetComponent<Animator>().Play("BeamMeUpScotty");
        laserTop.GetComponent<Animator>().Play("BeamMeUpScotty");
        laserBottom.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(3.0f);
        gameObject.GetComponent<AudioSource>().Stop();
        laserBottom.GetComponent<Animator>().Play("LaserWindDown");
        laserTop.GetComponent<Animator>().Play("LaserWindDown");
        yield return new WaitForSeconds(0.25f);
        laserBottom.GetComponent<BoxCollider2D>().enabled = false;
        laserBottom.SetActive(false);
        laserTop.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        laserCooldown = false;
    }
}
