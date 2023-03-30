using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class AlligatorSceneSetup : MonoBehaviour
{
    // !!! this doesn't do anything, just remnants from trying to dodge some compiler errors :(
    public static bool wonByTimeGator;

    public GameObject gatorPrefab;
    public GameObject waterBoundPlane;
    GameObject firstGator;
    GameObject boundaryPlane;
    GameObject crown; // possibly used to place crown at real center?
    Vector3 gatorSpawnPos;
    Quaternion gatorSpawnRot;

    //public TMP_Text delayDisplay;

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
    bool secondGatorSpawned = false;

    public TMP_Text gameover;

    void Start()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        firstGator = GameObject.FindGameObjectWithTag("Alligator");
        gatorSpawnPos = firstGator.transform.position;
        gatorSpawnRot = firstGator.transform.rotation;

        StartCoroutine(startGame());
    }

    void FixedUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= gameLength + 6 && !gameDone)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<AlligatorControls>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
            }
            wonByTimeGator = true;
            EndGame();
            gameDone = true;
        }

        /*if (Time.time >= startDelay && !gameRunning)
        {
            //StartCoroutine(CountDown(gameLength));
            delayDisplay.text = "Go!";
            gameRunning = true;
            foreach (GameObject player in players)
            {
                player.GetComponent<AlligatorControls>().hasStarted = true;
                player.GetComponent<PlayerMovement>().enabled = true;
            }
            StartCoroutine(HideCountdownDisplay());

        } else if (Time.time < startDelay)
        {
            delayDisplay.text = (-Mathf.FloorToInt(Time.time - startDelay)).ToString();
        }*/

        if (gameRunning)
        {
            // checking the individual score of each player
            for (int i = 0; i < players.Length; i++)
            {
                int playerScore = players[i].GetComponent<AlligatorControls>().points;

                // if 75% of the score is done
                if (playerScore >= winScore * 0.75 && !secondGatorSpawned)
                {
                    SpawnSecondGator();
                }

                if (playerScore >= winScore)
                {
                    if(!gameDone)
                    {
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

        p1State.SetText("" + GameObject.FindGameObjectWithTag("Player 1").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p2State.SetText("" + GameObject.FindGameObjectWithTag("Player 2").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p3State.SetText("" + GameObject.FindGameObjectWithTag("Player 3").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        p4State.SetText("" + GameObject.FindGameObjectWithTag("Player 4").transform.parent.GetComponent<AlligatorControls>().points + " Points");
    }

    void EndGame()
    {
        

        StartCoroutine(FinishGame());
    }

    void SpawnSecondGator()
    {
        secondGatorSpawned = true;
        // maybe add some kind of animation?
        Debug.Log("Second gator spawned!");
        GameObject secondGator = Instantiate(gatorPrefab, gatorSpawnPos, gatorSpawnRot);
        secondGator.GetComponent<AlligatorBrain>().water = waterBoundPlane;
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(2);
        gameover.gameObject.SetActive(true);
        int count = 3;

        while (count > 0)
        {
            gameover.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }
        gameover.SetText("Start!");
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<AlligatorControls>().hasStarted = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<AlligatorControls>().hasStarted = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<AlligatorControls>().hasStarted = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<AlligatorControls>().hasStarted = true;

        gameRunning = true;
        StartCoroutine(CountDown(gameLength));
    }

    IEnumerator FinishGame()
    {
        if(alligatorpoints.Count != 4)
        {
            alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").transform.parent.GetComponent<AlligatorControls>().points));
            alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").transform.parent.GetComponent<AlligatorControls>().points));
            alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").transform.parent.GetComponent<AlligatorControls>().points));
            alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").transform.parent.GetComponent<AlligatorControls>().points));
        }
        

        alligatorpoints.Sort();
        alligatorpoints.Reverse();

        distinct = alligatorpoints.Distinct(new ItemEqualityComparer()).ToList();
        
        for (int i = 0; i < alligatorpoints.Count; i++)
        {
            Debug.Log("Before hat screen: " + alligatorpoints[i].playerID);
            Debug.Log("Before hat screen: " + alligatorpoints[i].playerPoints);
        }

        distinct = alligatorpoints.Distinct(new ItemEqualityComparer()).ToList();
        for (int i = 0; i < distinct.Count; i++)
        {
            Debug.Log("Before hat screen DISTINCT: " + distinct[i].playerID);
            Debug.Log("Before hat screen DISTINCT: " + distinct[i].playerPoints);
        }

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("Game Over!");
        yield return new WaitForSeconds(2);

        Destroy(GameObject.FindGameObjectWithTag("Alligator Crown"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator CountDown(int seconds)
    {
        int count = seconds;

        while (count > -1)
        {
            countdown.SetText("" + count);
            yield return new WaitForSeconds(1);
            count--;
        }

        if(count <= 0)
        {
            EndGame();
        }
    }

    /*IEnumerator HideCountdownDisplay()
    {
        yield return new WaitForSeconds(2);
        delayDisplay.enabled = false;
    }*/
}
