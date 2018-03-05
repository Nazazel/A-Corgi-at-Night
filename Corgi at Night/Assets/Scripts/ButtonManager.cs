using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameObject player;
    private GameObject savetext;

    void Start()
    {
        savetext = GameObject.Find("SAVEFILE");
        savetext.SetActive(false);
    }

    public void NewGameBtn(string newGameLevel)
    {
        if(PlayerPrefs.HasKey("PlayerX"))
        {
            PlayerPrefs.DeleteKey("PlayerX");
            PlayerPrefs.DeleteKey("PlayerY");
            PlayerPrefs.DeleteKey("PlayerZ");
            //PlayerPrefs.DeleteKey("IntroPlayed");
        }
        SceneManager.LoadScene(newGameLevel);
    }

    public void load(string newGameLevel)
    {
        if (PlayerPrefs.HasKey("PlayerX"))
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
        if(player.GetComponent<Pibble>().introRunning)
        {
            PlayerPrefsX.SetBool("IntroPlayed", false);
        }
        else
        {
            PlayerPrefsX.SetBool("IntroPlayed", true);
        }

        
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