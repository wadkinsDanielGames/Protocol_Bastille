using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class EndAI : MonoBehaviour {
    public Vector3 velocity;
    public Vector3 startLocation;
    public bool caught = false;
    public int startLocationX;
    public int endLocationX;
    public int startLocationY;
    public int endLocationY;
    public int tempEndLocationX;
    public int tempEndLocationY;

    //public static event Action isCollidingEnd;

    private Animator anim;
    public static event Action isCollidingEnd;
    public static event Action blackscreenEnd;

    public GameObject gunshot;
    // On the start, the ending location is stored and the start location is stored.
    void Start()
    {
        //SetVelocity();
        tempEndLocationY = endLocationY;
        anim = GetComponent<Animator>();
        startLocation = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!caught)
        {
            //if the AI has not interacted with the player, the character will move up and down. Also handles animation.

            if (transform.position.y < endLocationY)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                //fireZone.offset = new Vector2(0f, 3.5f);
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
                    //fireZone.offset = new Vector2(0f, -3.5f);

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
            
            if (isCollidingEnd != null)
            {
                isCollidingEnd();
            }
            caught = true;
           // anim.SetBool("stop", caught);
            anim.enabled = false;
            StartCoroutine(Fire(transform));
        }
    }
    
    void OnEnable()
    {
        ChildAnthony.guards += setVelocity;
    }
    void OnDisable()
    {
        ChildAnthony.guards -= setVelocity;

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
        yield return new WaitForSeconds(2.5f);
        if (blackscreenEnd != null)
        {
            blackscreenEnd();
        }
    }

    void setVelocity()
    {
        velocity = new Vector3(0, 3, 0);
    }
   
}
