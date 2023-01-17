using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchUp : MonoBehaviour
{
    public static int numHatsCollected;
    float timePassed;

    bool moveOnP2 = false;
    bool moveOnP3 = false;
    bool moveOnP4 = false;
    public Transform startingPos;

    private void Start()
    {
        numHatsCollected = 0;
        startingPos = GameObject.Find("StartingPosition").transform;

        GameObject.FindGameObjectWithTag("Player 1").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player 3").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player 4").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed > 5)
        {
            if (!moveOnP2)
            {
                GameObject.FindGameObjectWithTag("Player 2").gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.FindGameObjectWithTag("Player 2").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                moveOnP2 = true;
            }
        }
        if(timePassed > 10)
        {
            if (!moveOnP3)
            {
                GameObject.FindGameObjectWithTag("Player 3").gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.FindGameObjectWithTag("Player 3").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                moveOnP3 = true;
            }
        }
        if (timePassed > 15)
        {
            if (!moveOnP4)
            {
                GameObject.FindGameObjectWithTag("Player 4").gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.FindGameObjectWithTag("Player 4").gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                moveOnP4 = true;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BaseHat")
        {
            Destroy(collision.gameObject);
            numHatsCollected += 1;
        }
    }
}
