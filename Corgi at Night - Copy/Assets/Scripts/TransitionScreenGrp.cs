using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenGrp : MonoBehaviour {

    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;

    private bool startTransition;
    public bool screenUp;
    private float transitionSpeed;
    private GameObject player;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("QuinSpriteFinal_1");
        startTransition = false;
        screenUp = true;
        transitionSpeed = -12.0f;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (screenUp)
        {
            if (startTransition && screen4.transform.position.y > -12.0f)
            {
                screen1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, transitionSpeed);
                screen2.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, transitionSpeed);
                screen3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, transitionSpeed);
                screen4.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, transitionSpeed);
            }
            else if (startTransition && screen4.transform.position.y <= -12.0f)
            {
                screen1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen2.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen4.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                startTransition = false;
                gameObject.SetActive(false);
                screenUp = false;
            }
        }
        else
        {
            if (startTransition && screen1.transform.position.y < 7.8f)
            {
                screen1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -transitionSpeed);
                screen2.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -transitionSpeed);
                screen3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -transitionSpeed);
                screen4.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -transitionSpeed);
            }
            else if (startTransition && screen4.transform.position.y >= 7.8f)
            {
                screen1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen2.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                screen4.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                startTransition = false;
                gameObject.SetActive(false);
                screenUp = true;
            }
        }
    }

    public void FadeDiag()
    {
        startTransition = true;
    }
}
