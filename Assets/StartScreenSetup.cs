using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenSetup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("StartL") && Input.GetButtonDown("StartR"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}