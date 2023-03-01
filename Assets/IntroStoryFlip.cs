using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStoryFlip : MonoBehaviour
{
    public GameObject[] panels;
    int index;
    void Start()
    {
        index = 0;        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("StartR"))
        {
            if (index == panels.Length-1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                index++;
            }
            
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if(i == index)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }

        
    }
}
