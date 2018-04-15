using UnityEngine;
using System.Collections;
using System;
public class Checkpoint : MonoBehaviour {
    public static event Action checkpointHandler;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Naomi" || other.name == "Anthony")
        {
            if (checkpointHandler!= null) {
                checkpointHandler();
            }
        }
    }
}
