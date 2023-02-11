using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-267, 162, 769), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-300, 162, 787), 90);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-267, 162, 810), 180);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-233, 162, 787), -90);

    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 50;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = true;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<SledGame>().enabled = true;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.GetComponent<Rigidbody>().drag = 1;

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
