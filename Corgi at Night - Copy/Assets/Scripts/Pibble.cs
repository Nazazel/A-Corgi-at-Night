﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pibble : MonoBehaviour {

    //Testing
    public bool sceneTesting;
    public bool hintSystemDisable;

    //General
    public bool gameOver;
	public bool introRunning;
    public bool outroRunning;
    public bool outroFinished;
    public bool paused;
    private bool idling;
    private bool idlingtimerstarted;
    public bool barking;
    public bool dead;
    public bool hidden;
    public bool hideable;
    private bool cooldown;
    private GameObject attackBox;
    public Vector3 spawnpoint;
    private GameObject originalParent;
    public bool isHeld;

    //Idle Animation List
    public string[] lookingAnim;
    public string[] sniffingAnim;

    //Camera
    public GameObject mainCam;

    //Audio Stuff
    private AudioListener mainListener;
    private AudioSource QuinnAS;
    private mutePls silencer;
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
	public bool falling;

    //Sprites
    private SpriteRenderer sr;
    private Animator pa;

    //Tutorial
    public bool firstPatrol;
    public bool firstDCJ;
    public bool firstNinCat;
    public bool firstDoomba;
    public bool firstRover;
    public bool firstKid;
    public bool firstBirb;
    public bool firstUFO;
    public bool firstLaser;
    public bool firstAsteroid;
    public bool firstWormhole;
    public bool release;
    public GameObject HintBox;
    public Text hintText;
    public bool hintActive;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(transform.rotation.y);
        if (sceneTesting)
        {
            introRunning = false;
        }
        else
        {
            if (PlayerPrefs.HasKey("IntroPlayed"))
            {
                introRunning = PlayerPrefsX.GetBool("IntroPlayed");
            }
            else
            {
                introRunning = true;
                StartCoroutine("intro");
            }
        }
        originalParent = gameObject.transform.parent.gameObject;
        Debug.Log(originalParent.name);
        paused = false;
        isHeld = false;
        if (!hintSystemDisable)
        {
            HintBox = GameObject.Find("HintBox");
            hintText = GameObject.Find("HintText").GetComponent<Text>();
            hintActive = false;
            HintBox.SetActive(false);
        }
        if (PlayerPrefs.HasKey("HintDC"))
        {
            firstDCJ = PlayerPrefsX.GetBool("HintDCJ");
            firstPatrol = PlayerPrefsX.GetBool("HintDC");
            firstNinCat = PlayerPrefsX.GetBool("HintNC");
            firstDoomba = PlayerPrefsX.GetBool("HintDoom");
            firstRover = PlayerPrefsX.GetBool("HintRov");
            firstKid = PlayerPrefsX.GetBool("HintKid");
            firstBirb = PlayerPrefsX.GetBool("HintBirb");
            firstUFO = PlayerPrefsX.GetBool("HintUFO");
            firstLaser = PlayerPrefsX.GetBool("HintLas");
            firstAsteroid = PlayerPrefsX.GetBool("HintAst");
            firstWormhole = PlayerPrefsX.GetBool("HintWorm");
        }
        else
        {
            firstPatrol = true;
            firstDCJ = true;
            firstNinCat = true;
            firstDoomba = true;
            firstRover = true;
            firstKid = true;
            firstBirb = true;
            firstUFO = true;
            firstLaser = true;
            firstAsteroid = true;
            firstWormhole = true;
        }
        release = true;
        barkQ = 0;
        cooldown = false;
		falling = false;
        silencer = GameObject.Find("MuteManager").GetComponent<mutePls>();
        //GameObject.DontDestroyOnLoad(GameObject.Find("Corgo"));
        mainCam = GameObject.Find("Main Camera");
        attackBox = GameObject.Find("AttackBox");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        pa = gameObject.GetComponent<Animator>();
        QuinnAS = gameObject.GetComponent<AudioSource>();
        lookingAnim = new string[2];
        lookingAnim[0] = "LookSR";
        lookingAnim[1] = "LookR";
        sniffingAnim = new string[2];
        sniffingAnim[0] = "SniffStand";
        sniffingAnim[1] = "SniffWalk";
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
        outroRunning = false;
        outroFinished = false;
        gameOver = false;
        if(PlayerPrefs.HasKey("PlayerPos"))
        {
            spawnpoint = PlayerPrefsX.GetVector3("PlayerPos");
            gameObject.transform.position = PlayerPrefsX.GetVector3("PlayerPos");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position.x);
        if (!paused && !gameOver)
        {
            if (introRunning)
            {
                mainCam.transform.position = new Vector3(-3.32f, -0.43f, -10.0f);
                if (gameObject.transform.position.x <= -3.33f)
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
            else if (outroRunning)
            {
                if (gameObject.transform.position.x > -1144.0f)
                {
                    pa.Play("Walk");
                    QuinnAS.clip = walkAudio;
                    if (!QuinnAS.isPlaying)
                    {
                        QuinnAS.Play();
                    }
                    rb.velocity = new Vector2(-1.5f, rb.velocity.y);
                }
                else
                {
                    //Debug.Log(gameObject.transform.position.x);
                    rb.velocity = new Vector2(0.0f, rb.velocity.y);
                    if (gameObject.transform.position.x != -1144.0f)
                    {
                        QuinnAS.Stop();
                        pa.Play("Stand");
                        gameObject.transform.position = new Vector3(-1144.0f, gameObject.transform.position.y, 0.0f);
                        StopCoroutine("IdleAnimate");
                        gameOver = true;
                    }
                    else
                    {
                        QuinnAS.Stop();
                        pa.Play("Stand");
                        StopCoroutine("IdleAnimate");
                        gameOver = true;
                    }
                }
            }
            else
            {
                if (hintActive == false)
                {
					if (!dead && !landing && (jumping || falling) && !hidden)
                    {
                        if (rb.velocity.y < -0.5f)
                        {
                            Debug.Log("Free Falling");
                            landing = true;
                            pa.Play("Land");
                        }
                    }
                    if (!dead)
                    {
                        if (hidden)
                        {
                            pa.Play("Hide");
                        }

                        if (Input.GetKeyDown(KeyCode.Q) && !crouching && !jumping && !hidden && hideable)
                        {
                            Debug.Log("Hidden");
                            hidden = true;
                            sr.sortingOrder = 28;
                            idling = false;
                            StopCoroutine("IdleAnimate");
                            idlingtimerstarted = false;
                            pa.Play("Hide");
                            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                            gameObject.GetComponent<PolygonCollider2D>().enabled = !gameObject.GetComponent<PolygonCollider2D>().enabled;
                            cooldown = true;
                            StartCoroutine("HideCool");
                        }
                        else if (Input.GetKeyDown(KeyCode.Q) && !crouching && hidden && !cooldown)
                        {
                            Debug.Log("!");
                            sr.sortingOrder = 31;
                            gameObject.GetComponent<PolygonCollider2D>().enabled = !gameObject.GetComponent<PolygonCollider2D>().enabled;
                            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                            hidden = false;
                        }
                    }
                    if (canMove && !dead && !hidden)
                    {

                        if (Input.GetKey(KeyCode.Space) && !crouching && !jumping && !falling)
                        {
                            QuinnAS.clip = jumpAudio;
                            idling = false;
                            StopCoroutine("IdleAnimate");
                            idlingtimerstarted = false;
                            rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 1.5f);
                            StartCoroutine("JumpAnimation");
                            jumping = true;
                        }

                        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftShift) && !jumping && !falling)
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
                                //mainListener.transform.Rotate(Vector3.up, 180.0f);
                            }
                            if ((rb.velocity.x > 0.0f) && (rb.velocity.y != 0.0f))
                            {
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }

                            if (running)
                            {
								if (!jumping && !falling)
                                {
                                    QuinnAS.clip = runAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("Run");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + (-1.0f * runSpeed), gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (crouching)
                            {
                                if (!jumping && !falling)
                                {
                                    QuinnAS.clip = runAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("CWalkR");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + (-1.0f * crouchSpeed), gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (!running && !crouching)
                            {
								if (!jumping && !falling)
                                {
                                    QuinnAS.clip = walkAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("Walk");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + (-1.0f * speed), gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
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
                                //mainListener.transform.Rotate(Vector3.up, 180.0f);
                            }
                            if ((rb.velocity.x < 0.0f) && (rb.velocity.y != 0.0f))
                            {
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);

                            }

                            if (running)
                            {
                                if (!jumping && !falling)
                                {
                                    QuinnAS.clip = runAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("Run");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + runSpeed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (crouching)
                            {
								if (!jumping && !falling)
                                {
                                    QuinnAS.clip = runAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("CWalkR");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + crouchSpeed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (!running && !crouching)
                            {
								if (!jumping && !falling)
                                {
                                    QuinnAS.clip = walkAudio;
                                    if (!QuinnAS.isPlaying)
                                    {
                                        QuinnAS.Play();
                                    }
                                    pa.Play("Walk");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + speed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                        }
                        else if (!jumping && !falling && !barking && !crouching)
                        {
                            rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            idling = true;
                        }

                        if (idling && !hidden && !idlingtimerstarted && !isHeld)
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
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor") && jumping == true)
        {
            pa.Play("Stand");
        }
        else if (coll.gameObject.CompareTag("Floor") && falling == true)
        {
            pa.Play("Stand");
        }
        else if (coll.gameObject.CompareTag("Doomba") && !dead)
        {
            pa.Play("Stand");
        }
        else if (coll.gameObject.CompareTag("Death") && !dead)
        {
            dead = true;
            StartCoroutine("Death");
        }
        else if (coll.gameObject.CompareTag("NinjaCat") && !dead)
        {
            dead = true;
            StartCoroutine("Death");
        }
        else if (coll.gameObject.CompareTag("DogCatcherJR") && !dead)
        {
            dead = true;
            StartCoroutine("Death");
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
		if (coll.collider.CompareTag("Floor") && jumping == true)
		{
			//Debug.Log(gameObject.transform.position.y);
			falling = false;
			jumping = false;
			landing = false;
			//pa.Play("Stand");
		}
		else if (coll.collider.CompareTag("Floor") && falling == true)
		{
			//Debug.Log(gameObject.transform.position.y);
			falling = false;
			jumping = false;
			landing = false;
			//pa.Play("Stand");
		}
		else if (coll.gameObject.CompareTag("Doomba") && !dead)
		{
			falling = false;
			jumping = false;
			landing = false;
			//pa.Play("Stand");
			gameObject.transform.parent = coll.gameObject.transform;
		}
    }

//  CODE FOR FALLING OFF PLATFORMS: DOESN'T WORK RIGHT NOW, BUT POSSIBLE
//	JK, WORKS NOW (FUCK COLLIDERS...)
void OnCollisionExit2D(Collision2D colll)
    {
		if (colll.gameObject.CompareTag("Floor") && jumping)
		{
			falling = true;
			//pa.Play("Jump");
		}
		else if (colll.gameObject.CompareTag("Floor") && !falling && !jumping)
        {
            Debug.Log("Fall?");
            falling = true;
            //pa.Play("Land");
        }

        gameObject.transform.parent = originalParent.transform;
    }

    public IEnumerator Death()
    {
        gameObject.transform.parent = originalParent.transform;
        QuinnAS.clip = deathAudio;
        QuinnAS.Play();
        yield return new WaitForSeconds(0.05f);
        pa.Play("DieR");
        StopCoroutine("IdleAnimate");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(2.93f);
        if(isHeld)
        {
            isHeld = false;
            canMove = true;
        }
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.position = spawnpoint;
        dead = false;
        landing = false;
    }

    public void StompBoop()
    {
        QuinnAS.clip = stompAudio;
        QuinnAS.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        pa.Play("Jump");
    }

    public void Caught()
    {
        dead = true;
        StartCoroutine("Death");
    }

    public void Bark()
    {
        StartCoroutine("Borking");
    }

    public IEnumerator Borking()
    {
        if (barkQ == 0)
        {
            QuinnAS.clip = barkAudio1;
        }
        else if (barkQ == 1)
        {
            QuinnAS.clip = barkAudio2;
        }
        else if (barkQ == 2)
        {
            QuinnAS.clip = barkAudio3;
        }
        idling = false;
        QuinnAS.Play();
        StopCoroutine("IdleAnimate");
        idlingtimerstarted = false;
        barking = true;
        pa.Play("BorkR");
        yield return new WaitForSeconds(0.15f);
        if (barkQ < 2)
        {
            barkQ += 1;
        }
        else
        {
            barkQ = 0;
        }
        barking = false;
    }

    public IEnumerator IdleAnimate()
    {
        pa.Play("Stand");
        yield return new WaitForSeconds(5.0f);
        pa.Play(lookingAnim[Random.Range(0, 2)]);
        yield return new WaitForSeconds(2.15f);
        pa.Play("Stand");
        yield return new WaitForSeconds(5.0f);
        QuinnAS.clip = sniffAudio;
        QuinnAS.Play();
        pa.Play("SniffStand");
        yield return new WaitForSeconds(2.15f);
        pa.Play("Stand");
        yield return new WaitForSeconds(5.0f);
        pa.Play(lookingAnim[Random.Range(0, 2)]);
        yield return new WaitForSeconds(2.15f);
        pa.Play("Stand");
        yield return new WaitForSeconds(5.0f);
        pa.Play("SitR");
    }

    public IEnumerator HideCool()
    {
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }

    public void firstPatrolCatcher()
    {
        firstPatrol = false;
        release = false;
        StartCoroutine("patrolCatcherDiag");
    }

    public void firstDCJs()
    {
        firstDCJ = false;
        release = false;
        StartCoroutine("dcjDiag");
    }

    public void firstNinCats()
    {
        firstNinCat = false;
        release = false;
        StartCoroutine("ninCatDiag");
    }

    public void firstBirbs()
    {
        firstBirb = false;
        release = false;
        StartCoroutine("birbDiag");
    }

    public void firstUFOs()
    {
        firstUFO = false;
        release = false;
        StartCoroutine("UFODiag");
    }

    public void firstRovs()
    {
        firstRover = false;
        release = false;
        StartCoroutine("RovDiag");
    }

    public void firstDooms()
    {
        firstDoomba = false;
        release = false;
        StartCoroutine("DoomDiag");
    }

    public void firstKids()
    {
        firstKid = false;
        release = false;
        StartCoroutine("KidDiag");
    }

    public void firstLasers()
    {
        firstLaser = false;
        release = false;
        StartCoroutine("LasDiag");
    }

    public void firstAsteroids()
    {
        firstAsteroid = false;
        release = false;
        StartCoroutine("AstDiag");
    }

    public void firstWormholes()
    {
        firstWormhole = false;
        release = false;
        StartCoroutine("WormDiag");
    }

    public IEnumerator patrolCatcherDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Better watch out for the dog catchers roaming around the streets! Avoid their flashlight by hiding in nearby shrubs or cardboard boxes by pressing the \"Q\" key when close to the item of interest.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator dcjDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Looks like dog catching runs in the family. Jump on top of the jr. dog catchers to avoid losing!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator ninCatDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Uh-oh, looks like a cat! Not just any cat-- a ninja cat! Quick, use the \"E\" key to bark and scare him away!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator birbDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "That pigeon just threw an egg that exploded. Try to avoid getting hit if you don't wanna get scrambled.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator UFODiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "These aliens are out to get cute, stubby corgis! Watch out for their beams if you don't wanna get abducted!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator KidDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Oh boy, this little dude sure likes Quinn and his grip sure is strong for a kid!\n" +
        "To free Quinn of the kid's grasp, repeatedly tap the left and right arrow keys!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator DoomDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Is that a...knife wielding roomba? Well, I think you've seen it all.\n" + 
        "Don't approach these nasty roombas head on, try using them as platforms instead! \nUnless they come strapped with bombs or chainsaws!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator RovDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "That doesn't look like a very friendly rover. Show him who's boss by stomping on him not once, but TWICE!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator LasDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Hm, I don't think lasers like cute, fluffy dogs..or is it the other way around?\n" + 
        "Avoid lasers to prevent Quinn from getting hurt!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator AstDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Dodge asteroids to avoid getting injured!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator WormDiag()
    {
        silencer.SetAudioMute(true);
        pa.enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        hintActive = true;
        HintBox.SetActive(true);
        hintText.text = "Woah, a vortex! Where does it lead? Find out by pressing the \"R\" key.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.5f);
        silencer.SetAudioMute(false);
        pa.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        hintActive = false;
        HintBox.SetActive(false);
    }

    public IEnumerator intro()
    {
        yield return new WaitForSeconds(3.5f);
        pa.Play("LookR");
        yield return new WaitForSeconds(3.0f);
        pa.Play("SniffStand");
        QuinnAS.clip = sniffAudio;
        QuinnAS.Play();
        yield return new WaitForSeconds(3.0f);
        pa.Play("Stand");
        HintBox.SetActive(true);
        hintText.text = "Oh no! Poor little pup! It seems he has lost his way!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        hintText.text = "His owners called for him to stay, but the corgi didn't have it in him to ignore the deliciously cheesy scent of a freshly made pizza!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        hintText.text = "Mamma Mia! Help Quinn find his way back home to his owners.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        hintText.text = "If you at any point want to save your progress, take a look at the controls, or exit to the main menu, all these can be done via the pause menu (triggered by the ESC key).";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        HintBox.SetActive(false);
        introRunning = false;
    }

    public IEnumerator JumpAnimation()
    {
        Debug.Log("JUMP");
        yield return new WaitForSeconds(0.01f);
		if (QuinnAS.clip == jumpAudio && QuinnAS.isPlaying == false) {
			QuinnAS.Play ();
		}
        pa.Play("Jump");
    }

    public IEnumerator End()
    {
        pa.Play("Hide");
        yield return new WaitForSeconds(0.1f);
    }

}


