using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overthasea : MonoBehaviour {

    public GameObject bgc;
    public bool started;

    // Use this for initialization
    void Start()
    {
        started = false;
        bgc = GameObject.Find("BGController");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!started)
            {
                started = true;
                StartCoroutine("plsEnd");
            }
        }
    }

    public IEnumerator plsEnd()
    {
        yield return new WaitForSeconds(1.0f);
        bgc.GetComponent<BGController>().FadeIn(1.0f);
        Destroy(gameObject);
    }
}
