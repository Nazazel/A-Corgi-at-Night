﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameObject player;
    private GameObject savetext;
    private GameObject enemylist;
    private bool hasBeenSet;
	public GameObject controlsPage;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" && hasBeenSet == false)
        {
            savetext = GameObject.Find("SAVEFILE");
            savetext.SetActive(false);
            hasBeenSet = true;
        }
        hasBeenSet = false;
    }

    //void Update()
    //{
    //    if (SceneManager.GetActiveScene().name == "Main Menu" && hasBeenSet == false)
    //    {
    //        savetext = GameObject.Find("SAVEFILE");
    //        savetext.SetActive(false);
    //        hasBeenSet = true;
    //    }
    //}

    public void NewGameBtn(string newGameLevel)
    {
        if(PlayerPrefs.HasKey("PlayerPos"))
        {
            PlayerPrefs.DeleteAll();
        }
        SceneManager.LoadScene(newGameLevel);
    }

    public void load(string newGameLevel)
    {
        if (PlayerPrefs.HasKey("PlayerPos"))
        {
            SceneManager.LoadScene(newGameLevel);
        }
        else
        {
            StartCoroutine("NoSave");
        }
    }

    public void save()
    {
        player = GameObject.Find("QuinSpriteFinal_1");
		PlayerPrefsX.SetVector3("PlayerPos", new Vector3(player.GetComponent<Pibble>().spawnpoint.x, player.GetComponent<Pibble>().spawnpoint.y, player.GetComponent<Pibble>().spawnpoint.z));
        if (player.GetComponent<Pibble>().introRunning)
        {
            PlayerPrefsX.SetBool("IntroPlayed", true);
        }
        else
        {
            PlayerPrefsX.SetBool("IntroPlayed", false);
        }
        PlayerPrefsX.SetBool("HintDC", player.GetComponent<Pibble>().firstPatrol);
        PlayerPrefsX.SetBool("HintDCJ", player.GetComponent<Pibble>().firstDCJ);
        PlayerPrefsX.SetBool("HintNC", player.GetComponent<Pibble>().firstNinCat);
        PlayerPrefsX.SetBool("HintDoom", player.GetComponent<Pibble>().firstDoomba);
        PlayerPrefsX.SetBool("HintRov", player.GetComponent<Pibble>().firstRover);
        PlayerPrefsX.SetBool("HintKid", player.GetComponent<Pibble>().firstKid);
        PlayerPrefsX.SetBool("HintBirb", player.GetComponent<Pibble>().firstBirb);
        PlayerPrefsX.SetBool("HintUFO", player.GetComponent<Pibble>().firstUFO);
        enemylist = GameObject.Find("Enemies");
        foreach (Transform t in enemylist.transform)
        {
            Debug.Log(t.name);
            PlayerPrefs.SetString(t.name, t.name);
        }


    }

	public void controls()
	{
		controlsPage.SetActive (true);
	}

	public void controlsExit()
	{
		controlsPage.SetActive (false);
	}

    public void quitToMenu(string mainMenu)
    {
        Destroy(GameObject.Find("Test Corgo"));
        SceneManager.LoadScene(mainMenu);
    }

    public IEnumerator NoSave()
    {
        savetext.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        savetext.SetActive(false);
    }
}