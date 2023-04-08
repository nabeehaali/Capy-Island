using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDetails : MonoBehaviour
{
    public GameObject[] playerOptions;
    public int playerID;
    public Vector3 startPos;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        transform.position = startPos;

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if(sceneName == "03-CharacterSelectMini")
        {
            if(playerID == 1)
            {
                int randCharacter = Random.Range(1, 3);
                if(randCharacter == 1)
                {
                    Instantiate(playerOptions[0], gameObject.transform);
                    GameObject.Find("P1Banner").transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("UIManager").GetComponent<CharacterUIManager>().characters[0];
                    GameObject.Find("P1Banner").transform.GetChild(1).GetComponent<TMP_Text>().SetText("P1 - STEVE");
                }
                else if (randCharacter == 2)
                {
                    Instantiate(playerOptions[1], gameObject.transform);
                    GameObject.Find("P1Banner").transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("UIManager").GetComponent<CharacterUIManager>().characters[1];
                    GameObject.Find("P1Banner").transform.GetChild(1).GetComponent<TMP_Text>().SetText("P2 - HIPPO");
                }
            }
            else if (playerID == 2)
            {
                int randCharacter = Random.Range(1, 3);
                if (randCharacter == 1)
                {
                    Instantiate(playerOptions[2], gameObject.transform);
                    GameObject.Find("P2Banner").transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("UIManager").GetComponent<CharacterUIManager>().characters[2];
                    GameObject.Find("P2Banner").transform.GetChild(1).GetComponent<TMP_Text>().SetText("P3 - SCOOBERT");
                }
                else if (randCharacter == 2)
                {
                    Instantiate(playerOptions[3], gameObject.transform);
                    GameObject.Find("P2Banner").transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("UIManager").GetComponent<CharacterUIManager>().characters[3];
                    GameObject.Find("P2Banner").transform.GetChild(1).GetComponent<TMP_Text>().SetText("P4 - OCTAVIUS");
                }
            }
        }
        else
        {
            if (playerID == 1)
            {
                Instantiate(playerOptions[0], gameObject.transform);
                GameObject.Find("P1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (playerID == 2)
            {
                Instantiate(playerOptions[1], gameObject.transform);
                GameObject.Find("P2").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (playerID == 3)
            {
                Instantiate(playerOptions[2], gameObject.transform);
                GameObject.Find("P3").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (playerID == 4)
            {
                Instantiate(playerOptions[3], gameObject.transform);
                GameObject.Find("P4").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
        

        GetComponent<PlayerMovement>().rumbleFunction(0.25f, 1f, 0.25f);

        DontDestroyOnLoad(gameObject);
    }
}
