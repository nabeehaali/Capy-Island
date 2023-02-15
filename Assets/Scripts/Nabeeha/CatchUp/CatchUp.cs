using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatchUp : MonoBehaviour
{
    public static int numHatsCollected;
    float timePassed;

    bool moveOnP1 = false;
    bool moveOnP2 = false;
    bool moveOnP3 = false;
    bool moveOnP4 = false;
    public Transform startingPos;

    public static List<MinigamePoints> rankings = new List<MinigamePoints>();
    //public List<MinigamePoints> rankingsDistinct;

    private void Start()
    {
        numHatsCollected = 0;
        startingPos = GameObject.Find("StartingPosition").transform;

        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1));

        rankings.Sort();
        rankings.Reverse();

        Debug.Log(rankings[0].playerPoints);
        Debug.Log(rankings[1].playerPoints);
        Debug.Log(rankings[2].playerPoints);
        Debug.Log(rankings[3].playerPoints);
        //rankingsDistinct = rankings.Distinct(new ItemEqualityComparer()).ToList();

        //think about putting this stuff in the palyer settings!!!
        GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find(rankings[1].playerID).GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = true;

        //TODO: REowrk this and make it work with tie cases!!!!!
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        /*if (timePassed > 0)
        {
            if (!moveOnP1)
            {
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[0].playerID).GetComponent<Rigidbody>().isKinematic = false;

                if(rankings[0].playerPoints == rankings[1].playerPoints)
                {
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[1].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                if(rankings[0].playerPoints == rankings[2].playerPoints)
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rankings[0].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
        moveOnP1 = true;
            }
        }*/
        if (timePassed > 5)
        {
            if (!moveOnP2)
            {
                GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[1].playerID).GetComponent<Rigidbody>().isKinematic = false;
                /*if (rankings[1].playerPoints != rankings[0].playerPoints)
                {
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[1].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rankings[1].playerPoints == rankings[2].playerPoints)
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rankings[1].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }*/
                moveOnP2 = true;
            }
        }
        if(timePassed > 10)
        {
            if (!moveOnP3)
            {
                GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                /*if (rankings[2].playerPoints != rankings[1].playerPoints && rankings[2].playerPoints != rankings[0].playerPoints)
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                if (rankings[1].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }*/
                moveOnP3 = true;
            }
        }
        if (timePassed > 15)
        {
            if (!moveOnP4)
            {
                GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                /*if (rankings[3].playerPoints != rankings[2].playerPoints && rankings[3].playerPoints != rankings[1].playerPoints && rankings[3].playerPoints != rankings[0].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }*/
                moveOnP4 = true;
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        //make a torchControls script that deals with button handling for digging and add the bool value to this if statement
        if (collision.gameObject.tag == "BaseHat")
        {
            //increase y value of collided GO by X amount
            //if y value of collided object reaches certain amt or higher, then destory (better yet, try to add a projectile to add it to the player's head)
            Destroy(collision.gameObject);
            numHatsCollected += 1;

            //after destroying, instantiate it under hats group in player GO
        }
    }
}
