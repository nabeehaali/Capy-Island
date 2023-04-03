using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SledSceneSetup : MonoBehaviour
{
    public static bool wonByTime;
    public static bool wonbyLastMan;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public int gameLength;

    float timePassed;
    bool gameDone = false;

    //bool test;

    public GameObject p1State, p2State, p3State, p4State;

    public static List<MinigamePoints> sledpoints = new List<MinigamePoints>();
    public static List<MinigamePoints> sleddistinct;
    void Start()
    {
        //test = true;
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
        if (sledpoints.Count == 3 && !gameDone)
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
                Destroy(GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
                string tag = GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).gameObject.tag;
                if (tag == "Player 1")
                {
                    p1State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p1State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead (" + sledpoints.Find(x => x.playerID.Contains("Steve")).playerPoints + ")");
                }
                else if (tag == "Player 2")
                {
                    p2State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p2State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead (" + sledpoints.Find(x => x.playerID.Contains("Hippo")).playerPoints + ")");
                }
                else if (tag == "Player 3")
                {
                    p3State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p3State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead (" + sledpoints.Find(x => x.playerID.Contains("Scoobert")).playerPoints + ")");
                }
                else if (tag == "Player 4")
                {
                    p4State.GetComponent<Image>().CrossFadeAlpha(0.5f, 1.0f, true);
                    p4State.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText("Dead (" + sledpoints.Find(x => x.playerID.Contains("Octavius")).playerPoints + ")");
                }
            }

        }


    }

    void EndGame()
    {
        //Debug.Log("The game is over now");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            if (GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<SledControls>())
            {
                Destroy(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<SledControls>());
            }

        }


        GameObject.Find("icePlatformPieces").GetComponent<SledIceberg>().StopCoroutine("dropPiece");
        StartCoroutine(finishGame());
    }

    IEnumerator finishGame()
    {
        sledpoints.Sort();

        for (int i = 0; i < sledpoints.Count; i++)
        {
            Debug.Log("Before hat screen: " + sledpoints[i].playerID);
            Debug.Log("Before hat screen: " + sledpoints[i].playerPoints);
        }

        sleddistinct = sledpoints.Distinct(new ItemEqualityComparer()).ToList();
        for (int i = 0; i < sleddistinct.Count; i++)
        {
            Debug.Log("Before hat screen DISTINCT: " + sleddistinct[i].playerID);
            Debug.Log("Before hat screen DISTINCT: " + sleddistinct[i].playerPoints);
        }

        yield return new WaitForSeconds(1);
        gameover.gameObject.SetActive(true);
        gameover.SetText("Game Over!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            //GameObject.FindGameObjectsWithTag("Player")[i].transform.parent.gameObject.GetComponent<SledControls>().enabled = true;
        }

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<SledControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<SledControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<SledControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<SledControls>().enabled = true;

        //countdown.SetText("Start!");
        //yield return new WaitForSeconds(countdownTime);
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
