using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SledSceneSetup : MonoBehaviour
{
    public TMP_Text countdown;
    public int countdownTime;

    public int gameLength;

    float timePassed;
    bool gameDone = false;
       

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
        Debug.Log("The game is over now");
        //SceneManager.LoadScene("HatProgressSled");
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
