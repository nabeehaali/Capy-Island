using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerSpawnManager : MonoBehaviour
{
    public TMP_Text instruction;
    public Button play, back;
    public Transform[] spawnLocations;
    int playerCount;

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[playerInput.playerIndex].position;
        // doesn't work, possibly loop back later
        //playerInput.gameObject.GetComponentInChildren<CapySoundTrigger>().PlayChirp();

        //playerInput.gameObject.GetComponent<PlayerMovement>().enabled = true;

    }

    private void Update()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount == 4)
        {
            back.interactable = true;
            play.interactable = true;
            instruction.SetText("Ready to play?");

            /*for (int i = 0; i < playerCount; i++)
            {
                //GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerInput>().defaultActionMap = "UI";
               // GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerMovement>().enabled = false;
                GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerInput>().actions.FindActionMap("Player").Disable();
                GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerInput>().actions.FindActionMap("UI").Enable();
            }*/
        }
    }

}
