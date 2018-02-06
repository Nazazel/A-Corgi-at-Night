using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingStar : MonoBehaviour {
    private GameObject player;
    public NinCatStar ninja;
    private Rigidbody2D rb;
    private float dir;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            dir = -2.5f;
        }
        else if(player.transform.position.x > gameObject.transform.position.x)
        {
            dir = 2.5f;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("Projectile");
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Pibble>().hintActive == false)
        {
            rb.velocity = new Vector2(dir, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Pibble>().Caught();
        }
    }

    public IEnumerator Projectile()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
