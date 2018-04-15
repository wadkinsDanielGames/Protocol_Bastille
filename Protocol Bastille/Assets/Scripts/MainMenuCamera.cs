using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

    public Vector3 velocity;
    public int startLocationX;
    public int endLocationX;
    public int startLocationY;
    public int endLocationY;
    public int tempEndLocationX;
    public int tempEndLocationY;

    // Use this for initialization
    void Start () {
        tempEndLocationY = endLocationY;

    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < endLocationY)
        {
            transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
            return;
        }
        if (transform.position.y > endLocationY - 1)
        {
            endLocationY = startLocationY;
            if (transform.position.y > endLocationY)
            {
                transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * -velocity.y, 0);

                if (transform.position.y < endLocationY)
                {
                    endLocationY = tempEndLocationY;
                }
            }
        }
    }
}
