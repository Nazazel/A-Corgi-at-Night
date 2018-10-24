using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : MonoBehaviour {

    private Rigidbody2D DCJ;
    private GameObject player;
    private Animator ga;
    private float speed;
    public Vector3 initSpawn;
    public Vector3 levelStart;
    private bool waittime;
    public bool holding;
    private bool holdbreak;
    public bool switching;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
        ga = gameObject.GetComponent<Animator>();
        initSpawn = gameObject.transform.position;
        DCJ = gameObject.GetComponent<Rigidbody2D>();
        waittime = false;
        holdbreak = false;
        holding = false;
        switching = false;
        speed = -1.25f;
        StartCoroutine("windUp");
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Pibble>().hintActive == false && player.GetComponent<Pibble>().paused == false)
        {
            if (player.GetComponent<Pibble>().dead == false && ga.enabled == false)
            {
                ga.enabled = true;
                DCJ.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if (player.GetComponent<Pibble>().dead == true)
            {
                if (waittime == false)
                {
                    waittime = true;
                    StopAllCoroutines();
                    StartCoroutine("Death");
                }
            }
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
            {
                DCJ.velocity = new Vector2(speed, DCJ.velocity.y);
            }
        }
        else
        {
            ga.enabled = false;
            DCJ.velocity = new Vector2(0.0f, 0.0f);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Floor")
        {
            if (!switching && coll.gameObject.tag != "Player")
            {
                StartCoroutine("switchDir");
            }
        }
    }

    public IEnumerator switchDir()
    {
        StartCoroutine("windUp");
        switching = true;
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        speed = -1.0f * speed;
        yield return new WaitForSeconds(0.5f);
        switching = false;
    }

    public IEnumerator Death()
    {
        ga.enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.position = initSpawn;
        ga.enabled = true;
        waittime = false;
    }

    public IEnumerator windUp()
    {
        ga.Play("RoombaStartup");
        yield return new WaitForSeconds(0.33f);
        ga.Play("RoombaMove");
    }
}
