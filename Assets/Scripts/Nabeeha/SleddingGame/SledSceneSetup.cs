using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SledSceneSetup : MonoBehaviour
{
    public TMP_Text countdown;
    public int countdownTime;

    public int gameLength;

    float timePassed;
    bool gameDone = false;

    public List<string> playerOrder = new List<string>();

    void Start()
    {
        StartCoroutine(startGame());
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= gameLength + countdownTime && !gameDone)
        {
            EndGame();
            countdown.SetText("Game Over!");
            gameDone = true;
        }
    }

    void EndGame()
    {
        playerOrder.Reverse();

        for(int i = 0; i <playerOrder.Count; i++)
        {
            Debug.Log(playerOrder[i]);
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
