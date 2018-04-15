using UnityEngine;
using System.Collections;
using System;

public class AIMenuCops : MonoBehaviour
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

    //down x=-0.173------y=-.464
    //up   x=-0.078------y=0.524


    private Animator anim;
    public static event Action<bool> isColliding;
    public GameObject gunshot;
    // On the start, the ending location is stored and the start location is stored.
    void Start()
    {
        //SetVelocity();
        tempEndLocationX = endLocationX;
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
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
                return;
            }
            if (transform.position.x > endLocationX - 1)
            {
                endLocationX = startLocationX;
                if (transform.position.x > endLocationX)
                {
                    transform.Translate(Time.deltaTime * -velocity.x, Time.deltaTime * velocity.y, 0);

                    anim.SetBool("Left", true);
                    anim.SetBool("Right", false);

                    if (transform.position.x < endLocationX)
                    {
                        endLocationX = tempEndLocationX;
                    }
                }
            }

        }
        //After the player has been caught and the death animation is done playing, this continues the AI's walking.

    }

}