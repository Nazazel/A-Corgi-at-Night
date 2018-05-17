using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationController : MonoBehaviour {

    public CatJumpingForLife cat;
    public QuinnChase quinn_kid;
    public QuinnChase quinn_cat;
    public KidMenuAnimation kid;
    public MenuAnimation quinn;
    public GameObject ControllableQuinn;
    public GameObject Animation1;
    public GameObject Animation2;
    public GameObject Animation3;
    public GameObject ScreenBoundary;

	// Use this for initialization
	void Start () {
        StartCoroutine("MenuSequence");
        StartCoroutine("CheatSequence");
	}

    public IEnumerator CheatSequence()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.B));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));
        yield return new WaitForSeconds(0.01f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.01f);
        StopCoroutine("MenuSequence");
        Animation1.SetActive(false);
        Animation2.SetActive(false);
        Animation3.SetActive(false);
        ControllableQuinn.SetActive(true);
        ScreenBoundary.SetActive(true);

    }

    public IEnumerator MenuSequence()
    {
        yield return new WaitForSeconds(3.0f);
        quinn.AnimatingTime();
        yield return new WaitForSeconds(23.0f);
        kid.AnimatingTime();
        quinn_kid.AnimatingTime();
        yield return new WaitForSeconds(13.0f);
        cat.AnimatingTime();
        quinn_cat.AnimatingTime();
        yield return new WaitForSeconds(13.0f);
    }
}
