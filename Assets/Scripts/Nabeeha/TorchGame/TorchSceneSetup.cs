using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class TorchSceneSetup : MonoBehaviour
{
    public GameObject[] torchPlacements;
    public GameObject mainLight;
    public GameObject sceneLights;
    public int gameLength;
    int option;

    float timePassed;
    bool gameDone = false;

    public TMP_Text gameover;
    public TMP_Text countdown;
    public int countdownTime = 1;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    public static List<MinigamePoints> torchpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;
    void Start()
    {
        sceneLights.SetActive(false);
        option = Random.Range(0, torchPlacements.Length);

        for(int i = 0; i < torchPlacements.Length; i++)
        {
            if (i == option)
            {
                torchPlacements[i].SetActive(true);
            }
        }

        //StartCoroutine(startGame());
        StartCoroutine(countDown(gameLength));
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        //change the +1 to however long the start delay is
        if (timePassed > gameLength && !gameDone)
        {
            EndGame();
            gameDone = true;            
        }

        //player score UI
        p1Score.SetText("" + GameObject.FindGameObjectsWithTag("P1Point").Length + " Torches");
        p2Score.SetText("" + GameObject.FindGameObjectsWithTag("P2Point").Length + " Torches");
        p3Score.SetText("" + GameObject.FindGameObjectsWithTag("P3Point").Length + " Torches");
        p4Score.SetText("" + GameObject.FindGameObjectsWithTag("P4Point").Length + " Torches");

    }

    void EndGame()
    {
        torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectsWithTag("P1Point").Length));
        torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectsWithTag("P2Point").Length));
        torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectsWithTag("P3Point").Length));
        torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectsWithTag("P4Point").Length));

        torchpoints.Sort();
        torchpoints.Reverse();

        distinct = torchpoints.Distinct(new ItemEqualityComparer()).ToList();

        
        StartCoroutine(finishGame());

        //for (int i = 0; i < torchpoints.Count; i++)
        //{
        //    for (int j = 0; j < distinct.Count; j++)
        //    {
        //        if (torchpoints[i].playerPoints == distinct[j].playerPoints)
        //        {
        //            //if (i == 0)
        //            //{
        //            //    countdown.SetText("" + torchpoints[0].playerPoints + " is the winner!");
        //            //}
        //            Debug.Log(torchpoints[i].playerID + " is in " + (j + 1) + " place!");
        //        }
        //    }
        //}
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        mainLight.SetActive(false);
        sceneLights.SetActive(true);
        GameObject[] playerLights = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject pLight in playerLights)
        {
            Destroy(pLight.gameObject);
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("HatProgressTorch");
    }

    IEnumerator startGame()
    {
        countdown.SetText("Start!");
        yield return new WaitForSeconds(countdownTime);
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

