using UnityEngine;
using System.Collections;

public class NPCWalk : MonoBehaviour {
    public Vector3 velocity;
    public Vector3 startLocation;
    public bool talking = false;
    public int startLocationX;
    public int endLocationX;
    public int startLocationY;
    public int endLocationY;
    public int tempEndLocationX;
    public int tempEndLocationY;
    public bool standStill = false;
    public bool transition = false;
    public bool speaking = false;
    //down x=-0.173------y=-.464
    //up   x=-0.078------y=0.524


    private Animator anim;
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
        StartCoroutine(pause(transform));

        if (standStill == true)
        {
            anim.SetBool("IdleRight", true);
            anim.SetBool("IdleLeft", true);
        }
        //if the AI has not interacted with the player, the character will move back and forth from left to right. Also handles animation.
        if (!talking)
        {
            anim.SetBool("IdleRight", false);
            anim.SetBool("IdleLeft", false);

            if (transform.position.x < endLocationX)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);

                return;
                

            }
            if (transform.position.x > endLocationX)
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
    }


    //This will revert the location of the police officers back to their initial starting point (upon loading the characters last checkpoint)
    public void continueWalking()
    {
        talking = false;
        speaking = false;
        //endLocationX = tempEndLocationX;
        //endLocationY = tempEndLocationY;
        //transform.position = startLocation;
        //print("hi");
    }
    void stopMovement()
    {
        talking = true;
        standStill = true;
        speaking = true;
    }
    public IEnumerator pause(Transform t)
    {
        if (transition == false && speaking == false)
        {
            transition = true;
            yield return new WaitForSeconds(1f);
            talking = true;
            standStill = true;
            yield return new WaitForSeconds(3f);
            if (speaking != true) {
                talking = false;
                standStill = false;
                //transition = false;
            }
            //talking = false;
            //standStill = false;
            transition = false;
        }
    }
    void OnEnable()
    {
        Dialogue.returnNPCMovement += continueWalking;
        DialogueInteraction.stopMovementNPC += stopMovement;
    }
    void OnDisable()
    {
        Dialogue.returnNPCMovement -= continueWalking;
        DialogueInteraction.stopMovementNPC -= stopMovement;

    }
}
