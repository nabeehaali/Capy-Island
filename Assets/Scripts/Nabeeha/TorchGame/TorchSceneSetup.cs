using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TorchSceneSetup : MonoBehaviour
{
    public GameObject[] torchPlacements;
    public GameObject mainLight;
    public int gameLength;
    int option;

    float timePassed;
    bool gameDone = false;

    public TMP_Text countdown;
    public int countdownTime;
    void Start()
    {
        option = Random.Range(0, torchPlacements.Length);

        for(int i = 0; i < torchPlacements.Length; i++)
        {
            if (i == option)
            {
                torchPlacements[i].SetActive(true);
            }
        }

        StartCoroutine(startGame());
        //StartCoroutine(countDown(gameLength));
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        //change the +1 to however long the start delay is
        if (timePassed >= gameLength + countdownTime && !gameDone)
        {
            EndGame();
            countdown.SetText("Game Over!");
            gameDone = true;            
        }

    }

    void EndGame()
    {
        //update text
        //countdown.SetText("Game Over!");

        //turn on all lights
        mainLight.transform.Rotate(106, 0, 0);
        GameObject[] playerLights = GameObject.FindGameObjectsWithTag("Light");
        foreach(GameObject pLight in playerLights)
        {
            Destroy(pLight.gameObject);
        }


        //zoom camera in a bit

        List<TorchPoints> torchpoints = new List<TorchPoints>();

        torchpoints.Add(new TorchPoints("P1", GameObject.FindGameObjectsWithTag("P1Point").Length));
        torchpoints.Add(new TorchPoints("P2", GameObject.FindGameObjectsWithTag("P2Point").Length));
        torchpoints.Add(new TorchPoints("P3", GameObject.FindGameObjectsWithTag("P3Point").Length));
        torchpoints.Add(new TorchPoints("P4", GameObject.FindGameObjectsWithTag("P4Point").Length));

        torchpoints.Sort();
        torchpoints.Reverse();

        List<TorchPoints> distinct = torchpoints.Distinct(new ItemEqualityComparer()).ToList();

        for (int i = 0; i < torchpoints.Count; i++)
        {
            for (int j = 0; j < distinct.Count; j++)
            {
                if (torchpoints[i].playerPoints == distinct[j].playerPoints)
                {
                    //if (i == 0)
                    //{
                    //    countdown.SetText("" + torchpoints[0].playerPoints + " is the winner!");
                    //}
                    Debug.Log(torchpoints[i].playerID + " is in " + (j+1) + " place!");
                }
            }
        }
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

