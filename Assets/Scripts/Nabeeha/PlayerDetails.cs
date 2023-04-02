using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerDetails : MonoBehaviour
{
    public GameObject[] playerOptions;
    public int playerID;
    public Vector3 startPos;
    
    void Start()
    {
        transform.position = startPos;

        if (playerID == 1)
        {
            Instantiate(playerOptions[0], gameObject.transform);
            GameObject.Find("P1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //GameObject.Find("P1").transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Connected");
        }
        else if (playerID == 2)
        {
            Instantiate(playerOptions[1], gameObject.transform);
            GameObject.Find("P2").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //GameObject.Find("P2").transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Connected");
        }
        else if (playerID == 3)
        {
            Instantiate(playerOptions[2], gameObject.transform);
            GameObject.Find("P3").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //GameObject.Find("P3").transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Connected");
        }
        else if (playerID == 4)
        {
            Instantiate(playerOptions[3], gameObject.transform);
            GameObject.Find("P4").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //GameObject.Find("P4").transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Connected");
        }


        //Gamepad.current.SetMotorSpeeds(0.25f, 1f);
        //StartCoroutine(StopRumble(0.5f, Gamepad.current));
        RumbleManager.instance.RumblePulse(0.25f, 1f, 0.25f);

        DontDestroyOnLoad(gameObject);
    }

    //IEnumerator StopRumble(float duration, Gamepad pad)
    //{
    //    float elapsedTime = 0f;
    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    pad.SetMotorSpeeds(0f, 0f);
    //}
}
