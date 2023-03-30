using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(100, 0, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(95, 0, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(90, 0, 0), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(105, 0, 0), 0);

    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 0;
        //player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = false;
        //player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().useGravity = false;

        player.GetComponent<Animator>().enabled = false;

        ////disable hats
        //for (int i = 0; i < player.transform.childCount; i++)
        //{
        //    if (player.transform.GetChild(i).name == "Hats")
        //    {
        //        player.transform.GetChild(i).gameObject.SetActive(false);
        //    }
        //}

    }
}
