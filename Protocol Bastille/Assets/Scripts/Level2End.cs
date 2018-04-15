using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Level2End : MonoBehaviour {
    public static event Action level2End;
    public Player player;  
    void OnTriggerEnter2D(Collider2D other)
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(end(transform));
    }
    public IEnumerator end(Transform t)
    {
        player.isInteracting = true;

        yield return new WaitForSeconds(2f);
        if (level2End != null)
        {
            level2End();
        }

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("nightscene");

    }
}
