using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerSettingsTorch : MonoBehaviour
{
    public GameObject player1, player2, player3, player4;
    public GameObject spotLight;
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player1.transform.parent.gameObject.transform.position = new Vector3(0, 2.2f, 0);
        player1.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player1.GetComponent<TorchGame>().enabled = true;
        Instantiate(spotLight, player1.gameObject.transform);

        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player2.transform.parent.gameObject.transform.position = new Vector3(-10, 2.2f, 0);
        player2.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player2.GetComponent<TorchGame>().enabled = true;
        Instantiate(spotLight, player2.gameObject.transform);

        player3 = GameObject.FindGameObjectWithTag("Player 3");
        player3.transform.parent.gameObject.transform.position = new Vector3(10, 2.2f, 0);
        player3.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player3.GetComponent<TorchGame>().enabled = true;
        Instantiate(spotLight, player3.gameObject.transform);

        player4 = GameObject.FindGameObjectWithTag("Player 4");
        player4.transform.parent.gameObject.transform.position = new Vector3(20, 2.2f, 0);
        player4.transform.parent.gameObject.GetComponent<PlayerInput>().enabled = false;
        player4.GetComponent<TorchGame>().enabled = true;
        Instantiate(spotLight, player4.gameObject.transform);

        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(3);
        player1.GetComponent<PlayerInput>().enabled = true;
        player2.GetComponent<PlayerInput>().enabled = true;
        player3.GetComponent<PlayerInput>().enabled = true;
        player4.GetComponent<PlayerInput>().enabled = true;
    }
}
