using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public bool laserCooldown;
    public GameObject laserBottom;
    public GameObject laserTop;

	// Use this for initialization
	void Start () {
        laserCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!laserCooldown)
        {
            StartCoroutine("FirinMahLaser");
            laserCooldown = true;
        }
	}

    public IEnumerator FirinMahLaser()
    {
        laserBottom.SetActive(true);
        laserTop.SetActive(true);
        laserBottom.GetComponent<Animator>().Play("LaserStartup");
        laserTop.GetComponent<Animator>().Play("LaserStartup");
        yield return new WaitForSeconds(0.25f);
        laserBottom.GetComponent<Animator>().Play("BeamMeUpScotty");
        laserTop.GetComponent<Animator>().Play("BeamMeUpScotty");
        laserBottom.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(3.0f);
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
