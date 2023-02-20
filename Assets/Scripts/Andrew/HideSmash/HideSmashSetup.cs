using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HideSmashSetup : MonoBehaviour
{
    float timePassed;
    public int gameLength = 45;

    public TMP_Text gameover;
    public TMP_Text countdown;
    public int countdownTime = 1;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    // Start is called before the first frame update
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(0,1.6f,0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(3, 1.6f, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(6, 1.6f, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-4, 1.6f, 0), 0);

        StartCoroutine(startGame());
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.GetComponent<TrailRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        //change the +1 to however long the start delay is
        if (timePassed > gameLength)
        {
            EndGame();
        }

        //p1Score.SetText("" + GameObject.FindGameObjectWithTag("Player 1").GetComponent<HideSmash>().playerScore + " Vases");
        //p2Score.SetText("" + GameObject.FindGameObjectWithTag("Player 2").GetComponent<HideSmash>().playerScore + " Vases");
        p3Score.SetText("" + GameObject.FindGameObjectWithTag("Player 3").GetComponent<HideSmash>().playerScore + " Vases");
        //p4Score.SetText("" + GameObject.FindGameObjectWithTag("Player 4").GetComponent<HideSmash>().playerScore + " Vases");
    }

    void EndGame()
    {
        
        ///////////// NEED TO COUNT POINTS //////////////////////

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
        SceneManager.LoadScene("HatProgressHideSmash");
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
