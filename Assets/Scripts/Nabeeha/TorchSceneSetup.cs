using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TorchSceneSetup : MonoBehaviour
{
    public GameObject[] torchPlacements;
    public GameObject mainLight;
    public int gameLength;
    int option;

    float timePassed;
    bool gameDone = false;

    public TMP_Text countdown;
    void Start()
    {
        option = UnityEngine.Random.Range(1, 3);

        if (option == 1)
        {
            torchPlacements[0].SetActive(true);
        }
        else if (option == 2)
        {
            torchPlacements[1].SetActive(true);
        }
        else if (option == 3)
        {
            torchPlacements[2].SetActive(true);
        }

        StartCoroutine(countDown(gameLength));
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= gameLength && !gameDone)
        {
            EndGame();
            gameDone = true;            
        }

    }

    void EndGame()
    {
        //update text
        countdown.SetText("Game Over!");

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

        //foreach (TorchPoints torch in torchpoints)
        //{
        //    print(torch.playerID + " " + torch.playerPoints);
        //}

        for (int i = 0; i < torchpoints.Count; i++)
        {
            if (torchpoints[i].playerPoints == GameObject.FindGameObjectsWithTag("P1Point").Length && torchpoints[i].playerID == "P1")
            {
                //add delay, then make player jump
                Debug.Log("P1 is in " + (i + 1) + " place");
            }
            else if (torchpoints[i].playerPoints == GameObject.FindGameObjectsWithTag("P2Point").Length && torchpoints[i].playerID == "P2")
            {
                Debug.Log("P2 is in " + (i + 1) + " place");
            }
            else if (torchpoints[i].playerPoints == GameObject.FindGameObjectsWithTag("P3Point").Length && torchpoints[i].playerID == "P3")
            {
                Debug.Log("P3 is in " + (i + 1) + " place");
            }
            else if (torchpoints[i].playerPoints == GameObject.FindGameObjectsWithTag("P4Point").Length && torchpoints[i].playerID == "P4")
            {
                Debug.Log("P4 is in " + (i + 1) + " place");
            }
        }
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

