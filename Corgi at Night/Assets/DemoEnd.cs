using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoEnd : MonoBehaviour {

    private GameObject fade;

    // Use this for initialization
    void Start () {
        fade = GameObject.Find("TransitionControl");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("DemoEnding");
        }       
    }

    public IEnumerator DemoEnding()
    {
        fade.GetComponent<TransitionController>().FadeBegin();
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("DemoEnding");
        
    }
}
