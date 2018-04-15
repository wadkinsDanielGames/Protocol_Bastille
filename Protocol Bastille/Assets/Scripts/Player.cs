using UnityEngine;
using System.Collections;
using System;
public class Player : MonoBehaviour {
    Vector2 input;
    bool isMoving = false;
    Vector2 lastMove;
    Vector3 checkpointPosition;
    float t;
    public bool isInteracting = false;
    private float speed;
    private Animator anim;
    private Rigidbody2D myRigidBody;
    public float walkSpeed = 3f;
    public static event Action<bool> returnMovementAI;
    //public static event Action returnMovementNPC;

    public static event Action bloodSpot;
    public static event Action kidTransform;
    public bool transformed = false;
    public bool transforming = false;
    public bool isDead = false;
    public bool finished = false;
    public bool doom = false;
    public GameObject bloodPuddle;
    bool isSpinning = true;
    public bool monsterFound = false;
    void Start() {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        //stores initial checkpoint mark (this changes as you progress)
        checkpointPosition = new Vector3(3f, 0f, 0f);
        //AICopScriptLR.isColliding += dead;
        //AICopScriptUD.isCollidingUD += deadUD;
    }
    void Update()
    {
        if (name == "Naomi") {
            anim.SetBool("transform", transformed);
        }

        //This ultimately will handle animations.
        if (!isMoving)
            {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
                anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxis("Vertical"));
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
                anim.SetBool("moving", isMoving);

            //This will get the last movement and will leave your idle position facing the correct way.
                if (input != Vector2.zero)
                {

                //This will stop all forms of movement available when speaking to NPC.
                if (isInteracting == true || isDead == true || monsterFound == true)
                {
                    myRigidBody.velocity = Vector2.zero;
                    return;
                }

                if (input.x < 0)
                    {
                        lastMove = new Vector2(Input.GetAxis("Horizontal"), 0f);

                    }
                    if (input.x > 0)
                    {
                        lastMove = new Vector2(Input.GetAxis("Horizontal"), 0f);
                    }
                    if (input.y > 0)
                    {
                        lastMove = new Vector2(0f, Input.GetAxis("Vertical"));
                    }
                    if (input.y < 0)
                    {
                        lastMove = new Vector2(0f, Input.GetAxis("Vertical"));
                    }
                //This starts the coroutine that controls movement.
               // StartCoroutine(Move(transform));



                
                isMoving = true;

                if (Input.GetAxisRaw("Horizontal") > .5f || Input.GetAxisRaw("Horizontal") < -.5f)
                {
                    myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, myRigidBody.velocity.y);
                    anim.SetBool("moving", isMoving);

                }
                if (Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f)
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
                    anim.SetBool("moving", isMoving);

                }

                if (Input.GetAxisRaw("Horizontal") < .5f && Input.GetAxisRaw("Horizontal") > -.5f)
                {
                    myRigidBody.velocity = new Vector2(0.0f, myRigidBody.velocity.y);

                }

                if (Input.GetAxisRaw("Vertical") < .5f && Input.GetAxisRaw("Vertical") > -.5f)
                {
                    myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 0.0f);

                }
                //This will handle diagonal movement speed, so it stays more consistent.
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > .5f)
                {
                    speed = walkSpeed / 1.35f;
                }
                else
                {
                    speed = walkSpeed;
                }

                isMoving = false;

    



            }
                //This will handle what happens when you're detected by the cop AI.
            //AICopScript.isColliding += dead;

        }
    }

    //This will handle the movement.
    /*
    public IEnumerator Move(Transform entity)
    {
       
            isMoving = true;

            if (Input.GetAxisRaw("Horizontal") > .5f || Input.GetAxisRaw("Horizontal") < -.5f)
            {
                myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, myRigidBody.velocity.y);
                anim.SetBool("moving", isMoving);

            }
            if (Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
                anim.SetBool("moving", isMoving);

            }

            if (Input.GetAxisRaw("Horizontal") < .5f && Input.GetAxisRaw("Horizontal") > -.5f)
            {
                myRigidBody.velocity = new Vector2(0.0f, myRigidBody.velocity.y);

            }

            if (Input.GetAxisRaw("Vertical") < .5f && Input.GetAxisRaw("Vertical") > -.5f)
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 0.0f);

            }
            //This will handle diagonal movement speed, so it stays more consistent.
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > .5f)
            {
                speed = walkSpeed / 1.35f;
            }
            else
            {
                speed = walkSpeed;
            }

            isMoving = false;
            yield return 0;
    */

    //This method handles the death sequence, by reverting your position back to the last checkpoint.
    public void dead(bool rip)
    {
        if (rip == true)
        {
            if (isSpinning == true)//This was created so that we wouldn't rotate twice in the case that two enemies spot the character
                //at the same time.
            {

                StartCoroutine(CheckpointHandler(transform));
            }

            //transform.position = checkpointPosition;
        }
    }

    public void deadUD(bool rip)
    {
        if (rip == true)
        {
            if (isSpinning == true)//This was created so that we wouldn't rotate twice in the case that two enemies spot the character
                                   //at the same time.
            {

                StartCoroutine(CheckpointHandler(transform));
            }

            //transform.position = checkpointPosition;
        }
    }

    public void deadEnd()
    {
            if (isSpinning == true)
            {
                StartCoroutine(deadScene(transform));
            }
    }

    public IEnumerator deadScene(Transform checkpoint)
    {
        isDead = true;
        isSpinning = false;
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        doom = true;
        if (bloodSpot != null)//will toggle the blood on and off
        {
            bloodSpot();
        }

        isDead = false;
        isSpinning = true;
    }

    void OnEnable()
    {
        AICopScriptLR.isColliding += dead;
        AICopScriptUD.isCollidingUD += deadUD;
        EndAI.isCollidingEnd += deadEnd;
        Checkpoint.checkpointHandler += updateCheckpoint;
        ChildAnthony.stopMovement += stop;
        CityLevelDialogue.end += done;
        Dialogue.ending += ending;
        HouseTransport.transportMovement += transport;
    }
    void OnDisable()
    {
        AICopScriptLR.isColliding -= dead;
        AICopScriptUD.isCollidingUD -= deadUD;
        EndAI.isCollidingEnd -= deadEnd;
        Checkpoint.checkpointHandler -= updateCheckpoint;
        ChildAnthony.stopMovement -= stop;
        CityLevelDialogue.end -= done;
        Dialogue.ending -= ending;
        HouseTransport.transportMovement -= transport;


    }
    void updateCheckpoint()
    {
        checkpointPosition = new Vector3(67,-40, 0);
    }

    //This method will delay the death sequence, so that there is time for an animation to play before reverting back to the checkpoint.
    public IEnumerator CheckpointHandler(Transform checkpoint)
    {
            isDead = true;
            isSpinning = false;
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.forward * -10);
            if (bloodSpot != null)//will toggle the blood on and off
            {
                //   print("hi");
                bloodSpot();
            }
            yield return new WaitForSeconds(2f);
            transform.Rotate(Vector3.forward * 90);
            transform.position = checkpointPosition;
            isDead = false;
            isSpinning = true;

            if (returnMovementAI != null)
            {
                returnMovementAI(true);
            }
        

        
    }
    void stop()
    {
        isInteracting = true;
    }
    void transport()
    {
        isInteracting = true;
        StartCoroutine(transported(transform));
    }
    public IEnumerator transported(Transform i)
    {
        yield return new WaitForSeconds(3.5f);
        isInteracting = false;
    }

   void done()
    {
        transforming = true;

    }
    void ending()
    {
        isInteracting = true;
        StartCoroutine(flashAnimation(transform));
    }
    public IEnumerator flashAnimation(Transform i)
    {
        if (finished == false) {
            yield return new WaitForSeconds(.3f);
            transformed = false;
            yield return new WaitForSeconds(.3f);
            transformed = true;
            yield return new WaitForSeconds(.3f);
            transformed = false;
            yield return new WaitForSeconds(.3f);
            transformed = true;
            yield return new WaitForSeconds(.3f);
            transformed = false;
            yield return new WaitForSeconds(.3f);
            transformed = true;
            yield return new WaitForSeconds(.3f);
            transformed = false;
            yield return new WaitForSeconds(.6f);
            transformed = true;
            finished = true;
            if (kidTransform != null)
            {
                kidTransform();
            }
        }
    }
}