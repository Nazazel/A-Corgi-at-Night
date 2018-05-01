using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public GameObject Fade1;
    public GameObject Fade2;
    public GameObject Fade3;
    public GameObject Fade4;
    public GameObject Fade5;
    public GameObject Fade6;
    public GameObject Fade7;

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
        Debug.Log("Run Code");
        Fade1.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade2.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade3.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade4.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade5.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade6.GetComponent<TransitionScreenGrp>().FadeDiag();
        Fade7.GetComponent<TransitionScreenGrp>().FadeDiag();
        Debug.Log("Code Complete");
    }
}
