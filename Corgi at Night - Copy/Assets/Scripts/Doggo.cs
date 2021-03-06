﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doggo : MonoBehaviour {

	//General
	private bool idling;
	private bool idlingtimerstarted;
	public bool barking;
	public bool dead;
	public bool hidden;
	public bool hideable;
	private bool cooldown;
	private GameObject attackBox;
	public Vector3 spawnpoint;
    public bool introRunning;

	//Idle Animation List
	public string[] lookingAnim;
	public string[] sniffingAnim;

	//Camera
	public GameObject mainCam;

	//Audio Stuff
	private GameObject mainListener;
	private AudioSource QuinnAS;
	private int barkQ;
	public AudioClip barkAudio1;
	public AudioClip barkAudio2;
	public AudioClip barkAudio3;
	public AudioClip jumpAudio;
	public AudioClip deathAudio;
	public AudioClip sniffAudio;
	public AudioClip stompAudio;
	public AudioClip walkAudio;
	public AudioClip runAudio;

	//Movement
	public bool canMove;
	private Rigidbody2D rb;
	private float runSpeed;
	private float speed;
	private float crouchSpeed;
	private float jumpHeight;
	public bool running;
	public bool crouching;
	public bool jumping;
	private bool right;
	private bool landing;

	//Sprites
	private SpriteRenderer sr;
	private Animator pa;

	//Tutorial
	public bool firstPatrol;
	public bool firstDCJ;
	public bool firstNinCat;
	public bool release;
	public GameObject HintBox;
	public Text hintText;
	public bool hintActive;

	// Use this for initialization
	void Start () {
        //Debug.Log(gameObject.transform.position);
        gameObject.transform.position = new Vector3(-6.32f, -0.8f, 0.0f);
        HintBox = GameObject.Find("HintBox");
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        introRunning = true;
		hintActive = false;
		HintBox.SetActive (false);
		firstPatrol = true;
		firstDCJ = true;
		firstNinCat = true;
		release = true;
		barkQ = 0;
		cooldown = false;
		mainListener = GameObject.Find ("AudioListener");
		GameObject.DontDestroyOnLoad (GameObject.Find("Corgo"));
		mainCam = GameObject.Find ("Main Camera");
        mainCam.transform.position = new Vector3(-3.32f, -0.43f, -10.0f);
        Debug.Log(mainCam.transform.position.x);
        attackBox = GameObject.Find ("AttackBox");
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		pa = gameObject.GetComponent<Animator> ();
		QuinnAS = gameObject.GetComponent<AudioSource> ();
		lookingAnim = new string[2];
		lookingAnim [0] = "LookSR";
		lookingAnim [1] = "LookR";
		sniffingAnim = new string[2];
		sniffingAnim [0] = "SniffStand";
		sniffingAnim [1] = "SniffWalk";
		speed = 2.0f;
		runSpeed = 4.0f;
		crouchSpeed = 1.0f;
		jumpHeight = 3.0f;
		canMove = true;
		crouching = false;
		barking = false;
		running = false;
		jumping = false;
		landing = false;
		idling = false;
		idlingtimerstarted = false;
		dead = false;
		hidden = false;
		right = true;
        StartCoroutine("intro");
	}

	// Update is called once per frame
	void Update () {
        if (introRunning)
        {
            mainCam.transform.position = new Vector3(-3.32f,-0.43f,-10.0f);
            if(gameObject.transform.position.x <= -3.33f)
            {
                pa.Play("Walk");
                QuinnAS.clip = walkAudio;
                if (!QuinnAS.isPlaying)
                {
                    QuinnAS.Play();
                }
                rb.velocity = new Vector2(1.5f, rb.velocity.y);
            }
            else
            {
                //Debug.Log(gameObject.transform.position.x);
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
                if (gameObject.transform.position.x != -3.32f)
                {
                    QuinnAS.Stop();
                    pa.Play("Stand");
                    gameObject.transform.position = new Vector3(-3.32f, -0.787f, 0.0f);
                }
            }
        }
        else
        {
            if (!dead && !landing && jumping && !hidden)
            {
                if (rb.velocity.y < -0.5f)
                {
                    Debug.Log("Free Falling");
                    landing = true;
                    pa.Play("Land");
                }
            }
            if (canMove && !dead)
            {
                if (hidden)
                {
                    pa.Play("Hide");
                }

                if (Input.GetKeyDown(KeyCode.H) && !crouching && !jumping && !hidden && hideable)
                {
                    Debug.Log("Hidden");
                    hidden = true;
                    sr.sortingOrder = 24;
                    idling = false;
                    StopCoroutine("IdleAnimate");
                    idlingtimerstarted = false;
                    pa.Play("Hide");
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = !gameObject.GetComponent<PolygonCollider2D>().enabled;
                    cooldown = true;
                    StartCoroutine("HideCool");
                }
                else if (Input.GetKeyDown(KeyCode.H) && !crouching && hidden && !cooldown)
                {
                    Debug.Log("!");
                    sr.sortingOrder = 30;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = !gameObject.GetComponent<PolygonCollider2D>().enabled;
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    hidden = false;
                }
            }
            if (canMove && !dead && !hidden)
            {

                if (Input.GetKey(KeyCode.Space) && !crouching && !jumping)
                {
                    QuinnAS.clip = jumpAudio;
                    idling = false;
                    StopCoroutine("IdleAnimate");
                    QuinnAS.Play();
                    idlingtimerstarted = false;
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 1.5f);
                    pa.Play("Jump");
                    jumping = true;
                }

                if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftShift) && !jumping)
                {
                    idling = false;
                    StopCoroutine("IdleAnimate");
                    idlingtimerstarted = false;
                    crouching = true;
                    if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    {
                        pa.Play("Crouch");
                    }
                }
                else
                {
                    crouching = false;
                }

                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.DownArrow))
                {
                    running = true;
                }
                else
                {
                    running = false;
                }

                if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    idling = false;
                    StopCoroutine("IdleAnimate");
                    idlingtimerstarted = false;
                    //attackBox.transform.localPosition = new Vector (0.0f, 0.007f, attackBox.transform.position.z);
                    if (gameObject.transform.rotation.y == 0)
                    {
                        mainCam.transform.localPosition = new Vector3(0.0f, 0.36f, 10.0f);
                        mainCam.transform.Rotate(Vector3.up, 180.0f);
                        gameObject.transform.Rotate(Vector3.up, 180.0f);
                        mainListener.transform.Rotate(Vector3.up, 180.0f);
                    }
                    if ((rb.velocity.x > 0.0f) && (rb.velocity.y != 0.0f))
                    {
                        rb.velocity = new Vector2(0.0f, rb.velocity.y);
                    }

                    if (running)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = runAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("Run");
                        }
                        rb.velocity = new Vector2(-1.0f * runSpeed, rb.velocity.y);
                    }
                    else if (crouching)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = runAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("CWalkR");
                        }
                        rb.velocity = new Vector2(-1.0f * crouchSpeed, rb.velocity.y);
                    }
                    else if (!running && !crouching)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = walkAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("Walk");
                        }
                        rb.velocity = new Vector2(-1.0f * speed, rb.velocity.y);
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                {
                    idling = false;
                    StopCoroutine("IdleAnimate");
                    idlingtimerstarted = false;
                    //attackBox.transform.localPosition = new Vector3 (0.984f, 0.007f, attackBox.transform.position.z);
                    if (gameObject.transform.rotation.y != 0)
                    {
                        mainCam.transform.localPosition = new Vector3(0.0f, 0.36f, -10.0f);
                        mainCam.transform.Rotate(Vector3.up, 180.0f);
                        gameObject.transform.Rotate(Vector3.up, 180.0f);
                        mainListener.transform.Rotate(Vector3.up, 180.0f);
                    }
                    if ((rb.velocity.x < 0.0f) && (rb.velocity.y != 0.0f))
                    {
                        rb.velocity = new Vector2(0.0f, rb.velocity.y);

                    }

                    if (running)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = runAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("Run");
                        }
                        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                    }
                    else if (crouching)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = runAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("CWalkR");
                        }
                        rb.velocity = new Vector2(crouchSpeed, rb.velocity.y);
                    }
                    else if (!running && !crouching)
                    {
                        if (!jumping)
                        {
                            QuinnAS.clip = walkAudio;
                            if (!QuinnAS.isPlaying)
                            {
                                QuinnAS.Play();
                            }
                            pa.Play("Walk");
                        }
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
                else if (!jumping && !barking && !crouching)
                {
                    rb.velocity = new Vector2(0.0f, rb.velocity.y);
                    idling = true;
                }

                if (idling && !hidden && !idlingtimerstarted)
                {
                    if (QuinnAS.isPlaying && (QuinnAS.clip == walkAudio || QuinnAS.clip == runAudio))
                    {
                        QuinnAS.Stop();
                    }
                    idlingtimerstarted = true;
                    StartCoroutine("IdleAnimate");
                }
            }
        }
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Floor") && jumping == true) {
			jumping = false;
			landing = false;
			pa.Play ("Stand");
		}
		else if (coll.gameObject.CompareTag ("Death") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
		else if (coll.gameObject.CompareTag ("NinjaCat") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
		else if (coll.gameObject.CompareTag ("DogCatcherJR") && !dead) {
			dead = true;
			StartCoroutine("Death");
		}
	}

//  CODE FOR FALLING OFF PLATFORMS: DOESN'T WORK RIGHT NOW, BUT POSSIBLE
//	JK, WORKS NOW (FUCK COLLIDERS...)
	void OnCollisionExit2D(Collision2D colll)
	{
		if (colll.gameObject.CompareTag ("Floor") && !jumping) {
			Debug.Log ("Fall?");
			landing = true;
			jumping = true;
			pa.Play ("Land");
		}
	}

	public IEnumerator Death()
	{
		QuinnAS.clip = deathAudio;
		QuinnAS.Play ();
		pa.Play("DieR");
		StopCoroutine ("IdleAnimate");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3.0f);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.transform.position = spawnpoint;
		dead = false;
        canMove = true;
		landing = false;
	}

	public void StompBoop()
	{
		QuinnAS.clip = stompAudio;
		QuinnAS.Play ();
		rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		pa.Play ("Jump");
	}

	public void Caught()
	{
		dead = true;
		StartCoroutine("Death");
	}

	public void Bark()
	{
		StartCoroutine ("Borking");
	}

	public IEnumerator Borking()
	{
		if (barkQ == 0) {
			QuinnAS.clip = barkAudio1;
		} 
		else if (barkQ == 1) {
			QuinnAS.clip = barkAudio2;
		} 
		else if (barkQ == 2) {
			QuinnAS.clip = barkAudio3;
		}
		idling = false;
		QuinnAS.Play ();
		StopCoroutine ("IdleAnimate");
		idlingtimerstarted = false;
		barking = true;
		pa.Play ("BorkR");
		yield return new WaitForSeconds (0.15f);
		if (barkQ < 2) {
			barkQ += 1;
		} 
		else {
			barkQ = 0;
		}
		barking = false;
	}

	public IEnumerator IdleAnimate()
	{
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play (lookingAnim [Random.Range (0, 2)]);
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		QuinnAS.clip = sniffAudio;
		QuinnAS.Play ();
		pa.Play ("SniffStand");
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play (lookingAnim [Random.Range (0, 2)]);
		yield return new WaitForSeconds (2.15f);
		pa.Play("Stand");
		yield return new WaitForSeconds (5.0f);
		pa.Play ("SitR");
	}

	public IEnumerator HideCool()
	{
		yield return new WaitForSeconds (0.1f);
		cooldown = false;
	}

	public void firstPatrolCatcher()
	{
		firstPatrol = false;
		release = false;
		StartCoroutine ("patrolCatcherDiag");
	}

	public void firstDCJs()
	{
		firstDCJ = false;
		release = false;
		StartCoroutine ("dcjDiag");
	}

	public void firstNinCats()
	{
		firstNinCat = false;
		release = false;
		StartCoroutine ("ninCatDiag");
	}

	public IEnumerator patrolCatcherDiag()
	{
		HintBox.SetActive (true);
		hintText.text = "Better watch out for the dog catchers roaming around the streets! Avoid their flashlight by hiding in nearby shrubs or cardboard boxes by pressing the \"Q\" key when close to the item of interest.";
		yield return new WaitUntil (() => Input.GetKeyDown (KeyCode.Return));
		yield return new WaitForSeconds (0.5f);
		HintBox.SetActive (false);
	}

	public IEnumerator dcjDiag()
	{
		HintBox.SetActive (true);
		hintText.text = "Looks like dog catching runs in the family. Jump on top of the jr. dog catchers to avoid losing!";
		yield return new WaitUntil (() => Input.GetKeyDown (KeyCode.Return));
		yield return new WaitForSeconds (0.5f);
		HintBox.SetActive (false);
	}

	public IEnumerator ninCatDiag()
	{
		HintBox.SetActive (true);
		hintText.text = "Uh-oh, looks like a cat! Not just any cat-- a ninja cat! Quick, use the \"E\" key to bark and scare him away!";
		yield return new WaitUntil (() => Input.GetKeyDown (KeyCode.Return));
		yield return new WaitForSeconds (0.5f);
		HintBox.SetActive (false);
	}

    public IEnumerator intro()
    {
        yield return new WaitForSeconds(3.0f);
        pa.Play("LookR");
        yield return new WaitForSeconds(4.0f);
        pa.Play("SniffStand");
        QuinnAS.clip = sniffAudio;
        QuinnAS.Play();
        yield return new WaitForSeconds(3.0f);
        pa.Play("Stand");
        HintBox.SetActive(true);
        hintText.text = "Looks like this pupper lost his way! Help Quinn find his way home!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        HintBox.SetActive(false);
        introRunning = false;
    }
}


