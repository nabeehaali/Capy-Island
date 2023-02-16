using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CatchUpSceneSetup : MonoBehaviour
{
    float timePassed;

    bool moveOnP1 = false;
    bool moveOnP2 = false;
    bool moveOnP3 = false;
    bool moveOnP4 = false;
    public Transform startingPos;

    public static List<MinigamePoints> rankings = new List<MinigamePoints>();
    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    public TMP_Text hatsText;
    int hatsCollected;
    int totalHats;

    public static List<MinigamePoints> catchuppoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    public TMP_Text gameover;
    bool gameDone = false;

    private void Start()
    {
        //startingPos = GameObject.Find("StartingPosition").transform;

        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1));

        rankings.Sort();
        rankings.Reverse();

        totalHats = DisasterSceneSetup.p1HatsOff + DisasterSceneSetup.p2HatsOff + DisasterSceneSetup.p3HatsOff + DisasterSceneSetup.p4HatsOff;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > 0)
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
        }
        if (timePassed > 5)
        {
            if (!moveOnP2)
            {
                if (rankings[1].playerPoints != rankings[0].playerPoints)
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
                }
                moveOnP2 = true;
            }
        }
        if (timePassed > 10)
        {
            if (!moveOnP3)
            {
                if (rankings[2].playerPoints != rankings[1].playerPoints && rankings[2].playerPoints != rankings[0].playerPoints)
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
                }
                moveOnP3 = true;
            }
        }
        if (timePassed > 15)
        {
            if (!moveOnP4)
            {
                if (rankings[3].playerPoints != rankings[2].playerPoints && rankings[3].playerPoints != rankings[1].playerPoints && rankings[3].playerPoints != rankings[0].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                }
                moveOnP4 = true;
            }
        }


        //player score UI
        p1Score.SetText("" + GameObject.FindGameObjectWithTag("Player 1").GetComponent<CatchUp>().numHatsCollected + " Hats");
        p2Score.SetText("" + GameObject.FindGameObjectWithTag("Player 2").GetComponent<CatchUp>().numHatsCollected + " Hats");
        p3Score.SetText("" + GameObject.FindGameObjectWithTag("Player 3").GetComponent<CatchUp>().numHatsCollected + " Hats");
        p4Score.SetText("" + GameObject.FindGameObjectWithTag("Player 4").GetComponent<CatchUp>().numHatsCollected + " Hats");

        hatsCollected = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CatchUp>().numHatsCollected + GameObject.FindGameObjectWithTag("Player 2").GetComponent<CatchUp>().numHatsCollected + GameObject.FindGameObjectWithTag("Player 3").GetComponent<CatchUp>().numHatsCollected + GameObject.FindGameObjectWithTag("Player 4").GetComponent<CatchUp>().numHatsCollected;

        hatsText.SetText("Hats Collected: " + hatsCollected + " / " + totalHats);

        if (hatsCollected == totalHats && !gameDone)
        {
            EndGame();
            gameDone = true;
        }


    }

    void EndGame()
    {
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").GetComponent<CatchUp>().numHatsCollected));

        catchuppoints.Sort();
        catchuppoints.Reverse();

        distinct = catchuppoints.Distinct(new ItemEqualityComparer()).ToList();

        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("HatProgressCatchUp");
    }
}
