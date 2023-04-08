using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class TorchSceneSetupMini : MonoBehaviour
{
    public GameObject spotLight;
    public Animator transition;

    public GameObject[] torchPlacements;
    public GameObject sceneLights;
    public GameObject[] accentLights = new GameObject[2];
    public int gameLength;
    int option;
    int lightsSum;

    float timePassed;
    bool gameDone = false;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    public static List<MinigamePoints> torchpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    string playerName1 = "", playerName2 = "", playerName3 = "", playerName4 = "";
    void Start()
    {
        transition.SetTrigger("FadeOut");

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-150, 121.5f, -126), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-150, 121.5f, -126), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-140, 121.5f, -126), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-140, 121.5f, -126), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(3).gameObject.SetActive(true);
        }

        for (int i = 0; i < sceneLights.transform.childCount; i++)
        {
            sceneLights.transform.GetChild(i).gameObject.SetActive(false);
        }

        option = Random.Range(0, torchPlacements.Length);

        for (int i = 0; i < torchPlacements.Length; i++)
        {
            if (i == option)
            {
                torchPlacements[i].SetActive(true);
            }
        }

        StartCoroutine(startGame());
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        player.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        player.GetComponent<TorchGame>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false;

        //setting movement sound set
        player.GetComponent<CapySoundTrigger>().moveType = "GROUND";

        Instantiate(spotLight, player.gameObject.transform);

        //to avoid errors
        player.transform.parent.gameObject.GetComponent<HideSmashControls>().animator = player.transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        //checking is time is up (+6 for starting time delay)
        if (timePassed >= gameLength + 6 && !gameDone)
        {
            EndGame();
            gameDone = true;
        }
        
        //player score UI
        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            p1Score.SetText("" + GameObject.FindGameObjectsWithTag("P1Point").Length + " Torches");
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            p2Score.SetText("" + GameObject.FindGameObjectsWithTag("P2Point").Length + " Torches");
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            p3Score.SetText("" + GameObject.FindGameObjectsWithTag("P3Point").Length + " Torches");
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            p4Score.SetText("" + GameObject.FindGameObjectsWithTag("P4Point").Length + " Torches");
        }
               

        //checking which lights to activate depending on torches lit in scene
        lightsSum = GameObject.FindGameObjectsWithTag("P1Point").Length + GameObject.FindGameObjectsWithTag("P2Point").Length + GameObject.FindGameObjectsWithTag("P3Point").Length + GameObject.FindGameObjectsWithTag("P4Point").Length;
        if (lightsSum >= 1 && lightsSum < 4)
        {
            accentLights[0].SetActive(true);
        }
        else if (lightsSum >= 4 && lightsSum < 7)
        {
            accentLights[1].SetActive(true);
        }
        else if (lightsSum >= 7 && lightsSum < 9)
        {
            accentLights[2].SetActive(true);
        }
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

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<TorchControls>().enabled = true;
        }

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
    void EndGame()
    {
        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        //make sure to include this in the other games
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerInput>().defaultActionMap = "UI";
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).tag == "Player 1")
            {
                torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("P1Point").Length));
            }
            else if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).tag == "Player 2")
            {
                torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("P2Point").Length));
            }
            else if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).tag == "Player 3")
            {
                torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("P3Point").Length));
            }
            else if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).tag == "Player 4")
            {
                torchpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).name, GameObject.FindGameObjectsWithTag("P4Point").Length));
            }

        }
        torchpoints.Sort();
        torchpoints.Reverse();

        distinct = torchpoints.Distinct(new ItemEqualityComparer()).ToList();

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("GAME OVER!");
        yield return new WaitForSeconds(2);
        for (int i = 0; i < sceneLights.transform.childCount; i++)
        {
            sceneLights.transform.GetChild(i).gameObject.SetActive(true);
        }
        //mainLight.SetActive(false);
        //sceneLights.SetActive(true);
        GameObject[] playerLights = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject pLight in playerLights)
        {
            Destroy(pLight.gameObject);
        }

        for (int i = 0; i < torchpoints.Count; i++)
        {
            for (int j = 0; j < distinct.Count; j++)
            {
                if (torchpoints[i].playerPoints == distinct[j].playerPoints)
                {
                    Debug.Log(torchpoints[i].playerID + " is in " + (j + 1) + "place!");
                    if (j == 0)
                    {
                        if (torchpoints[i].playerID == "StevetheCapy(Clone)")
                        {
                            playerName1 = "Steve";
                        }
                        if (torchpoints[i].playerID == "HippotheFlower(Clone)")
                        {
                            playerName2 = "Hippo";
                        }
                        if (torchpoints[i].playerID == "ScooberttheNerd(Clone)")
                        {
                            playerName3 = "Scoobert";
                        }
                        if (torchpoints[i].playerID == "OctaviustheGangster(Clone)")
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
