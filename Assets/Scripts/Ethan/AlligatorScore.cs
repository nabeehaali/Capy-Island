using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AlligatorScore : MonoBehaviour
{
    public int timeout = 10;
    public int winScore = 10;
    GameObject[] players;
    bool gameRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameRunning)
        {
            for (int i = 0; i < players.Length; i++)
            {
                int playerScore = players[i].GetComponent<AlligatorPlayerScript>().points;
                if (playerScore >= winScore)
                {
                    Debug.Log("PLAYER WIN!");
                    gameRunning = false;
                    EndGame();
                    break;
                }
            }
        }
        
    }

    void EndGame()
    {
        // prob wanna trigger some kinda animation / transition here

    }
}
