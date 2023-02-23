using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TorchPlayerSettings : MonoBehaviour
{
    //public GameObject player1, player2, player3, player4;
    public GameObject spotLight;
    void Start()
    {
        BeginGame(GameObject.FindGameObjectWithTag("Player 1"), new Vector3(-160, 122, -126), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 2"), new Vector3(-150, 122, -126), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 3"), new Vector3(-140, 122, -126), 0);
        BeginGame(GameObject.FindGameObjectWithTag("Player 4"), new Vector3(-130, 122, -126), 0);

    }

    private void BeginGame(GameObject player, Vector3 startPos, float yAngle)
    {
        player.transform.parent.gameObject.transform.position = startPos;
        player.transform.parent.gameObject.transform.rotation = Quaternion.identity;
        player.transform.parent.gameObject.transform.Rotate(0, yAngle, 0, Space.Self);
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        player.transform.parent.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 20;
        player.GetComponent<TorchGame>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(spotLight, player.gameObject.transform);

        //disable hats
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if(player.transform.GetChild(i).name == "Hats")
            {
                player.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
