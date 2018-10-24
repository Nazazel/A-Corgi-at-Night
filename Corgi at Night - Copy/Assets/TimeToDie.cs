using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToDie : MonoBehaviour {

    private GameObject fade;
    public bool stabby;
    public GameObject quinn;
    private bool started;
    private Animator va;

	// Use this for initialization
	void Start () {
        fade = GameObject.Find("TransitionControl");
        quinn = GameObject.FindWithTag("Player");
        va = gameObject.GetComponent<Animator>();
        stabby = false;
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(stabby && !started)
        {
            started = true;
            StartCoroutine("ItsPizzaTime");
        }
	}

    public IEnumerator ItsPizzaTime()
    {
        yield return new WaitForSeconds(3.4f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        va.Play("vet_injection");
        yield return new WaitForSeconds(0.5f);
        quinn.GetComponent<Pibble>().StartCoroutine("End");
        va.Play("vet_laugh");
        yield return new WaitForSeconds(4.0f);
        fade.GetComponent<TransitionController>().FadeBegin();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadSceneAsync("End");
        


    }
}
