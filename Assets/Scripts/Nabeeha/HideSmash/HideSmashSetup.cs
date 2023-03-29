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
    bool gameDone = false;

    public TMP_Text gameover;
    public TMP_Text countdown;

    public static List<MinigamePoints> idolPoints = new List<MinigamePoints>();
    public static List<MinigamePoints> distinct;

    public TMP_Text p1Score, p2Score, p3Score, p4Score;

    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-16.37f, 2.08f, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-6.03f, 2.08f, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(4.9f, 2.08f, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(15.67f, 2.08f, 0), 0);

        StartCoroutine(startGame());
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().defaultActionMap = "Player";
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = false;
        player.GetComponent<HideSmash>().enabled = true;

        player.GetComponent<Rigidbody>().isKinematic = false;

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

        p1Score.SetText("" + GameObject.FindGameObjectWithTag("Player 1").GetComponent<HideSmash>().playerScore + " Vases");
        p2Score.SetText("" + GameObject.FindGameObjectWithTag("Player 2").GetComponent<HideSmash>().playerScore + " Vases");
        p3Score.SetText("" + GameObject.FindGameObjectWithTag("Player 3").GetComponent<HideSmash>().playerScore + " Vases");
        p4Score.SetText("" + GameObject.FindGameObjectWithTag("Player 4").GetComponent<HideSmash>().playerScore + " Vases");
    }

    void EndGame()
    {

        //make sure to include this in the other games
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().speed = 0;
            Destroy(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<HideSmashControls>());
            //set animation bool to false?????
        }


        StartCoroutine(finishGame());

    }

    IEnumerator finishGame()
    {
        idolPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 1").name, GameObject.FindGameObjectWithTag("Player 1").GetComponent<HideSmash>().playerScore));
        idolPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 2").name, GameObject.FindGameObjectWithTag("Player 2").GetComponent<HideSmash>().playerScore));
        idolPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 3").name, GameObject.FindGameObjectWithTag("Player 3").GetComponent<HideSmash>().playerScore));
        idolPoints.Add(new MinigamePoints(GameObject.FindGameObjectWithTag("Player 4").name, GameObject.FindGameObjectWithTag("Player 4").GetComponent<HideSmash>().playerScore));

        idolPoints.Sort();
        idolPoints.Reverse();

        distinct = idolPoints.Distinct(new ItemEqualityComparer()).ToList();

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

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;

        GameObject.FindGameObjectWithTag("Player 1").transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 2").transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 3").transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = true;
        GameObject.FindGameObjectWithTag("Player 4").transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = true;

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
