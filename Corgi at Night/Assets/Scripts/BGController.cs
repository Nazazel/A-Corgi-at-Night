using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour {

	public GameObject cityBG;
	public GameObject carnivalBG;
	public GameObject moonBG;

	// Use this for initialization
	void Start () {
		cityBG.SetActive (true);
		carnivalBG.SetActive (false);
		moonBG.SetActive (false);
	}
	
	// Update is called once per frame
	public void cityFinish()
	{
		cityBG.SetActive (false);
		carnivalBG.SetActive (true);
	}

	public void carnivalFinish()
	{
		carnivalBG.SetActive (false);
	}

	public void nosoFinish()
	{
		moonBG.SetActive (true);
	}

	public void moonFinish()
	{

	}
}
