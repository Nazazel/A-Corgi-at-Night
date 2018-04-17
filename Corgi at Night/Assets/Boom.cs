using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{

    public GameObject player;
    public GameObject doom;
    public Animator doomAnim;
    public Vector3 initSpawn;

    // Use this for initialization
    void Start()
    {
        initSpawn = doom.transform.position;
        player = GameObject.Find("QuinSpriteFinal_1");
        doomAnim = doom.GetComponent<Animator>();
        doomAnim.Play("RoombaBoom");
        doom.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && player.GetComponent<Pibble>().dead == false)
        {
            StartCoroutine("SelfDestructSequenceInitiated");
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && player.GetComponent<Pibble>().dead == false)
        {
            StopCoroutine("SelfDestructSequenceInitiated");
        }
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.58f);
        doomAnim.enabled = false;
        yield return new WaitForSeconds(2.42f);
        gameObject.transform.position = initSpawn;
        doomAnim.enabled = true;
        doomAnim.Play("RoombaBoom");
        
    }

    public IEnumerator SelfDestructSequenceInitiated()
    {
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<Pibble>().dead = true;
        player.GetComponent<Pibble>().StartCoroutine("Death");
        doomAnim.Play("Boomba");
        StartCoroutine("Death");
    }
}
