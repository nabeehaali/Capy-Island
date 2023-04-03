using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroStoryFlip : MonoBehaviour
{
    public GameObject[] video;
    public GameObject NextScene;
    public Animator transition;
    public int index;
    bool endStory = false;
    public bool next = false;
    void Start()
    {
        transition.SetTrigger("FadeOut");
        index = 0;
    }

    
    void Update()
    {
        if (index > video.Length - 1 && !endStory)
        {
            StartCoroutine(sceneTransition());
            endStory = true;
        }

        for (int i = 0; i < video.Length; i++)
        {
            if(i == index)
            {
                video[i].SetActive(true);
            }
            else
            {
                video[i].SetActive(false);
            }

            if(video[i].GetComponent<VideoPlayer>().time >= 7)
            {
                NextScene.SetActive(true);
                next = false;
            }
            video[i].GetComponent<VideoPlayer>().loopPointReached += videoDone;
        }
        
    }

    void videoDone(VideoPlayer vp)
    {
        if (!next)
        {
            index++;
            NextScene.SetActive(false);
            next = true;
        }        
    }

    public IEnumerator sceneTransition()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
