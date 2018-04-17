using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawMassacre : MonoBehaviour {

    private GameObject player;
    private bool waittime;
    private Animator ga;

	// Use this for initialization
	void Start () {
        waittime = false;
        ga = gameObject.GetComponent<Animator>();
        player = GameObject.Find("QuinSpriteFinal_1");
        ga.Play("RoombaKruger");
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (player.GetComponent<Pibble>().dead == true)
        {
            if (waittime == false)
            {
                waittime = true;
                StopAllCoroutines();
                StartCoroutine("Death");
            }
        }
    }

    public IEnumerator Death()
    {
        ga.enabled = false;
        yield return new WaitForSeconds(3.0f);
        ga.enabled = true;
        waittime = false;
    }
}
