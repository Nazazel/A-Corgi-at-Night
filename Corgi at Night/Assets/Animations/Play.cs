using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
	
	private Text finScore;
    public AudioSource audio;
    public bool started;

    // Use this for initialization
    void Start()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            if (!started)
            {
                started = true;
                StartCoroutine("wait");
            }
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1F);
        SceneManager.LoadSceneAsync("Main Level");
    }

	public void vidStop()
	{
		((MovieTexture)GetComponent<Renderer> ().material.mainTexture).Pause();
		audio.Stop ();
	}
}
