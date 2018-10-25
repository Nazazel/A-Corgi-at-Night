using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuQuinn : MonoBehaviour {

    //Testing
    public bool sceneTesting;
    public bool hintSystemDisable;

    //General
    public bool introRunning;
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
    
    public bool hintActive;

    // Use this for initialization
    void Start () {
        QuinnAS = gameObject.GetComponent<AudioSource>();
        Debug.Log(transform.rotation.y);
        originalParent = gameObject.transform.parent.gameObject;
        Debug.Log(originalParent.name);
        paused = false;
        isHeld = false;
        release = true;
        barkQ = 0;
        cooldown = false;
        //GameObject.DontDestroyOnLoad(GameObject.Find("Corgo"));
        attackBox = GameObject.Find("AttackBox");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        pa = gameObject.GetComponent<Animator>();
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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!paused)
        {
                if (hintActive == false)
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
                    if (canMove && !dead && !hidden)
                    {

                        if (Input.GetKeyDown(KeyCode.E) && dead == false && hidden == false && gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 && barking == false && running == false && crouching == false && jumping == false && paused == false)
                        {
                            Bark();
                        }

                        if (Input.GetKey(KeyCode.Space) && !crouching && !jumping)
                        {
                            idling = false;
                            StopCoroutine("IdleAnimate");
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
                                gameObject.transform.Rotate(Vector3.up, 180.0f);
                            }
                            if ((rb.velocity.x > 0.0f) && (rb.velocity.y != 0.0f))
                            {
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }

                            if (running)
                            {
                                if (!jumping)
                                {
                                    pa.Play("Run");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + (-1.0f * runSpeed), gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (crouching)
                            {
                                if (!jumping)
                                {
                                    pa.Play("CWalkR");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + (-1.0f * crouchSpeed), gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (!running && !crouching)
                            {
                                if (!jumping)
                                {
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
                                gameObject.transform.Rotate(Vector3.up, 180.0f);
                            }
                            if ((rb.velocity.x < 0.0f) && (rb.velocity.y != 0.0f))
                            {
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);

                            }

                            if (running)
                            {
                                if (!jumping)
                                {
                                    pa.Play("Run");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + runSpeed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (crouching)
                            {
                                if (!jumping)
                                {
                                    pa.Play("CWalkR");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + crouchSpeed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                            else if (!running && !crouching)
                            {
                                if (!jumping)
                                {
                                    pa.Play("Walk");
                                }
                                rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + speed, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            }
                        }
                        else if (!jumping && !barking && !crouching)
                        {
                            rb.velocity = new Vector2(gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x + 0.0f, gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y + rb.velocity.y);
                            idling = true;
                        }

                        if (idling && !hidden && !idlingtimerstarted && !isHeld)
                        {
                            idlingtimerstarted = true;
                            StartCoroutine("IdleAnimate");
                        }
                    }
                }
            }
        }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor") && jumping == true)
        {
            Debug.Log(gameObject.transform.position.y);
            jumping = false;
            landing = false;
            pa.Play("Stand");
        }
        else if (coll.gameObject.CompareTag("Doomba") && !dead)
        {
            jumping = false;
            landing = false;
            pa.Play("Stand");
            gameObject.transform.parent = coll.gameObject.transform;
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

    void OnCollisionExit2D(Collision2D colll)
    {
        if (colll.gameObject.CompareTag("Floor") && !jumping)
        {
            Debug.Log("Fall?");
            landing = true;
            jumping = true;
            pa.Play("Land");
        }
        gameObject.transform.parent = originalParent.transform;
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
}
