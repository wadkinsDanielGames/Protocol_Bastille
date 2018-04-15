using UnityEngine;
using System.Collections;
using System;

public class AICopScriptLR : MonoBehaviour {
    public Vector3 velocity;
    public Vector3 startLocation;
    public bool caught = false;
    public int startLocationX;
    public int endLocationX;
    public int startLocationY;
    public int endLocationY;
    public int tempEndLocationX;
    public int tempEndLocationY;
    private BoxCollider2D fireZone;

    //down x=-0.173------y=-.464
    //up   x=-0.078------y=0.524


    private Animator anim;
    public static event Action<bool> isColliding;
    public GameObject gunshot;
	// On the start, the ending location is stored and the start location is stored.
	void Start () {
        //SetVelocity();
        tempEndLocationX = endLocationX;
        fireZone = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        startLocation = transform.position;
        //Walking.returnMovementAI += continueWalking;

    }

    // Update is called once per frame
    void Update()
    {
        //if the AI has not interacted with the player, the character will move back and forth from left to right. Also handles animation.
        if (!caught)
        {
            if (transform.position.x < endLocationX)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                fireZone.offset = new Vector2(2.5f, 0);
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
                gunshot.transform.position = transform.position + new Vector3(0.557f, -0.202f, 0f);

                return;


            }
            if (transform.position.x > endLocationX)
            {
                endLocationX = startLocationX;
                if (transform.position.x > endLocationX)
                {
                    transform.Translate(Time.deltaTime * -velocity.x, Time.deltaTime * velocity.y, 0);
                    fireZone.offset = new Vector2(-2.5f, 0);

                    anim.SetBool("Left", true);
                    anim.SetBool("Right", false);
                    gunshot.transform.position = transform.position + new Vector3(-0.557f, -0.202f, 0f);

                    if (transform.position.x < endLocationX)
                    {
                        endLocationX = tempEndLocationX;
                    }
                }
            }
        }
    }

   
    //If the player enters the field of view, the player is considered caught.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Anthony" || other.gameObject.name == "Naomi")
        {
            if (isColliding != null)
            {
                isColliding(true);
            }
            caught = true;
            StartCoroutine(Fire(transform));
        }
    }
    //This will revert the location of the police officers back to their initial starting point (upon loading the characters last checkpoint)
    public void continueWalking(bool continuation)
    {
        caught = false;
        endLocationX = tempEndLocationX;
        endLocationY = tempEndLocationY;
        transform.position = startLocation;
        //print("hi");
    }

    public IEnumerator Fire(Transform checkpoint)
    {
        gunshot.SetActive(true);
        yield return new WaitForSeconds(.1f);
        gunshot.SetActive(false);
        yield return new WaitForSeconds(.1f);
        gunshot.SetActive(true);
        yield return new WaitForSeconds(.1f);
        gunshot.SetActive(false);
    }
    void OnEnable()
    {
        Player.returnMovementAI += continueWalking;

    }
    void OnDisable()
    {
        Player.returnMovementAI -= continueWalking;

    }
}