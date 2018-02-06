using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinCatStar : MonoBehaviour
{

    private Rigidbody2D NinCat;
    private Animator ca;
    public Vector3 initSpawn;
    private GameObject player;
    public float speed;
    private PolygonCollider2D catColl;
    public AudioSource caDS;
    public AudioSource caDB;
    public GameObject star;

    private float jumpHeight;
    private bool jumping;
    private bool waittime;
    private bool shooting;

    // Use this for initialization
    void Start()
    {
        shooting = false;
        player = GameObject.Find("QuinSpriteFinal_1");
        initSpawn = gameObject.transform.position;
        NinCat = gameObject.GetComponent<Rigidbody2D>();
        catColl = gameObject.GetComponent<PolygonCollider2D>();
        ca = gameObject.GetComponent<Animator>();
        speed = -1.0f;
        jumpHeight = 3.0f;
        jumping = false;
        waittime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Pibble>().hintActive == false)
        {
            if (catColl.enabled == true && player.GetComponent<Pibble>().dead == false)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if (player.GetComponent<Pibble>().hidden == false)
            {
                if (shooting == false)
                {
                    shooting = true;
                    StartCoroutine("StarThrow");
                }
                if (player.GetComponent<Pibble>().dead == true)
                {
                    if (waittime == false)
                    {
                        StopCoroutine("StarThrow");
                        waittime = true;
                        StartCoroutine("Death");
                    }
                }
                else
                {
                    if (ca.enabled == false)
                    {
                        ca.enabled = true;
                    }
                }
                if (!jumping && player.GetComponent<Pibble>().dead == false)
                {
                    caDB.Play();
                    ca.Play("CatJumpUp");
                    NinCat.velocity = new Vector2(NinCat.velocity.x, jumpHeight * 1.5f);
                    jumping = true;
                }
                if ((player.transform.position.x < gameObject.transform.position.x) && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
                {
                    speed = -1.0f;
                    if (gameObject.GetComponent<SpriteRenderer>().flipX == true && player.GetComponent<Pibble>().dead == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    NinCat.velocity = new Vector2(speed, NinCat.velocity.y);
                }
                else if ((player.transform.position.x > gameObject.transform.position.x) && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
                {
                    speed = 1.0f;
                    if (gameObject.GetComponent<SpriteRenderer>().flipX == false && player.GetComponent<Pibble>().dead == false)
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    NinCat.velocity = new Vector2(speed, NinCat.velocity.y);
                }
                if (NinCat.velocity.y < 0)
                {
                    ca.Play("CatJumpDown");
                }
            }
            else
            {
                ca.Play("CatIdle");
            }
        }
        else
        {
            ca.enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor") && jumping == true)
        {
            jumping = false;
        }
        else if (coll.gameObject.CompareTag("DogCatcherJR") && jumping == true)
        {
            jumping = false;
        }
        else if (coll.gameObject.CompareTag("NinjaCat") && jumping == true)
        {
            speed = -speed;
            jumping = false;
        }

    }

    public IEnumerator Death()
    {
        shooting = true;
        StopCoroutine("StarThrow");
        ca.enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.position = initSpawn;
        ca.enabled = true;
        jumping = false;
        waittime = false;
        shooting = false;
    }

    public IEnumerator Die()
    {
        shooting = true;
        StopCoroutine("StarThrow");
        catColl.enabled = false;
        caDS.Play();
        ca.Play("CatPoof");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(1.05f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Destroy(gameObject);
    }

    public IEnumerator StarThrow()
    {
        Instantiate(star, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(star, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(star, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        shooting = false;
    }
}