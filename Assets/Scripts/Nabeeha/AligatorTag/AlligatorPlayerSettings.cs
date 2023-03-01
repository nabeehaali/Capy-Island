using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlligatorPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-2f, -250, 599), 125);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(47, -250, 597), -125);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(1.31f, -250, 571), 35);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(42, -250, 567), -35);

    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false; 
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<AlligatorGame>().enabled = true;

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        player.GetComponent<Rigidbody>().drag = 1.7f;

        //disable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Hats")
            {
                player.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
