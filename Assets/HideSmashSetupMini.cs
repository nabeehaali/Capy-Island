using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HideSmashSetupMini : MonoBehaviour
{
    float timePassed;
    public int gameLength = 45;
    bool gameDone = false;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public static List<MinigamePoints> idolPoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    string playerName1 = "", playerName2 = "", playerName3 = "", playerName4 = "";

    void Start()
    {
        //transition.SetTrigger("FadeOut");

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-6.03f, 2.08f, -4), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-6.03f, 2.08f, -4), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(4.9f, 2.08f, -4), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(4.9f, 2.08f, -4), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(3).gameObject.SetActive(true);
        }

        //BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-16.37f, 2.08f, -4), 0);
        //BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-6.03f, 2.08f, -4), 0);
        //BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(4.9f, 2.08f, -4), 0);
        //BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(15.67f, 2.08f, -4), 0);

        StartCoroutine(startGame());
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = false;
        player.GetComponent<HideSmash>().enabled = true;

        player.GetComponent<Rigidbody>().isKinematic = false;

    }

    void Update()
    {
        timePassed += Time.deltaTime;

        //checking is time is up (+6 for starting time delay)
        if (timePassed >= gameLength + 6 && !gameDone)
        {
            EndGame();
            gameDone = true;
        }

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            p1Score.SetText("" + GameObject.FindGameObjectWithTag("Player 1").GetComponent<HideSmash>().playerScore + " Vases");
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            p2Score.SetText("" + GameObject.FindGameObjectWithTag("Player 2").GetComponent<HideSmash>().playerScore + " Vases");
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            p3Score.SetText("" + GameObject.FindGameObjectWithTag("Player 3").GetComponent<HideSmash>().playerScore + " Vases");
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            p4Score.SetText("" + GameObject.FindGameObjectWithTag("Player 4").GetComponent<HideSmash>().playerScore + " Vases");
        }
    }

    void EndGame()
    {

        //make sure to include this in the other games
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
        }

        //removing light at the end of the game
        GameObject.Find("hideandsmash_idolgrp").GetComponent<StatueRotation>().StopAllCoroutines();
        GameObject.Find("hideandsmash_idolgrp").GetComponent<StatueRotation>().enabled = false;
        GameObject.Find("hideandsmash_idolgrp").GetComponent<Animator>().enabled = false;



        StartCoroutine(finishGame());

    }

    IEnumerator finishGame()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            idolPoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<HideSmash>().playerScore));
        }

        idolPoints.Sort();
        idolPoints.Reverse();

        distinct = idolPoints.Distinct(new ItemEqualityComparer()).ToList();

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("GAME OVER!");
        yield return new WaitForSeconds(2);

        for (int i = 0; i < idolPoints.Count; i++)
        {
            for (int j = 0; j < distinct.Count; j++)
            {
                if (idolPoints[i].playerPoints == distinct[j].playerPoints)
                {
                    Debug.Log(idolPoints[i].playerID + " is in " + (j + 1) + "place!");
                    if (j == 0)
                    {
                        if (idolPoints[i].playerID == "StevetheCapy(Clone)")
                        {
                            playerName1 = "Steve";
                        }
                        if (idolPoints[i].playerID == "HippotheFlower(Clone)")
                        {
                            playerName2 = "Hippo";
                        }
                        if (idolPoints[i].playerID == "ScooberttheNerd(Clone)")
                        {
                            playerName3 = "Scoobert";
                        }
                        if (idolPoints[i].playerID == "OctaviustheGangster(Clone)")
                        {
                            playerName4 = "Octavius";
                        }
                    }
                }
            }
        }

        gameover.SetText("The winner is:\n" + playerName1 + " " + playerName2 + " " + playerName3 + " " + playerName4);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("04-MinigameSelection");
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(2);
        gameover.gameObject.SetActive(true);
        int count = 3;

        while (count > 0)
        {
            gameover.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }
        gameover.SetText("START!");
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(false);

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<HideSmashControls>().enabled = true;
        }

        StartCoroutine(countDown(gameLength));
    }

    IEnumerator countDown(int seconds)
    {
        int count = seconds;

        while (count > -1)
        {
            countdown.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }
    }
}
