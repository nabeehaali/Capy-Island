using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SledSceneSetup : MonoBehaviour
{
    public TMP_Text countdown;
    public int countdownTime;

    public int gameLength;

    float timePassed;
    bool gameDone = false;

    public GameObject p1State, p2State, p3State, p4State;

    public static List<MinigamePoints> sledpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> sleddistinct;
    void Start()
    {
        StartCoroutine(startGame());
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        //checking is time is up
        if (timePassed >= gameLength + countdownTime && !gameDone)
        {
            EndGame();
            countdown.SetText("Game Over!");
            gameDone = true;
        }

        //checking if there is one player standing on ice
        if(SledGame.ranking == 1)
        {
            //TODO: add delay
            EndGame();
            countdown.SetText("Game Over!");
            gameDone = true;
        }

        //UI updates
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<SledGame>().inWater == true)
            {
                string tag = GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.tag;
                if(tag == "Player 1")
                {
                    p1State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p1State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + sledpoints.Find(x => x.playerID.Contains("StevetheCapy")).playerPoints);
                }
                else if (tag == "Player 2")
                {
                    p2State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p2State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + sledpoints.Find(x => x.playerID.Contains("HippotheFlower")).playerPoints);
                }
                else if (tag == "Player 3")
                {
                    p3State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p3State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + sledpoints.Find(x => x.playerID.Contains("ScooberttheNerd")).playerPoints);
                }
                else if (tag == "Player 4")
                {
                    p4State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p4State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("" + sledpoints.Find(x => x.playerID.Contains("OctaviustheGangster")).playerPoints);
                }

            }
        }
    }

    void EndGame()
    {
        //Debug.Log("The game is over now");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<SledGame>().inWater == false)
            {
                sledpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.name, SledGame.ranking));
            }
        }

        sledpoints.Sort();

        sleddistinct = sledpoints.Distinct(new ItemEqualityComparer()).ToList();

        SceneManager.LoadScene("HatProgressSled");
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
