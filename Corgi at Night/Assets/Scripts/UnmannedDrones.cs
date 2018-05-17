using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmannedDrones : MonoBehaviour {

    private Rigidbody2D DCJ;
    private GameObject player;
    private Animator ga;
    private float speed;
    public Vector3 initSpawn;
    private bool waittime;
    private bool idle;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("QuinSpriteFinal_1");
        ga = gameObject.GetComponent<Animator>();
        initSpawn = gameObject.transform.position;
        DCJ = gameObject.GetComponent<Rigidbody2D>();
        waittime = false;
        idle = false;
        if (PlayerPrefs.HasKey("PlayerPos"))
        {
            if (!PlayerPrefs.HasKey(gameObject.name))
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Pibble>().hintActive == false && player.GetComponent<Pibble>().paused == false)
        {
            if (player.GetComponent<Pibble>().hidden == false)
            {
                if (idle == true)
                {
                    ga.Play("DCJWalk");
                }
                if (player.GetComponent<Pibble>().dead == true)
                {
                    if (waittime == false)
                    {
                        waittime = true;
                        StartCoroutine("Death");
                    }
                }
                else
                {
                    if (ga.enabled == false)
                    {
                        ga.enabled = true;
                    }
                }
                if ((player.transform.position.x < gameObject.transform.position.x) && player.GetComponent<Pibble>().dead == false && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
                {
                    if (gameObject.transform.rotation.y == 0)
                    {
                        gameObject.transform.Rotate(Vector3.up, 180.0f);
                        Debug.Log(gameObject.transform.rotation.y);
                    }
                    speed = -0.75f;
                    DCJ.velocity = new Vector2(speed, DCJ.velocity.y);
                }
                else if ((player.transform.position.x > gameObject.transform.position.x) && player.GetComponent<Pibble>().dead == false && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
                {
                    if (gameObject.transform.rotation.y != 0)
                    {
                        gameObject.transform.Rotate(Vector3.up, 180.0f);
                        Debug.Log(gameObject.transform.rotation.y);
                    }
                    speed = 0.75f;
                    DCJ.velocity = new Vector2(speed, DCJ.velocity.y);
                }
                else if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
                {
                    DCJ.velocity = new Vector2(-speed, DCJ.velocity.y);
                }
            }
            else
            {
                idle = true;
                ga.Play("RoverIdle");
            }
        }
        else
        {
            ga.enabled = false;
            DCJ.velocity = new Vector2(0.0f, 0.0f);
        }
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

    public IEnumerator Die()
    {
        Debug.Log("bye");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        ga.Play("WallEDeathHDin1080p");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
