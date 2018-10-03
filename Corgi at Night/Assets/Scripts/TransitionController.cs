using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public GameObject musicManager;

    public GameObject Fade1;
    public GameObject Fade2;
    public GameObject Fade3;
    public GameObject Fade4;
    public GameObject Fade5;
    public GameObject Fade6;
    public GameObject Fade7;
	public GameObject Fade8;
	public GameObject Fade9;
	public GameObject Fade10;
	public GameObject Fade11;
	public GameObject Fade12;
	public GameObject Fade13;
	public GameObject Fade14;
	public GameObject Fade15;
	public GameObject Fade16;
	public GameObject Fade17;
    public GameObject Fade18;

    // Update is called once per frame
    public void FadeBegin()
    {
        Debug.Log("setTrue");
        Fade1.SetActive(true);
        Fade2.SetActive(true);
        Fade3.SetActive(true);
        Fade4.SetActive(true);
        Fade5.SetActive(true);
        Fade6.SetActive(true);
        Fade7.SetActive(true);
		Fade8.SetActive(true);
		Fade9.SetActive(true);
		Fade10.SetActive(true);
		Fade11.SetActive(true);
		Fade12.SetActive(true);
		Fade13.SetActive(true);
		Fade14.SetActive(true);
		Fade15.SetActive(true);
		Fade16.SetActive(true);
		Fade17.SetActive(true);
        Fade18.SetActive(true);
        Debug.Log("Run Code");
        Fade1.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade2.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade3.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade4.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade5.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade6.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade7.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade8.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade9.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade10.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade11.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade12.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade13.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade14.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade15.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade16.GetComponent<TransitionScreenGrp>().FadeDiag();
		Fade17.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade18.GetComponent<TransitionScreenGrp>().FadeDiag();
        Debug.Log("Code Complete");
        musicManager.GetComponent<BGController>().FadeOut(3.0f);
        StartCoroutine("fadeBack");
    }

    public IEnumerator fadeBack()
    {
        yield return new WaitForSeconds(3.1f);
        musicManager.GetComponent<BGController>().FadeIn(2.0f);
    }
}
