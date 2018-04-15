using UnityEngine;
using System.Collections;
using System;
public class HouseTransport : MonoBehaviour
{
    public GameObject Anthony;
    //public GameObject blackScreen;
    public static event Action transportMovement;
    public Vector3 transportLocation;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (transportMovement != null)
        {
            transportMovement();
        }
        StartCoroutine(transport(transform));
    }
    public IEnumerator transport(Transform t)
    {
        //blackScreen.transform.position = new Vector3(0,0,0);
        yield return new WaitForSeconds(1.5f);
        Anthony.transform.position = transportLocation;
    }
}
