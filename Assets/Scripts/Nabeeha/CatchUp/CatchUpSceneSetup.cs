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
    public static int totalHats;

    public static List<MinigamePoints> catchuppoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    public TMP_Text gameover;
    bool gameDone = false;

    public TMP_Text[] goPlayers;

    private void Start()
    {
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, (GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(3).GetChild(0).childCount));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, (GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(3).GetChild(0).childCount));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, (GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(3).GetChild(0).childCount));
        rankings.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, (GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).childCount - 1) + GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(3).GetChild(0).childCount));

        rankings.Sort();

        /*for (int i = 0; i < rankings.Count; i++)
        {
            Debug.Log(rankings[i].playerID + " has " + rankings[i].playerPoints + " points");
        }*/

        totalHats = DisasterSceneSetup.p1HatsOff + DisasterSceneSetup.p2HatsOff + DisasterSceneSetup.p3HatsOff + DisasterSceneSetup.p4HatsOff;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        Debug.Log(timePassed);

        if (timePassed > 0)
        {
            if (!moveOnP1)
            {
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[0].playerID).GetComponent<Rigidbody>().isKinematic = false;
                checkUI(0);
                    
                if (rankings[0].playerPoints == rankings[1].playerPoints)
                {
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[1].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(1);
                }
                if(rankings[0].playerPoints == rankings[2].playerPoints)
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(2);
                }
                if (rankings[0].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(3);
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
                    checkUI(1);
                }
                if (rankings[1].playerPoints == rankings[2].playerPoints)
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(2);
                }
                if (rankings[1].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(3);
                }
                moveOnP2 = true;
            }
        }
        if (timePassed > 10)
        {
            if (!moveOnP3)
            {
                if ((rankings[2].playerPoints != rankings[1].playerPoints) && (rankings[2].playerPoints != rankings[0].playerPoints))
                {
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[2].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[2].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(2);
                }
                if (rankings[2].playerPoints == rankings[3].playerPoints)
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(3);
                }
                moveOnP3 = true;
            }
        }
        if (timePassed > 15)
        {
            if (!moveOnP4)
            {
                if ((rankings[3].playerPoints != rankings[2].playerPoints) && (rankings[3].playerPoints != rankings[1].playerPoints) && (rankings[3].playerPoints != rankings[0].playerPoints))
                {
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[3].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[3].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(3);
                }
                moveOnP4 = true;
            }
        }

        /*for (int i = 0; i < rankings.Count; i++)
        {
            if (timePassed > newTime)
            {
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[0].playerID).GetComponent<Rigidbody>().isKinematic = false;
                checkUI(0);

                if (rankings[0].playerPoints == rankings[i].playerPoints)
                {
                    GameObject.Find(rankings[i].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                    GameObject.Find(rankings[i].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    GameObject.Find(rankings[i].playerID).GetComponent<Rigidbody>().isKinematic = false;
                    checkUI(i);
                }
                newTime += 5;
                
            }
        }*/


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
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            Destroy(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<CatchUpControls>());
        }
        //GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 0;
        //GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 0;
        //GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 0;
        //GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 0;

        //GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;
        //GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;
        //GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;
        //GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = false;

        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").GetComponent<CatchUp>().numHatsCollected));
        catchuppoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").GetComponent<CatchUp>().numHatsCollected));

        catchuppoints.Sort();
        catchuppoints.Reverse();

        distinct = catchuppoints.Distinct(new ItemEqualityComparer()).ToList();

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        //destroy hats on head
        for (int i = 0; i < catchuppoints.Count; i++)
        {
            for (int j = 0; j < GameObject.Find(catchuppoints[i].playerID).transform.GetChild(3).childCount; j++)
            {
                if(GameObject.Find(catchuppoints[i].playerID).transform.GetChild(3).GetChild(j).gameObject.activeSelf == true)
                {
                    Destroy(GameObject.Find(catchuppoints[i].playerID).transform.GetChild(3).GetChild(j).gameObject);
                }
            }
            
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void checkUI(int index)
    {
        if (GameObject.Find(rankings[index].playerID).gameObject.tag == "Player 1")
        {
            StartCoroutine(goUI(0));
        }
        if (GameObject.Find(rankings[index].playerID).gameObject.tag == "Player 2")
        {
            StartCoroutine(goUI(1));
        }
        if (GameObject.Find(rankings[index].playerID).gameObject.tag == "Player 3")
        {
            StartCoroutine(goUI(2));
        }
        if (GameObject.Find(rankings[index].playerID).gameObject.tag == "Player 4")
        {
            StartCoroutine(goUI(3));
        }
    }

    IEnumerator goUI(int index)
    {
        goPlayers[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //Destroy(goPlayers[index].gameObject);
        goPlayers[index].gameObject.SetActive(false);
    }
}
