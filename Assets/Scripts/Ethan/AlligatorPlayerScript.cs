using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlligatorPlayerScript : MonoBehaviour
{
    public bool isLeader = true;
    public int points = 0;
    public int increase = 1;
    public float interval = 10f;
    public float startTime = 5f;
    public TextMeshPro display;
    public GameObject crownObj;

    float lastUpdate = 0;
    bool isBit = false;
    string playerID;

    bool hasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        playerID = gameObject.transform.GetChild(0).tag;
        Debug.Log("ID = " + playerID);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Time.time >= startTime)
        {
            Debug.Log("Start!");
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            hasStarted = true;
        }

        if(isLeader && Time.time - lastUpdate > interval)
        {
            points += increase;
            lastUpdate = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // uncomment this soon!
        // display.SetText(points.ToString());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Alligator" && other.GetComponent<AlligatorBrain>().rise)
        {
            isBit = true;
        }
    }

    private void bit()
    {

    }

    private void Steal()
    {
        Debug.Log(playerID + "has tried to steal!");
    }
}
