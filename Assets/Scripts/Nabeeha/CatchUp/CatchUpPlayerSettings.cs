using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchUpPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(0, 1.1f, -25), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(0, 1.1f, -41), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(0, 1.1f, -51f), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(0, 1.1f, -61f), 0);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.localPosition = Vector3.zero;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<CatchUp>().enabled = true;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<Rigidbody>().drag = 0;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}
