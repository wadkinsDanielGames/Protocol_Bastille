using UnityEngine;
using System.Collections;

public class StartingDialogue : MonoBehaviour
{
    private Dialogue dialogueManagerr;
    public string[] linesOfDialoguee;
    bool started = true;
    //private bool isColliding = false;

    // Use this for initialization
    void Start()
    {
        dialogueManagerr = FindObjectOfType<Dialogue>();
        dialogueManagerr.opened = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started == true)
        {
        
        if (dialogueManagerr.opened == false)
        {
            dialogueManagerr.linesOfDialogue = linesOfDialoguee;
            dialogueManagerr.lines = 0;
            dialogueManagerr.ShowDialogue();
            started = false;
            
            }
        }
    }

}
