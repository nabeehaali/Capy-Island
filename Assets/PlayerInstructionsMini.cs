using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstructionsMini : MonoBehaviour
{
    public Animator transition;
    void Start()
    {
        transition.SetTrigger("FadeOut");

        if(GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-796, 0, 0), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-796, 0, 0), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 3") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-796, 0, 0), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Player 4") != null)
        {
            BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-796, 0, 0), 0);
            GameObject.Find("PlayerPanel").transform.GetChild(3).gameObject.SetActive(true);
        }
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
        player.transform.parent.gameObject.GetComponent<PlayerInstructions>().enabled = true;
        player.transform.parent.gameObject.GetComponent<HideSmashControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<TorchControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<AlligatorControls>().enabled = false;
        player.transform.parent.gameObject.GetComponent<SledControls>().enabled = false;
        player.transform.localPosition = Vector3.zero;
        
        player.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<HideSmash>().enabled = false;
        player.GetComponent<TorchGame>().enabled = false;
        player.GetComponent<AlligatorGame>().enabled = false;
        player.GetComponent<SledGame>().enabled = false;

        //switching geometry
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.GetChild(4).gameObject.SetActive(false);

        //switching colliders
        player.GetComponent<MeshCollider>().enabled = true;
        player.GetComponent<BoxCollider>().enabled = false;

        player.GetComponent<Rigidbody>().mass = 1;
        player.GetComponent<Rigidbody>().drag = 0;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}
