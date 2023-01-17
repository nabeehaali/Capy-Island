using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchUpPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(0, 2.18f, -25), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(0, 2.18f, -41), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(0, 2.18f, -51f), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(0, 2.18f, -61f), 0);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 10;
        player.transform.localPosition = Vector3.zero;
        player.GetComponent<CatchUp>().enabled = true;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<SledGame>().enabled = false;
    }
}
