using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStoryNextPanel : MonoBehaviour
{
    IntroStoryFlip introflip;
    private void Start()
    {
        introflip = GameObject.Find("Flipping").GetComponent<IntroStoryFlip>();
    }
    void Update()
    {
        if (Input.GetButtonDown("StartR"))
        {
            introflip.index++;
            gameObject.SetActive(false);
            introflip.next = false;
        }
    }
}
