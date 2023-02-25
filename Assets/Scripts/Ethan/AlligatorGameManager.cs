using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AlligatorGameManager : MonoBehaviour
{
    public float startDelay = 5f; // default value, can be tweaked
    public int winScore = 10;
    GameObject[] players;
    public TextMeshProUGUI[] displays = new TextMeshProUGUI[4];
    bool gameRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        // displays are entered as an array in the Unity editor 
        foreach(TextMeshProUGUI display in displays)
        {
            // seems redundant, extra code that makes sure score matches the player bc order could not be 1:1
            string displayName = display.name.Replace(" Score", string.Empty);
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].transform.GetChild(0).tag == displayName)
                {
                    // adding the display to the player component
                    players[i].GetComponent<AlligatorPlayerScript>().display = display;
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time >= startDelay && !gameRunning)
        {
            gameRunning = true;
            foreach(GameObject player in players)
            {
                player.GetComponent<AlligatorPlayerScript>().hasStarted = true;
                player.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        if (gameRunning)
        {
            // checking the individual score of each player
            for (int i = 0; i < players.Length; i++)
            {
                int playerScore = players[i].GetComponent<AlligatorPlayerScript>().points;
                if (playerScore >= winScore)
                {
                    Debug.Log("PLAYER WIN!");
                    // stopping the point increase + stopping players from moving
                    foreach (GameObject player in players)
                    {
                        player.GetComponent<AlligatorPlayerScript>().hasEnded = true;
                        player.GetComponent<PlayerMovement>().enabled = false;
                    }
                    gameRunning = false;
                    EndGame();
                    break;
                }
            }
        }
        
    }

    void EndGame()
    {
        // FOR NABEEHA: this is the function that can be used to switch to the score scene!
        // really quick and ugly, but here's a loop that you could use to get the alligator tag points + player ids(?)
        for (int i = 0; i < players.Length; i++)
        {
            int playerScore = players[i].GetComponent<AlligatorPlayerScript>().points;
            int playerID = players[i].GetComponent<PlayerDetails>().playerID;
        }
    }
}
