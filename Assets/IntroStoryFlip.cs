using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStoryFlip : MonoBehaviour
{
    public GameObject[] panels;
    public Animator transition;
    int index;
    bool endStory = false;
    void Start()
    {
        transition.SetTrigger("FadeOut");
        index = 0;        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("StartR"))
        {
            if (index == panels.Length-1 && !endStory)
            {
                StartCoroutine(sceneTransition());
                endStory = true;
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

    IEnumerator sceneTransition()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
