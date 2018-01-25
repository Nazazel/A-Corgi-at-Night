using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    private GameObject fade;
    public GameObject teleportExit;
    private bool teleportable;
    private GameObject player;
    private bool cooldown;


    // Use this for initialization
    void Start()
    {
        cooldown = false;
        fade = GameObject.Find("Transition");
        player = GameObject.Find("QuinSpriteFinal_1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !player.GetComponent<Doggo>().jumping && !player.GetComponent<Doggo>().barking && !player.GetComponent<Doggo>().crouching && teleportable)
        {
            StartCoroutine("fadeScreen");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && !cooldown)
        {
            teleportable = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            teleportable = false;
        }
    }

    public IEnumerator fadeScreen()
    {
        fade.GetComponent<TransitionScreenGrp>().FadeDiag();
        yield return new WaitForSeconds(1.0f);
        player.transform.position = new Vector3(teleportExit.transform.position.x, teleportExit.transform.position.y + 0.1f, teleportExit.transform.position.z);
        yield return new WaitForSeconds(3.0f);
    }
}
