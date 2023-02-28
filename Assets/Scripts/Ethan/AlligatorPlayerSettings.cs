using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlligatorPlayerSettings : MonoBehaviour
{
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-50, 0, 40), 135);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(50, 0, 40), 225);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-50, 0, -40), 45);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(50, 0, -40), -45);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false; // re-enabled after start countdown
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<AlligatorPlayerScript>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<CatchUp>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<AlligatorCapy>().enabled = true;
        // a little strange looking but lets the capy's sit "in" the water atm
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation 
            | RigidbodyConstraints.FreezePositionY;
        // creating water movement feel
        player.GetComponent<Rigidbody>().drag = 0.2f;

        // just for the capy's to be sitting "in" the water
        // nvm this doesnt work well lol
        //player.transform.gameObject.transform.position = new Vector3(0, -1);

        //disable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Hats")
            {
                player.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.2f);
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-50, 0, 40), 135);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(50, 0, 40), 225);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-50, 0, -40), 45);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(50, 0, -40), -45);
    }

}
