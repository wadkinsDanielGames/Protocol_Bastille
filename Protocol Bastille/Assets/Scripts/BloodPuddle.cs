using UnityEngine;
using System.Collections;

public class BloodPuddle : MonoBehaviour {
    public GameObject player;
    public GameObject blood;
    public Player dead;
    //public bool startSequence = true;
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dead = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update () {

    }

    public void toggle()
    {
        StartCoroutine(toggled(transform));
    }
    void OnEnable()
    {
        Player.bloodSpot += toggle;

    }
    void OnDisable()
    {
        Player.bloodSpot -= toggle;

    }
    public IEnumerator toggled(Transform toggle)
    {
        transform.position = player.transform.position + new Vector3(0f, -.272f, 0f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (dead.doom == false)
        {
            yield return new WaitForSeconds(2f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        //startSequence = true;
    }


}
