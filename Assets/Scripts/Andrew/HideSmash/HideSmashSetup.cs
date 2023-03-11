using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HideSmashSetup2 : MonoBehaviour
{
    float timePassed;
    public int gameLength = 45;

    public TMP_Text gameover;
    public TMP_Text countdown;
    public int countdownTime = 1;
    List<GameObject> players = new List<GameObject>();
    List<int> playersVals = new List<int>();

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    // Start is called before the first frame update
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(12, 1f, -8), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(4, 1f, -10), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-8, 1f, -10), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-16, 1f, -10), 0);

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

        p1Score.SetText("" + GameObject.FindGameObjectWithTag("Player 1").GetComponent<HideSmash>().playerScore + " Vases");
        p2Score.SetText("" + GameObject.FindGameObjectWithTag("Player 2").GetComponent<HideSmash>().playerScore + " Vases");
        p3Score.SetText("" + GameObject.FindGameObjectWithTag("Player 3").GetComponent<HideSmash>().playerScore + " Vases");
        p4Score.SetText("" + GameObject.FindGameObjectWithTag("Player 4").GetComponent<HideSmash>().playerScore + " Vases");
    }

    void EndGame()
    {

        players.Add(GameObject.FindGameObjectWithTag("Player 1"));
        players.Add(GameObject.FindGameObjectWithTag("Player 2"));
        players.Add(GameObject.FindGameObjectWithTag("Player 3"));
        players.Add(GameObject.FindGameObjectWithTag("Player 4"));

        for (int i = 0; i < players.Count(); i++)
        {
            var item = players[i];
            var currentIndex = i;

            while (currentIndex > 0 && players[currentIndex - 1].GetComponent<HideSmash>().playerScore > item.GetComponent<HideSmash>().playerScore)
            {
                players[currentIndex] = players[currentIndex - 1];
                currentIndex--;
            }
            
            players[currentIndex] = item;
        }

        StartCoroutine(finishGame());

    }

    IEnumerator finishGame()
    {
        gameover.gameObject.SetActive(true);
        gameover.SetText("Winner is: " + players.Last().tag);
        yield return new WaitForSeconds(5);
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
