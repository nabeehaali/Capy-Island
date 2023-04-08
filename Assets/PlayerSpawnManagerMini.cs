using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerSpawnManagerMini : MonoBehaviour
{
    public TMP_Text instruction;
    public Button play, back;
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
        if (playerCount == 2)
        {
            back.interactable = true;
            play.interactable = true;
            instruction.SetText("Ready to play?");
        }
    }
}
