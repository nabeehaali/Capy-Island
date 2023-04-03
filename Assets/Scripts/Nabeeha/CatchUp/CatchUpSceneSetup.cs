using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchUpSceneSetup : MonoBehaviour
{
    public GameObject goP1, goP2, goP3, goP4, goTxt;

    int listCount;
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

        for (int i = 0; i < rankings.Count; i++)
        {
            Debug.Log(rankings[i].playerID + " has " + rankings[i].playerPoints + " points");
        }

        totalHats = DisasterSceneSetup.p1HatsOff + DisasterSceneSetup.p2HatsOff + DisasterSceneSetup.p3HatsOff + DisasterSceneSetup.p4HatsOff;
        totalHats /= 2;

        StartCoroutine(startGame());
        StartCoroutine(startPlayer());
    }

    private void Update()
    {
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

    IEnumerator startPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            if (rankings.Count != 0)
            {
                //show first element by deafult
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                GameObject.Find(rankings[0].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find(rankings[0].playerID).GetComponent<Rigidbody>().isKinematic = false;
                listCount++;

                if (rankings.Count > 1)
                {
                    if (rankings[0].playerPoints == rankings[1].playerPoints)
                    {
                        //compare if first element is the same as other elements
                        for (int i = 0; i < rankings.Count - 1; i++)
                        {
                            if (rankings[i].playerPoints == rankings[i + 1].playerPoints)
                            {
                                GameObject.Find(rankings[i + 1].playerID).gameObject.transform.parent.gameObject.transform.position = startingPos.position;
                                GameObject.Find(rankings[i + 1].playerID).gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
                                GameObject.Find(rankings[i + 1].playerID).GetComponent<Rigidbody>().isKinematic = false;
                                listCount++;

                                
                            }
                        }
                    }
                }
                
                //change spacing of panel depending on how many players get put on the field
                if (listCount == 1 || listCount == 4)
                {
                    GameObject.Find("GoPanelIcon").GetComponent<HorizontalLayoutGroup>().spacing = 0;
                }
                else if (listCount == 2)
                {
                    GameObject.Find("GoPanelIcon").GetComponent<HorizontalLayoutGroup>().spacing = -300;
                }
                else if (listCount == 3)
                {
                    GameObject.Find("GoPanelIcon").GetComponent<HorizontalLayoutGroup>().spacing = -150;
                }

                //changing ui based on who comes up
                for (int j = 0; j < listCount; j++)
                {
                    goTxt.SetActive(true);

                    if (GameObject.Find(rankings[j].playerID).gameObject.tag == "Player 1")
                    {
                        goP1.SetActive(true);
                    }
                    else if (GameObject.Find(rankings[j].playerID).gameObject.tag == "Player 2")
                    {
                        goP2.SetActive(true);
                    }
                    else if (GameObject.Find(rankings[j].playerID).gameObject.tag == "Player 3")
                    {
                        goP3.SetActive(true);
                    }
                    else if (GameObject.Find(rankings[j].playerID).gameObject.tag == "Player 4")
                    {
                        goP4.SetActive(true);
                    }
                }

                yield return new WaitForSeconds(1.5f);
                goTxt.SetActive(false);
                goP1.SetActive(false);
                goP2.SetActive(false);
                goP3.SetActive(false);
                goP4.SetActive(false);

                //remove element from list once player is on screen
                for (int i = 0; i < listCount; i++)
                {
                    //Debug.Log(rankings[0].playerPoints);
                    rankings.RemoveAt(0);
                }

                listCount = 0;
            }
        }
    }

    void EndGame()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            Destroy(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<CatchUpControls>());
        }

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
        gameover.SetText("Game Over!");
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
        goPlayers[index].gameObject.SetActive(false);
    }

    IEnumerator startGame()
    {
        gameover.gameObject.SetActive(true);
        int count = 5;

        while (count > 0)
        {
            gameover.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }
        gameover.gameObject.SetActive(false);
    }

 }
