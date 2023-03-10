using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class TorchSceneSetup : MonoBehaviour
{
    public GameObject[] torchPlacements;
    public GameObject sceneLights;
    public GameObject[] accentLights = new GameObject[2];
    public int gameLength;
    int option;
    int lightsSum;

    float timePassed;
    bool gameDone = false;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    public static List<MinigamePoints> torchpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;
    void Start()
    {

        for(int i = 0; i < sceneLights.transform.childCount; i++)
        {
            sceneLights.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        option = Random.Range(0, torchPlacements.Length);

        for(int i = 0; i < torchPlacements.Length; i++)
        {
            if (i == option)
            {
                torchPlacements[i].SetActive(true);
            }
        }

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

        //checking which lights to activate depending on torches lit in scene
        lightsSum = GameObject.FindGameObjectsWithTag("P1Point").Length + GameObject.FindGameObjectsWithTag("P2Point").Length + GameObject.FindGameObjectsWithTag("P3Point").Length + GameObject.FindGameObjectsWithTag("P4Point").Length;
        if (lightsSum >= 1 && lightsSum < 4)
        {
            accentLights[0].SetActive(true);
        }
        else if (lightsSum >= 4 && lightsSum < 7)
        {
            accentLights[1].SetActive(true);
        }
        else if (lightsSum >= 7 && lightsSum < 9)
        {
            accentLights[2].SetActive(true);
        }
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
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < sceneLights.transform.childCount; i++)
        {
            sceneLights.transform.GetChild(i).gameObject.SetActive(true);
        }
        //mainLight.SetActive(false);
        //sceneLights.SetActive(true);
        GameObject[] playerLights = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject pLight in playerLights)
        {
            Destroy(pLight.gameObject);
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("HatProgressTorch");
        //GameObject.Find("NextScene").SetActive(true);
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

