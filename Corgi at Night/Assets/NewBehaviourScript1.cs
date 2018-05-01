using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript1 : MonoBehaviour
{
    public GameObject title;
    // Use this for initialization
    void Start()
    {
        Image renderer = title.GetComponent<Image>();
        Color newColor = renderer.material.color;
        newColor.a = 0;
        renderer.material.color = newColor;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.0f);
        Image renderer = title.GetComponent<Image>();
        Color newColor = renderer.material.color;
        for (float f = 0f; f < 1.1f; f += 0.1f)
        {
            newColor.a = f;
            renderer.material.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        Image renderer = title.GetComponent<Image>();
        Color newColor = renderer.material.color;
        for (float f = 1f; f > -0.1f; f -= 0.1f)
        {
            newColor.a = f;
            renderer.material.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        PlayerPrefs.DeleteAll();
        yield return new WaitForSeconds(2.0f);
        Application.Quit();
    }
}