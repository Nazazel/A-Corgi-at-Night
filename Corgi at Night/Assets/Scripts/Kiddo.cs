using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiddo : MonoBehaviour {

    private Rigidbody2D DCJ;
    private GameObject player;
    private Animator ga;
    private float speed;
    public Vector3 initSpawn;
    public Vector3 levelStart;
    private bool waittime;
    public bool holding;
    private bool holdbreak;
    private bool switching;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("QuinSpriteFinal_1");
        ga = gameObject.GetComponent<Animator>();
        initSpawn = gameObject.transform.position;
        DCJ = gameObject.GetComponent<Rigidbody2D>();
        waittime = false;
        holdbreak = false;
        holding = false;
        switching = false;
        speed = -1.25f;
        Debug.Log(gameObject.GetComponent<SpriteRenderer>().flipX);
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
        if (player.GetComponent<Pibble>().dead == true)
        {
            if (waittime == false)
            {
                waittime = true;
                StopAllCoroutines();
                StartCoroutine("Death");
            }
        }
        if (!holding && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= 7)
        {
            DCJ.velocity = new Vector2(speed, DCJ.velocity.y);
        }
        else if (holding)
        {
            player.GetComponent<Animator>().Play("SitR");
            if (!holdbreak)
            {
                ga.Play("letitgoletitgoooo");
                holdbreak = true;
                StartCoroutine("hold");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            holding = true;
        }
        else if (coll.gameObject.tag != "Floor" && !holding)
        {
            if (!switching)
            {
                StartCoroutine("switchDir");
            }
        }
    }

    public IEnumerator Death()
    {
        StopCoroutine("hold");
        ga.enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        gameObject.transform.position = initSpawn;
        ga.enabled = true;
        waittime = false;
        holding = false;
        holdbreak = false;
        switching = false;
    }

    public IEnumerator Die()
    {
        DCJ.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        ga.Play("thenperish");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    public IEnumerator switchDir()
    {
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

    public IEnumerator hold()
    {
        DCJ.velocity = new Vector2(0.0f, 0.0f);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        player.GetComponent<Pibble>().isHeld = true;
        Debug.Log(player.transform.position.x - gameObject.transform.position.x);
        if(gameObject.GetComponent<SpriteRenderer>().flipX == true && player.transform.rotation.y == 1)
        {
            if (player.transform.position.x - gameObject.transform.position.x < 0)
            {
                if (player.GetComponent<SpriteRenderer>().flipX == false)
                {
                    player.GetComponent<SpriteRenderer>().flipX = true;
                    player.transform.position = new Vector3(gameObject.transform.position.x - 0.21f, -0.87f, player.transform.position.z);
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                player.transform.position = new Vector3(gameObject.transform.position.x + 0.23f, -0.87f, player.transform.position.z);
            }

        }
        else if (gameObject.GetComponent<SpriteRenderer>().flipX == false && player.transform.rotation.y == 0)
        {
            if (player.transform.position.x - gameObject.transform.position.x > 0)
            {
                if (player.GetComponent<SpriteRenderer>().flipX == false)
                {
                    player.GetComponent<SpriteRenderer>().flipX = true;
                    player.transform.position = new Vector3(gameObject.transform.position.x + 0.21f, -0.87f, player.transform.position.z);
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                player.transform.position = new Vector3(gameObject.transform.position.x - 0.23f, -0.87f, player.transform.position.z);
            }
        }
        player.GetComponent<Pibble>().StopCoroutine("IdleAnimate");
        player.GetComponent<Pibble>().canMove = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.transform.position = new Vector3(player.transform.position.x, -0.87f, player.transform.position.z);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Pibble>().isHeld = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<SpriteRenderer>().flipX = false;
        player.GetComponent<Pibble>().canMove = true;
        StartCoroutine("Die");

    }
}