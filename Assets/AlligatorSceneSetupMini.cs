using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class AlligatorSceneSetupMini : MonoBehaviour
{
    public Animator transition;
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
    string playerName1 = "", playerName2 = "", playerName3 = "", playerName4 = "";
    void Start()
    {
        transition.SetTrigger("FadeOut");

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(57, -250, 582), -90);
            GameObject.Find("PlayerPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(57, -250, 582), -90);
            GameObject.Find("PlayerPanel").transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-6.5f, -250, 584), 90);
            GameObject.Find("PlayerPanel").transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-6.5f, -250, 584), 90);
            GameObject.Find("PlayerPanel").transform.GetChild(3).gameObject.SetActive(true);
        }

        players = GameObject.FindGameObjectsWithTag("Player");
        firstGator = GameObject.FindGameObjectWithTag("Alligator");
        firstGator.GetComponent<AlligatorBrain>().enabled = false;
        gatorSpawnPos = firstGator.transform.position;
        gatorSpawnRot = firstGator.transform.rotation;
        StartCoroutine(startGame());
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= gameLength + 6 && !gameDone)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<AlligatorControls>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerInput>().defaultActionMap = "UI";
            }
            wonByTimeGator = true;
            EndGame();
            gameDone = true;
        }

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
                    if (!gameDone)
                    {
                        // stopping the point increase + stopping players from moving
                        foreach (GameObject player in players)
                        {
                            //player.GetComponent<AlligatorControls>().hasEnded = true;
                            player.GetComponent<AlligatorControls>().enabled = false;
                            player.GetComponent<PlayerMovement>().enabled = false;
                            player.GetComponent<PlayerInput>().defaultActionMap = "UI";
                        }
                        EndGame();
                        gameDone = true;
                    }
                    gameRunning = false;
                    //break;
                }
            }
        }

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            p1State.SetText("" + GameObject.FindGameObjectWithTag("Player 1").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            p2State.SetText("" + GameObject.FindGameObjectWithTag("Player 2").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            p3State.SetText("" + GameObject.FindGameObjectWithTag("Player 3").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            p4State.SetText("" + GameObject.FindGameObjectWithTag("Player 4").transform.parent.GetComponent<AlligatorControls>().points + " Points");
        }
        
        
        
        
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        player.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        //player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<AlligatorGame>().enabled = true;

        player.GetComponent<CapySoundTrigger>().moveType = "WATER";

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        player.GetComponent<Rigidbody>().drag = 1.7f;

        //to avoid errors
        player.transform.parent.gameObject.GetComponent<HideSmashControls>().animator = player.transform.GetChild(0).GetComponent<Animator>();

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
        gameover.SetText("START!");
        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(false);

        firstGator.GetComponent<AlligatorBrain>().enabled = true;

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<AlligatorControls>().enabled = true;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<AlligatorControls>().hasStarted = true;

        }

        gameRunning = true;
        StartCoroutine(CountDown(gameLength));
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

        if (count <= 0)
        {
            EndGame();
        }
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

    IEnumerator FinishGame()
    {
        if (alligatorpoints.Count != 2)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                alligatorpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<AlligatorControls>().points));
            }
        }


        alligatorpoints.Sort();
        alligatorpoints.Reverse();

        distinct = alligatorpoints.Distinct(new ItemEqualityComparer()).ToList();

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("GAME OVER!");
        yield return new WaitForSeconds(2);

        GameObject.FindGameObjectWithTag("Alligator Crown").transform.parent = null;

        for (int i = 0; i < alligatorpoints.Count; i++)
        {
            for (int j = 0; j < distinct.Count; j++)
            {
                if (alligatorpoints[i].playerPoints == distinct[j].playerPoints)
                {
                    Debug.Log(alligatorpoints[i].playerID + " is in " + (j + 1) + "place!");
                    if (j == 0)
                    {
                        if (alligatorpoints[i].playerID == "StevetheCapy(Clone)")
                        {
                            playerName1 = "Steve";
                        }
                        if (alligatorpoints[i].playerID == "HippotheFlower(Clone)")
                        {
                            playerName2 = "Hippo";
                        }
                        if (alligatorpoints[i].playerID == "ScooberttheNerd(Clone)")
                        {
                            playerName3 = "Scoobert";
                        }
                        if (alligatorpoints[i].playerID == "OctaviustheGangster(Clone)")
                        {
                            playerName4 = "Octavius";
                        }
                    }
                }
            }
        }

        gameover.SetText("The winner is:\n" + playerName1 + " " + playerName2 + " " + playerName3 + " " + playerName4);

        yield return new WaitForSeconds(3);
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("04-MinigameSelection");
    }
}
