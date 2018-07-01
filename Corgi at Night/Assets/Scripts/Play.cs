using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Play : MonoBehaviour
{
	
	private Text finScore;
	public VideoPlayer movAudio;
    public bool started;
	public bool delayed;

	void Start()
	{
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
        SceneManager.LoadSceneAsync("Main Level");
    }

	IEnumerator DelayedStart()
	{
		yield return new WaitForSeconds(1F);
		delayed = true;
	}

}
