using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSpawnManager : MonoBehaviour
{
    public Button play;
    public Transform[] spawnLocations;
    int playerCount;

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[playerInput.playerIndex].position;
    }

    private void Update()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount == 4)
        {
            play.interactable = true;
        }
    }

}
