using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsNotMurderIfItsRobots : MonoBehaviour {

    public GameObject rover;
    public int health;

    void Start()
    {
        health = 2;
        rover = gameObject.transform.parent.gameObject;
    }

    public void Die()
    {
		
        if (health == 2)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            health = 1;
            StartCoroutine("stun");
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rover.SendMessage("Die2");
        }
    }

    public IEnumerator stun()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
