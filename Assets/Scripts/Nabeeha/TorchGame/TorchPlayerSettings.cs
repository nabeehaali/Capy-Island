using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TorchPlayerSettings : MonoBehaviour
{
    public GameObject player1, player2, player3, player4;
    public GameObject spotLight;
    GameObject[] allPlayers;

    public int countdownTime;

    void Start()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");

        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player1.transform.parent.gameObject.transform.position = new Vector3(0, 2.2f, 0);
        player1.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player1.GetComponent<TorchGame>().enabled = true;
        player1.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(spotLight, player1.gameObject.transform);

        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player2.transform.parent.gameObject.transform.position = new Vector3(-10, 2.2f, 0);
        player2.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player2.GetComponent<TorchGame>().enabled = true;
        player2.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(spotLight, player2.gameObject.transform);

        player3 = GameObject.FindGameObjectWithTag("Player 3");
        player3.transform.parent.gameObject.transform.position = new Vector3(10, 2.2f, 0);
        player3.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player3.GetComponent<TorchGame>().enabled = true;
        player3.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(spotLight, player3.gameObject.transform);

        player4 = GameObject.FindGameObjectWithTag("Player 4");
        player4.transform.parent.gameObject.transform.position = new Vector3(20, 2.2f, 0);
        player4.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player4.GetComponent<TorchGame>().enabled = true;
        player4.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(spotLight, player4.gameObject.transform);

        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(countdownTime);

        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].gameObject.transform.GetChild(0).GetComponent<PlayerInput>().enabled = true;
        }
    }
}
