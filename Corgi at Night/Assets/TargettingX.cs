using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingX : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("whyarewehere");
        //justtosuffer?
	}
	
    public IEnumerator whyarewehere()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
