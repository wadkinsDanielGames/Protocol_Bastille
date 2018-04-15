using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;
    public GameObject otherTarget;
    public bool other = false;
    void Start()
    {

    }
    void Update()
    {
        //This places a blank gameobject that that follows the player (if the player is the assigned target)
        if(other == false){
            targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            moveSpeed = 2;
            targetPosition = new Vector3(otherTarget.transform.position.x, otherTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    public void Switch()
    {
        other = true;
    }
    public void OnEnable()
    {
        MonsterCutscene.Camera += Switch;
    }
    public void OnDisable()
    {
        MonsterCutscene.Camera -= Switch;
    }
}

