using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour {

	public GameObject cityBG;
	public GameObject carnivalBG;
	public GameObject moonBG;

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
                cityBG.SetActive(true);
                carnivalBG.SetActive(false);
                moonBG.SetActive(false);
            }
            else if (carnivalActive)
            {
                cityFinish();
            }
            else if (nosoActive)
            {
                carnivalFinish();
            }
            else if (moonActive)
            {
                nosoFinish();
            }
        }
        else
        {
            cityActive = true;
            carnivalActive = false;
            nosoActive = false;
            moonActive = false;
            cityBG.SetActive(true);
            carnivalBG.SetActive(false);
            moonBG.SetActive(false);
        }
	}
	
	// Update is called once per frame
	public void cityFinish()
	{
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
        cityBG.SetActive(false);
        carnivalBG.SetActive(false);
        moonBG.SetActive (true);
        cityActive = false;
        carnivalActive = false;
        nosoActive = false;
        moonActive = true;
    }

}
