using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJumpingForLife : MonoBehaviour {

    public bool jumping;
    public bool anim;
    public bool idleNotPlayed = true;
    public bool finishedIdle = false;
    public Animator pa;
    // Use this for initialization
    void Start()
    {
        anim = false;
        jumping = false;
        pa = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            Debug.Log(gameObject.transform.position.x);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (!jumping)
            {
                pa.Play("CatJumpUp");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 4.5f);
                jumping = true;
            }
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                pa.Play("CatJumpDown");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor") && jumping == true)
        {
            jumping = false;
        }
    }

        public void AnimatingTime()
    {
        anim = true;
        StartCoroutine("TimedDestruction");
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
