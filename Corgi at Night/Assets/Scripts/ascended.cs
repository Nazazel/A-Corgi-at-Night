using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ascended : MonoBehaviour {

    public GameObject elevatorBarrier;
    public bool ascensionStarted;
    public Rigidbody2D rb;
    public GameObject corgo;
    public bool targetReached;

    void Start()
    {
        targetReached = false;
        ascensionStarted = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        corgo = GameObject.Find("QuinSpriteFinal_1");
    }

    void Update()
    {
        if (ascensionStarted && gameObject.transform.position.y < -1.636f)
        {
            rb.velocity = new Vector2(0.0f, 1.0f);
			gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
        }
        else if (ascensionStarted && !targetReached)
        {
            corgo.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0.4f;
            Debug.Log(gameObject.transform.position.y);
            Debug.Log("Target Reached");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.velocity = new Vector2(0f, 0f);
            targetReached = true;
        }
    }
    // Use this for initialization
    void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Player") && col.gameObject.transform.position.x >= 150.3f && !ascensionStarted)
        {
            col.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.friction=0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            ascensionStarted = true;
            StartCoroutine("ascend");
        }
    }

    public IEnumerator ascend()
    {
        elevatorBarrier.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        elevatorBarrier.SetActive(false);
    }
}
