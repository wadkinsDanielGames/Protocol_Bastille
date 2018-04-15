using UnityEngine;
using System.Collections;

public class BlackScreenFadeIn : MonoBehaviour {
    public Vector3 velocity;
    public bool isOn = false;
    public GameObject blankObject;
    public Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        //transform.position = new Vector3(550f, 200f, 0f);
        initialPosition = blankObject.transform.position;
        isOn = true;
        velocity = new Vector3(600f,0f,0f);

    }

    // Update is called once per frame
    void Update () {
        if (isOn == true)
        {
            if (transform.position.x < initialPosition.x)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
            }
            else
            {
                transform.Translate(0f, 0f, 0f);
            }
        }
    }
}
