﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    public GameObject textBox;
    public Text theText;
    public AudioSource music;

    public TextAsset textFile;
    public string[] textLines;


    public int currentLine;
    public int endAtLine;

    public bool started;

    public float fadeSpeed = 1.5f;
    public Image FadeImg;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        music = gameObject.GetComponent<AudioSource>();
        started = false;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }


        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        FadeImg = GameObject.Find("Fade").GetComponent<Image>();
        InvokeRepeating("FadeToClear", 0.0f, 0.1f);

    }

    void Update()
    {

        if (!started)
        {
            started = true;
            StartCoroutine("waitThreeSeconds");
        }
    }

    public void FadeToClear()
    {
        //Bug: this gets called again whenever Level One is entered
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (FadeImg.color.a < 0.1f)
        {
            CancelInvoke("FadeToClear");
            FadeImg.color = Color.clear;
        }
    }

    void FadeToBlack()
    {
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
        if (FadeImg.color.a == 1.0f)
        {
            CancelInvoke("FadeToBlack");
        }
    }

    void updateLine()
    {
        StartCoroutine("waitThreeSeconds");
    }

    IEnumerator waitThreeSeconds()
    {
        yield return new WaitForSeconds(3.0f);
        theText.text = "Project Leads:\n\nNazely Hartoonian\n\nUlises Perez";
        yield return new WaitForSeconds(3.0f);
        theText.text = "Programmers:\n\nUlises Perez\n\nYixuan (Angela) Li";
        yield return new WaitForSeconds(3.0f);
        theText.text = "Artists:\n\nClaudia O'Flaherty\n\nYixuan (Angela) Li \n\nRachel Lee\n\nVictoria Barinova";
        yield return new WaitForSeconds(3.0f);
        theText.text = "Audio Designer:\n\nBrandon Delehoy";
        yield return new WaitForSeconds(3.0f);
        theText.text = "Writer:\n\nNazely Hartoonian";
        yield return new WaitForSeconds(3.0f);
        theText.text = "Some audio gathered from Freesound.org";
        yield return new WaitForSeconds(3.0f);
        theText.text = " ";
        FadeImg = GameObject.Find("Fade").GetComponent<Image>();
        InvokeRepeating("FadeToBlack", 0.0f, 0.1f);
        yield return new WaitForSeconds(3.0f);
        yield return new WaitForSeconds(3.0f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Main Menu");
    }
}