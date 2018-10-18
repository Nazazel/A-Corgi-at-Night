using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTheSea : MonoBehaviour {

	public GameObject bgc;
    public bool started;

	// Use this for initialization
	void Start () {
        started = false;
		bgc = GameObject.Find ("BGController");
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
        bgc.GetComponent<BGController>().initiateFO();
        yield return new WaitForSeconds(2.1f);
        bgc.GetComponent<BGController>().carnivalFinish();
        Destroy(gameObject);
    }

}
