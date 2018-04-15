using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class BlackScreen : MonoBehaviour {
    public bool isMoving = false;
    public Vector3 velocity;
    public static event Action nextScene;
    public GameObject blankObject;
    public Vector3 initialPosition;
    public Vector3 initialPosition2;
    public Vector3 tempPosition;
    public GameObject blankObject2;
    public bool transporting;
    public bool forTransporter;
    public GameObject monsterr;
    // Use this for initialization
    void Start () {
        velocity = new Vector3(-900f, 0f, 0f);
        //MainMenu.blackScreen += Open;
        initialPosition = blankObject.transform.position;
        tempPosition = initialPosition;
        initialPosition2 = blankObject2.transform.position;
        transporting = false;
    }

    // Update is called once per frame
    void Update () {
       if (isMoving == true) {
            if (transporting == false)
            {
                if (transform.position.x > initialPosition.x)
                {
                    transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                }
                else
                {
                    transform.Translate(0, 0, 0);
                    //isMoving = false;
                }
            }
            else
            {
                if (transform.position.x > initialPosition.x)
                {
                    if (forTransporter== false) {
                        transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
                    }
                }
                else
                {
                    initialPosition.x = initialPosition2.x;
                    forTransporter = true;
                    if (transform.position.x < initialPosition.x)
                    {
                        transform.Translate(Time.deltaTime * -velocity.x, Time.deltaTime * velocity.y, 0);

                    }
                    //isMoving = false;
                }
            }

       }

    }

    public void Open()
    {
        isMoving = true;
        StartCoroutine(MoveScreen(transform));
    }
    public void End()
    {
        isMoving = true;
        //StartCoroutine(MoveScreen(transform));
    }
    public void Ending()
    {
        velocity = new Vector3(-1500,0,0);
        isMoving = true;
        transporting = true;
        forTransporter = false;
        initialPosition = tempPosition;

    }

    public IEnumerator MoveScreen(Transform entity)
    {

        yield return new WaitForSeconds(2.5f);
        nextScene();
    }
    void Lvl1End()
    {
        //print("hey");
        isMoving = true;
        transporting = true;
        forTransporter = false;
        initialPosition = tempPosition;
        StartCoroutine(lvl1EndingScene(transform));
        //
        
    }

    public IEnumerator lvl1EndingScene(Transform t)
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("intermissionScene");
    }

   

    void Lvl2End()
    {
        isMoving = true;
        transporting = true;
        forTransporter = false;
        initialPosition = tempPosition;
    }

    void Monster()
    {
        

        isMoving = true;
        transporting = true;
        forTransporter = false;
        initialPosition = tempPosition;
        StartCoroutine(monster(transform));
    }

    public IEnumerator monster(Transform t)
    {
        yield return new WaitForSeconds(1.5f);
        monsterr.SetActive(true);
    }

    void OnEnable()
    {
        MainMenu.blackScreen += Open;
        Dialogue.endingNews += End;
        HouseTransport.transportMovement += Ending;
        Level2End.level2End += Lvl2End;
        EndAI.blackscreenEnd += Lvl1End;
        DialogueInteraction.monsterCutscene += Monster;

    }
    void OnDisable()
    {
        MainMenu.blackScreen -= Open;
        Dialogue.endingNews -= End;
        HouseTransport.transportMovement -= Ending;
        Level2End.level2End -= Lvl2End;
        EndAI.blackscreenEnd -= Lvl1End;
        DialogueInteraction.monsterCutscene -= Monster;

    }

}
