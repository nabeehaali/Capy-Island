using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class SledSceneSetup : MonoBehaviour
{
    public TMP_Text countdown;
    public int countdownTime;

    public int gameLength;

    float timePassed;
    bool gameDone = false;

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
            EndGame();
            countdown.SetText("Game Over!");
            gameDone = true;
        }
    }

    void EndGame()
    {
        Debug.Log("The game is over now");
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
