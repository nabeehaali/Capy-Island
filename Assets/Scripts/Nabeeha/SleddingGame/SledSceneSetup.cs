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
        
        //check which players have SetActive = true
        //for loop number of GO with playerTag = player that are active
        //if the child of that GO has tag of 'Player X'
        //Debug.Log "player X
        // OR use an if statement to check the child gameObject Tag
        //also need to check order of when players fall off the iceberg
        // ^^ this would go in the Sled Game script -> dump GO in array and it will generate an order
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
