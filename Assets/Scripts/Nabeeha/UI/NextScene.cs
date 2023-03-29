using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject fade;
    public Animator transition;
    bool nextScene = false;

    void Update()
    {
        if (Input.GetButtonDown("NextScene") && !nextScene)
        {
            //destroy hinje joints on all players hats
            //if hats have a hinjejoint component
            StartCoroutine(sceneTransition());
            nextScene = true;
        }
    }

    IEnumerator sceneTransition()
    {
        fade.SetActive(true);
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
