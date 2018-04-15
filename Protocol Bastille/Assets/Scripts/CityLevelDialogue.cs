using UnityEngine;
using System.Collections;
using System;
public class CityLevelDialogue : MonoBehaviour {

    private Dialogue dialogueManagerrr;
    public string[] linesOfDialogueee;
    bool started = false;
    int done = 0;
    public static event Action end;
    //private bool isColliding = false;

    // Use this for initialization
    void Start()
    {
        dialogueManagerrr = FindObjectOfType<Dialogue>();
        dialogueManagerrr.opened = false;
        if (name == "last")
        {
            started = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (started == true && done == 0)
        {

            if (dialogueManagerrr.opened == false)
            {
                dialogueManagerrr.linesOfDialogue = linesOfDialogueee;
                dialogueManagerrr.lines = 0;
                dialogueManagerrr.ShowDialogue();
                started = false;
                done = 1;
                if (name == "KidAnthony")
                {
                    if (end != null)
                    {
                        end();
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Naomi" || other.name == "Anthony") {
            if (name == "KidAnthony")
            {
                StartCoroutine(delay(transform));
            }
            else
            {
                started = true;
            }
        }
    }
    public IEnumerator delay(Transform i)
    {
        yield return new WaitForSeconds(1.3f);
        started = true;
    }
}
