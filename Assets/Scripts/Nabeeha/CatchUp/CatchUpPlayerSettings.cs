using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchUpPlayerSettings : MonoBehaviour
{
    public Transform waitingPos;

    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), waitingPos.position, 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), waitingPos.position, 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), waitingPos.position, 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), waitingPos.position, 0);
    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        //player.transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<CatchUpControls>().enabled = true;
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        player.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        player.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<CatchUp>().enabled = true;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<SledGame>().enabled = false;
        player.GetComponent<HideSmash>().enabled = false;
        player.GetComponent<AlligatorGame>().enabled = false;
        player.GetComponent<Rigidbody>().drag = 0;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        player.GetComponent<CapySoundTrigger>().moveType = "GROUND";

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

        player.transform.GetChild(3).gameObject.SetActive(true);
    }
}
