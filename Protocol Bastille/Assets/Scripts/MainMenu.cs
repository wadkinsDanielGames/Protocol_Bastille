using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
public class MainMenu : MonoBehaviour {
    public static event Action blackScreen;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject blankObject;
    public GameObject blankObject2;
    public GameObject blankObject3;
    Vector3 velocity = new Vector3(200f, 0f, 0f);
    bool isMoving = false;
    Vector3 initialPosition1;
    Vector3 initialPosition2;
    Vector3 initialPosition3;
    Vector3 initialPosition4;

    Vector3 initialPosition5;
    Vector3 initialPosition6;
    Vector3 initialPosition7;
    bool isReturning = false;
    // Use this for initialization
    void Start () {
        BlackScreen.nextScene += StartScene;
        initialPosition1 = button1.transform.position;
        initialPosition2 = button2.transform.position;
        initialPosition3 = button3.transform.position;
        initialPosition4 = button4.transform.position;

        initialPosition5 = blankObject.transform.position;
        initialPosition6 = blankObject2.transform.position;
        initialPosition7 = blankObject3.transform.position;
        //StartCoroutine(LocationSettings(transform));


    }

    // Update is called once per frame
    void FixedUpdate () {
	if(isMoving == true)
        {
            if (button1.transform.position.x < initialPosition5.x)
            {
                button1.transform.Translate(Time.deltaTime * velocity.x, 0f, 0f);
            }
            if (button2.transform.position.x < initialPosition5.x)
            {
                button2.transform.Translate(Time.deltaTime * velocity.x, 0f, 0f);
            }
            if (button3.transform.position.x > initialPosition6.x)
            {
                button3.transform.Translate(Time.deltaTime * -velocity.x, 0f, 0f);
            }
            if (button4.transform.position.x > initialPosition7.x)
            {
                button4.transform.Translate(Time.deltaTime * -velocity.x, 0f, 0f);
            }
        }
    if (isReturning == true)
        {
            if (button1.transform.position.x > initialPosition1.x)
            {
                button1.transform.Translate(Time.deltaTime * -velocity.x, 0f, 0f);
            }
            if (button2.transform.position.x > initialPosition2.x)
            {
                button2.transform.Translate(Time.deltaTime * -velocity.x, 0f, 0f);
            }
            if (button3.transform.position.x < initialPosition3.x)
            {
                button3.transform.Translate(Time.deltaTime * velocity.x, 0f, 0f);
            }
            if (button4.transform.position.x < initialPosition4.x)
            {
                button4.transform.Translate(Time.deltaTime * velocity.x, 0f, 0f);
            }
        }
    }

    public void StartGame()
    {
        if (blackScreen != null) { 
            blackScreen();
        }
        //SceneManager.LoadScene("City");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("news scene");
    }

    public void Controls()
    {
        isMoving = true;
        isReturning = false;
    }

    public void Back()
    {
        isMoving = false;
        isReturning = true;
    }
    void OnEnable()
    {
        BlackScreen.nextScene += StartScene;

    }
    void OnDisable()
    {
        BlackScreen.nextScene -= StartScene;

    }
    IEnumerator LocationSettings(Transform position)
    {
        yield return new WaitForSeconds(0.1f);
        initialPosition1 = button1.transform.position;
        initialPosition2 = button2.transform.position;
        initialPosition3 = button3.transform.position;
        initialPosition4 = button4.transform.position;
        initialPosition5 = blankObject.transform.position;
        yield return 0;
    }
}
