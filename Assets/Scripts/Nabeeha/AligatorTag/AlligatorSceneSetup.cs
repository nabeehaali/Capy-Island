using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class AlligatorSceneSetup : MonoBehaviour
{
    // !!! this doesn't do anything, just remnants from trying to dodge some compiler errors :(

    public TMP_Text countdown;
    public int gameLength;

    float timePassed;
    bool gameDone = false;

    public TMP_Text p1State, p2State, p3State, p4State;

    public static List<MinigamePoints> alligatorpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    public float startDelay = 0f; // default value, can be tweaked
    public int winScore = 10;
    GameObject[] players;

    bool gameRunning = false;

    public TMP_Text gameover;

    void Start()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        /*
        // displays are entered as an array in the Unity editor 
        foreach (TextMeshProUGUI display in displays)
        {
            // seems redundant, extra code that makes sure score matches the player bc order could not be 1:1
            string displayName = display.name.Replace(" Score", string.Empty);
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].transform.GetChild(0).tag == displayName)
                {
                    // adding the display to the player component
                    players[i].GetComponent<AlligatorControls>().display = display;
                    break;
                }
            }
        }*/

        StartCoroutine(countDown(gameLength));

        //place hat somewhere random in the environment????
    }

    void FixedUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed > gameLength && !gameDone)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<AlligatorControls>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
            }
            EndGame();
            gameDone = true;
        }

        p1State.SetText("" + GameObject.FindGameObjectWithTag("Player 1").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p2State.SetText("" + GameObject.FindGameObjectWithTag("Player 2").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p3State.SetText("" + GameObject.FindGameObjectWithTag("Player 3").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p4State.SetText("" + GameObject.FindGameObjectWithTag("Player 4").transform.parent.GetComponent<AlligatorControls>().points + " Points");

        if (Time.time >= startDelay && !gameRunning)
        {
            gameRunning = true;
            foreach (GameObject player in players)
            {
                player.GetComponent<AlligatorControls>().hasStarted = true;
                player.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        if (gameRunning)
        {
            // checking the individual score of each player
            for (int i = 0; i < players.Length; i++)
            {
                int playerScore = players[i].GetComponent<AlligatorControls>().points;
                if (playerScore >= winScore)
                {
                    if(!gameDone)
                    {
                        Debug.Log("PLAYER WIN!");
                        // stopping the point increase + stopping players from moving
                        foreach (GameObject player in players)
                        {
                            //player.GetComponent<AlligatorControls>().hasEnded = true;
                            player.GetComponent<AlligatorControls>().enabled = false;
                            player.GetComponent<PlayerMovement>().enabled = false;
                        }
                        EndGame();
                        gameDone = true;
                    }
                    gameRunning = false;
                    //break;
                }
            }
        }

    }

    void EndGame()
    {
        alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").transform.parent.GetComponent<AlligatorControls>().points));
        alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").transform.parent.GetComponent<AlligatorControls>().points));
        alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").transform.parent.GetComponent<AlligatorControls>().points));
        alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").transform.parent.GetComponent<AlligatorControls>().points));

        alligatorpoints.Sort();
        alligatorpoints.Reverse();

        distinct = alligatorpoints.Distinct(new ItemEqualityComparer()).ToList();

        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        Destroy(GameObject.FindGameObjectWithTag("Alligator Crown"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
