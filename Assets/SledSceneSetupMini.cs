using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SledSceneSetupMini : MonoBehaviour
{
    public Animator transition;

    public static bool wonByTime;
    public static bool wonbyLastMan;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public int gameLength;

    float timePassed;
    bool gameDone = false;

    //bool test;

    public GameObject p1State, p2State, p3State, p4State;
    string playerName1 = "", playerName2 = "", playerName3 = "", playerName4 = "";

    public static List<MinigamePoints> sledpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> sleddistinct;
    void Start()
    {
        transition.SetTrigger("FadeOut");

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-267, 165, 769), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-267, 165, 769), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-267, 165, 810), 180);
            GameObject.Find("PlayerPanel").transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-267, 165, 810), 180);
            GameObject.Find("PlayerPanel").transform.GetChild(3).gameObject.SetActive(true);
        }

        StartCoroutine(startGame());
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        
            //checking is time is up
            if (timePassed >= gameLength + 6 && !gameDone)
        {
            wonByTime = true;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<SledGame>().inWater == false)
                {
                    sledpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.name, 1));
                }
            }
            EndGame();
            gameDone = true;
        }

        //checking if there is one player standing on ice
        if (sledpoints.Count == 1 && !gameDone)
        {
            wonbyLastMan = true;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<SledGame>().inWater == false)
                {
                    sledpoints.Add(new MinigamePoints(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.name, 1));
                }
            }
            EndGame();
            gameDone = true;
        }

        //UI updates

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {

            if (GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<SledGame>().inWater == true)
            {
                //Destroy(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
                string tag = GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.tag;
                if (tag == "Player 1")
                {
                    p1State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p1State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead");
                }
                else if (tag == "Player 2")
                {
                    p2State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p2State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead");
                }
                else if (tag == "Player 3")
                {
                    p3State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p3State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead");
                }
                else if (tag == "Player 4")
                {
                    p4State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p4State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead");
                }
            }

        }


    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 50;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<SledGame>().enabled = true;

        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        player.transform.GetChild(0).transform.localRotation = Quaternion.identity;

        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.GetComponent<Rigidbody>().drag = 1;

        //switching geometry
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.GetChild(4).gameObject.SetActive(true);

        //switching colliders
        player.GetComponent<MeshCollider>().enabled = false;
        player.GetComponent<BoxCollider>().enabled = true;

        //setting movement sound set
        player.GetComponent<CapySoundTrigger>().moveType = "ICE";

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

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<SledControls>().enabled = true;
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
        //Debug.Log("The game is over now");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }


        GameObject.Find("icePlatformPieces").GetComponent<SledIceberg>().StopCoroutine("dropPiece");
        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        sledpoints.Sort();

        sleddistinct = sledpoints.Distinct(new ItemEqualityComparer()).ToList();

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("GAME OVER!");
        yield return new WaitForSeconds(2);

        for (int i = 0; i < sledpoints.Count; i++)
        {
            for (int j = 0; j < sleddistinct.Count; j++)
            {
                if (sledpoints[i].playerPoints == sleddistinct[j].playerPoints)
                {
                    Debug.Log(sledpoints[i].playerID + " is in " + (j + 1) + "place!");
                    if (j == 0)
                    {
                        if (sledpoints[i].playerID == "StevetheCapy(Clone)")
                        {
                            playerName1 = "Steve";
                        }
                        if (sledpoints[i].playerID == "HippotheFlower(Clone)")
                        {
                            playerName2 = "Hippo";
                        }
                        if (sledpoints[i].playerID == "ScooberttheNerd(Clone)")
                        {
                            playerName3 = "Scoobert";
                        }
                        if (sledpoints[i].playerID == "OctaviustheGangster(Clone)")
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
