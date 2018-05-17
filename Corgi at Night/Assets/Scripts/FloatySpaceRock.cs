using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpaceRock : MonoBehaviour {

    private Rigidbody2D DCJ;
    private GameObject player;
    private Animator ga;
    private float speed;
    public Vector3 initSpawn;
    private bool waittime;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
        ga = gameObject.GetComponent<Animator>();
        initSpawn = gameObject.transform.position;
        DCJ = gameObject.GetComponent<Rigidbody2D>();
        waittime = false;
        speed = -1.0f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }
	
	// Update is called once per frame
	void Update () {
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

    public IEnumerator Death()
    {
        ga.enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        gameObject.transform.position = initSpawn;
        ga.enabled = true;
        waittime = false;
    }
}
