using UnityEngine;
using System.Collections;
using System;
public class MonsterCutscene : MonoBehaviour {
    public Player main;
    public GameObject node1;
    public GameObject node2;
    public GameObject node3;
    public GameObject node4;
    public int nodePassed = 0;
    public Vector3 velocity = new Vector3(6f, 0f, 0f);
    public bool lightsOn = false;
    private Animator anim;
    public GameObject lights;
    public static event Action Camera;
    // Use this for initialization
    void Start () {
        main = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();

        main.isInteracting = true;
        StartCoroutine(beginAnimation(transform));
	}
	
    public IEnumerator beginAnimation(Transform t)
    {

        yield return new WaitForSeconds(3f);
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(2f);
        lights.SetActive(true);
        yield return new WaitForSeconds(.2f);

        anim.SetBool("frightened", true);

        if (Camera != null)
        {
            Camera();
        }
        yield return new WaitForSeconds(2.0f);
        lightsOn = true;
    }
	// Update is called once per frame
	void Update () {
        if (lightsOn == true) {
            if (nodePassed == 0)
            {
                anim.SetBool("monsterleft", true);
                if (transform.position.x > node1.transform.position.x)
                {
                    transform.Translate(Time.deltaTime * -velocity.x, Time.deltaTime * velocity.y, 0);
                }

                else
                {
                    nodePassed = 1;
                }
            }
            if (nodePassed == 1)
            {
                velocity = new Vector3(0f, 6f, 0);
                anim.SetBool("monsterdown", true);
                if (transform.position.y > node2.transform.position.y)
                {
                    transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * -velocity.y, 0);
                }
                else
                {
                    nodePassed = 2;
                }
            }
            if (nodePassed == 2)
            {
                velocity = new Vector3(6f, 0f, 0);
                anim.SetBool("monsterleft1", true);
                if (transform.position.x > node3.transform.position.x)
                {
                    transform.Translate(Time.deltaTime * -velocity.x, Time.deltaTime * velocity.y, 0);
                }
                else
                {
                    nodePassed = 3;
                }
            }

            if (nodePassed == 3)
            {
                velocity = new Vector3(0f, 6f, 0);
                anim.SetBool("monsterup", true);
                if (transform.position.y < node4.transform.position.y)
                {
                    transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                }
                else
                {
                    nodePassed = 4;
                }
            }
            if (nodePassed == 4)
            {
                anim.SetBool("blank", true);
                
            }
        }

    }
}
