using UnityEngine;
using System.Collections;
using System;

public class AICopScriptUD : MonoBehaviour
{
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



    private Animator anim;
    public static event Action<bool> isCollidingUD;
    public GameObject gunshot;
    // On the start, the ending location is stored and the start location is stored.
    void Start()
    {
        //SetVelocity();
        tempEndLocationY = endLocationY;
        fireZone = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        startLocation = transform.position;
        //Walking.returnMovementAI += continueWalking;

    }

    // Update is called once per frame
    void Update()
    {
        if (!caught) { 
        //if the AI has not interacted with the player, the character will move up and down. Also handles animation.

        if (transform.position.y < endLocationY)
        {
            transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
            fireZone.offset = new Vector2(0f, 2.5f);
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            gunshot.transform.position = transform.position + new Vector3(-0.078f, 0.524f, 0f);

                return;
        }

        if (transform.position.y > endLocationY)
        {
            endLocationY = startLocationY;
            if (transform.position.y > endLocationY)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * -velocity.y, 0);
                fireZone.offset = new Vector2(0f, -2.5f);

                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                gunshot.transform.position = transform.position + new Vector3(-0.173f, -0.464f, 0f);

                    if (transform.position.y < endLocationY)
                {
                    endLocationY = tempEndLocationY;
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
            if (isCollidingUD != null)
            {
                isCollidingUD(true);
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