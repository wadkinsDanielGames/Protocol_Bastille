using UnityEngine;
using System.Collections;
using System;
public class DialogueInteraction : MonoBehaviour
{
    private Dialogue dialogueManager;
    public string[] linesOfDialogue;
    private bool isColliding = false;
    //private NPCWalk npc;
    public static event Action stopMovementNPC;
    public static event Action monsterCutscene;

    public string[] alternativeDialogue;
    public bool level2CompleteDialogue = false;
    public DialogueInteraction mother;
    public GameObject lvl2End;
    public Player main;
    bool monsterLocated = false;
    // Use this for initialization
    void Start()
    {
        main = FindObjectOfType<Player>();
        dialogueManager = FindObjectOfType<Dialogue>();
        //mother = FindObjectOfType<DialogueInteraction>();
     //   npc = FindObjectOfType<NPCWalk>();  
    }

    // Update is called once per frame
    void Update()
    {
        //If you're standing in the speech zone and press space, this will initiate the dialogue sequence.
        if (isColliding == true && monsterLocated == false)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (dialogueManager.opened == false)
                {
                    if (alternativeDialogue.Length == 0) { 
                        dialogueManager.linesOfDialogue = linesOfDialogue;
                        dialogueManager.lines = 0;
                        dialogueManager.ShowDialogue();
                        //npc.talking = true;
                        //npc.standStill = true; 
                    }
                    else
                    {
                        if (level2CompleteDialogue == false) {
                            dialogueManager.linesOfDialogue = linesOfDialogue;
                            dialogueManager.lines = 0;
                            dialogueManager.ShowDialogue(); 
                        }
                        else
                        {
                            dialogueManager.linesOfDialogue = alternativeDialogue;
                            dialogueManager.lines = 0;
                            dialogueManager.ShowDialogue();
                        }
                    }
                    if (name == "Sister")
                    {
                        mother.level2CompleteDialogue = true;
                        lvl2End.SetActive(true);
                        //level2CompleteDialogue = true;
                    }
                    if (name == "Sister2")
                    {
                        StartCoroutine(monster(transform));
                        main.monsterFound = true;
                        monsterLocated = true;
                    }
                    if (stopMovementNPC != null)
                    {
                        stopMovementNPC();
                    }
                }
            }
        }
    }

    public IEnumerator monster(Transform t)
    {
        yield return new WaitForSeconds(2.0f);
        if (monsterCutscene != null)
        {
            monsterCutscene();
        }
    }

    //If you're within the collisionbox range, you can initiate the dialogue sequence.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Anthony" || other.gameObject.name == "Naomi")
        {
            isColliding = true;
        }
    }

    //If you leave the collisionbox, you cannot initiate the dialogue sequence.
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Anthony" || other.gameObject.name == "Naomi")
        {
            isColliding = false;
        }
    }
}
