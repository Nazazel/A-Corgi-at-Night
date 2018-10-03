using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour {

	public GameObject cityBG;
	public GameObject carnivalBG;
	public GameObject moonBG;

    public AudioClip cityMusic;
    public AudioClip carnivalMusic;
    public AudioClip nosoMusic;
    public AudioClip moonMusic;

    public AudioSource levelMusic;

    public bool cityActive;
    public bool carnivalActive;
    public bool nosoActive;
    public bool moonActive;

	// Use this for initialization
	void Start () {
        if(PlayerPrefs.HasKey("PlayerPos"))
        {
            cityActive = PlayerPrefsX.GetBool("cityBG");
            carnivalActive = PlayerPrefsX.GetBool("carnivalBG");
            nosoActive = PlayerPrefsX.GetBool("nosoBG");
            moonActive = PlayerPrefsX.GetBool("moonBG");
            if (cityActive)
            {
                levelMusic.clip = cityMusic;
                cityBG.SetActive(true);
                carnivalBG.SetActive(false);
                moonBG.SetActive(false);
                FadeIn(2.0f);
            }
            else if (carnivalActive)
            {
                levelMusic.clip = carnivalMusic;
                cityFinish();
                FadeIn(2.0f);
            }
            else if (nosoActive)
            {
                levelMusic.clip = nosoMusic;
                carnivalFinish();
                FadeIn(2.0f);
            }
            else if (moonActive)
            {
                levelMusic.clip = moonMusic;
                nosoFinish();
                FadeIn(2.0f);
            }
        }
        else
        {
            levelMusic.clip = cityMusic;
            cityActive = true;
            carnivalActive = false;
            nosoActive = false;
            moonActive = false;
            cityBG.SetActive(true);
            carnivalBG.SetActive(false);
            moonBG.SetActive(false);
            FadeIn(2.0f);
        }
	}
	
	// Update is called once per frame
	public void cityFinish()
	{
        levelMusic.clip = carnivalMusic;
        cityBG.SetActive (false);
		carnivalBG.SetActive (true);
        moonBG.SetActive(false);
        cityActive = false;
        carnivalActive = true;
        nosoActive = false;
        moonActive = false;
    }

	public void carnivalFinish()
	{
        levelMusic.clip = nosoMusic;
        cityBG.SetActive(false);
		carnivalBG.SetActive (false);
        moonBG.SetActive(false);
        cityActive = false;
        carnivalActive = false;
        nosoActive = true;
        moonActive = false;
    }

	public void nosoFinish()
	{
        levelMusic.clip = moonMusic;
        cityBG.SetActive(false);
        carnivalBG.SetActive(false);
        moonBG.SetActive (true);
        cityActive = false;
        carnivalActive = false;
        nosoActive = false;
        moonActive = true;
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = levelMusic.volume;

        while (levelMusic.volume > 0)
        {
            levelMusic.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        levelMusic.Stop();
        levelMusic.volume = startVolume;
    }

    public IEnumerator FadeIn(float FadeTime)
    {
        float startVolume = 0.2f;

        levelMusic.volume = 0;
        levelMusic.Play();

        while (levelMusic.volume < 0.5f)
        {
            levelMusic.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        levelMusic.volume = 0.5f;
    }

}
