using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuinnChase : MonoBehaviour {

    public bool anim;
    public bool idleNotPlayed = true;
    public bool finishedIdle = false;
    public Animator pa;
    // Use this for initialization
    void Start()
    {
        anim = false;
        pa = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            Debug.Log(gameObject.transform.position.x);
            pa.Play("Run");
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0.0f);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0.0f);
            }
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
