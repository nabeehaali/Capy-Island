using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchUpPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(0, 0, -60), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(0, 0, -60), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(0, 0, -60), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(0, 0, -60), 0);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
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
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        //disable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Hats")
            {
                for (int k = 0; k < player.transform.GetChild(i).childCount; k++)
                {
                    player.transform.GetChild(i).GetChild(k).gameObject.SetActive(false);
                }
                
            }
        }
    }
}
