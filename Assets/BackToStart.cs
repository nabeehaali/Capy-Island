using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    [SerializeField] VideoPlayer creditVideo;
    void Start()
    {
        creditVideo.loopPointReached += CreditFinish;
    }

    void CreditFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene("01-StartScreen");
    }
}
