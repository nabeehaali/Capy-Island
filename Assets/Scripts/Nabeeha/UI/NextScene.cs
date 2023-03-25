using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("NextScene"))
        {
            //destroy hinje joints on all players hats
            //if hats have a hinjejoint component

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
