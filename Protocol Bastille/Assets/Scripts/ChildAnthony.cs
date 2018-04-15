using UnityEngine;
using System.Collections;
using System;
public class ChildAnthony : MonoBehaviour {
    public GameObject other;
    public bool isDone = false;
    public Vector3 velocity = new Vector3(0,5,0);
    public static event Action stopMovement;
    public static event Action guards;
    public Animator anim;
    public bool transforming = false;
    public bool isFalling = false;
    public GameObject lastDialogue;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        Player.kidTransform += finalAnimation;
    }
    void OnDisable()
    {
        Player.kidTransform -= finalAnimation;
    }
    // Update is called once per frame
    void Update () {
        anim.SetBool("transform", transforming);

        if (isDone == true) {

            if (transform.position.y > -43.5) {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * -velocity.y, 0);
            }
            else
            {
                isDone = false;
                isFalling = true;
                anim.SetBool("fall", isFalling);
                StartCoroutine(stop(transform));
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        isDone = true;
        transform.position = new Vector3(other.transform.position.x, -35, 0);
        if (stopMovement != null)
        {
            stopMovement();
        }
    }
    public IEnumerator stop(Transform move)
    {
        transform.Rotate(Vector3.forward * -10);
        transform.position = new Vector3(transform.position.x + .2f,transform.position.y - .1f, 0);
        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        transform.position = new Vector3(transform.position.x + .2f, transform.position.y - .1f, 0);

        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        transform.position = new Vector3(transform.position.x + .2f, transform.position.y - .1f, 0);

        yield return new WaitForSeconds(.01f);
        transform.Rotate(Vector3.forward * -10);
        transform.position = new Vector3(transform.position.x + .2f, transform.position.y - .1f, 0);

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
    }
    void finalAnimation()
    {

        StartCoroutine(final(transform));
    }
    public IEnumerator final(Transform t)
    {
        yield return new WaitForSeconds(.3f);
        transforming = false;
        yield return new WaitForSeconds(.3f);
        transforming = true;
        yield return new WaitForSeconds(.3f);
        transforming = false;
        yield return new WaitForSeconds(.3f);
        transforming = true;
        yield return new WaitForSeconds(.3f);
        transforming = false;
        yield return new WaitForSeconds(.3f);
        transforming = true;
        yield return new WaitForSeconds(.3f);
        transforming = false;
        yield return new WaitForSeconds(.3f);
        transforming = true;
        yield return new WaitForSeconds(.6f);
        transforming = false;
        lastDialogue.SetActive(true);
        if(guards != null)
        {
            guards();
        }
    }
}

