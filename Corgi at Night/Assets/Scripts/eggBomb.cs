using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggBomb : MonoBehaviour {

    private CircleCollider2D shellsplosion;
    private Animator eggAnim;
    private GameObject player;
    private bool started;

    // Update is called once per frame
    void Start () {
        started = false;
        player = GameObject.Find("QuinSpriteFinal_1");
        shellsplosion = gameObject.GetComponent<CircleCollider2D>();
        eggAnim = gameObject.GetComponent<Animator>();
	}

    void Update()
    {
        if (player.GetComponent<Pibble>().dead == true || player.GetComponent<Pibble>().hintActive == true || player.GetComponent<Pibble>().paused == true)
        {
            if(player.GetComponent<Pibble>().dead == true && !started)
            {
                started = true;
                StartCoroutine("missioncomplete");
            }
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor"))
        {
            shellsplosion.radius = shellsplosion.radius * 1.5f;
            StartCoroutine("deathLinger");
            eggAnim.Play("boom");
        }
    }

    public IEnumerator deathLinger()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public IEnumerator missioncomplete()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
