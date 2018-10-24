using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour {

    public bool anim;
    public bool idleNotPlayed = true;
    public bool finishedIdle = false;
    public Animator pa;
	// Use this for initialization
	void Start () {
        anim = false;
        pa = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim)
        {
            Debug.Log(gameObject.transform.position.x);
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                if(idleNotPlayed && gameObject.transform.position.x > -1.0f)
                {
                    pa.Play("Walk");
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
                }
                else if (idleNotPlayed && gameObject.transform.position.x <= -1.0f)
                {
                    idleNotPlayed = false;
                    StartCoroutine("idleAnimate");
                }
                else if(finishedIdle)
                {
                    pa.Play("Walk");
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
                }
                
                
            }
            else
            {
                if (idleNotPlayed && gameObject.transform.position.x < -2.0f)
                {
                    pa.Play("Walk");
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
                }
                else if (idleNotPlayed && gameObject.transform.position.x >= -2.0f)
                {
                    idleNotPlayed = false;
                    StartCoroutine("idleAnimate");
                }
                else if (finishedIdle)
                {
                    pa.Play("Walk");
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
                }
            }
        }
	}

    public void AnimatingTime()
    {
        anim = true;
        StartCoroutine("TimedDestruction");
    }

    public IEnumerator idleAnimate()
    {
        pa.Play("Stand");
        yield return new WaitForSeconds(0.5f);
        pa.Play("SniffStand");
        yield return new WaitForSeconds(2.15f);
        pa.Play("Stand");
        yield return new WaitForSeconds(0.5f);
        pa.Play("LookR");
        yield return new WaitForSeconds(2.15f);
        finishedIdle = true;
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(gameObject);
    }
}
