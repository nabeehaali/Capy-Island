using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NothingPlayerSettings : MonoBehaviour
{
    public GameObject player1, player2, player3, player4;
    //GameObject[] allPlayers;
    void Start()
    {
        //allPlayers = GameObject.FindGameObjectsWithTag("Player");

        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(0, 12, 23), 180);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(0, 12, -20), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(20, 12, -8.5f), -90);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-20, 12, -8.5f), 90);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<newMove>().enabled = true;

    }
}
