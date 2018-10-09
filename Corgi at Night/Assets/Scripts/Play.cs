using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Experimental.Audio;

public class Play : MonoBehaviour
{
	
	private Text finScore;
	public VideoPlayer movAudio;
    public AudioSource vidAudio;
    public bool started;
	public bool delayed;

	void Start()
	{
        //Debug.Log(movAudio.GetComponent<AudioSource>().clip.name);
        movAudio.Play();
        vidAudio.Play();
		delayed = false;
		StartCoroutine ("DelayedStart");
	}
    // Update is called once per frame
    void Update()
    {
		Debug.Log (movAudio.isPlaying);
		if (!movAudio.isPlaying && delayed)
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
        if (SceneManager.GetActiveScene().name == "End")
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
        else
        {
            SceneManager.LoadSceneAsync("Main Level");
        }
    }

	IEnumerator DelayedStart()
	{
		yield return new WaitForSeconds(1F);
		delayed = true;
	}

}
