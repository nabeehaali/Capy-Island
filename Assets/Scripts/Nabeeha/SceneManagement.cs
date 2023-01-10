using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    /*public Animator camAnim;
    public GameObject startCanvas, characterSelectCanvas, PlayerManager;
    private void Update()
    {
        if (Input.GetButtonDown("StartL") && Input.GetButtonDown("StartR"))
        {
            CameraShift();
        }
    }
    public void CameraShift()
    {
        camAnim.SetTrigger("Start");
        StartCoroutine(canvasSwitch());    
    }*/
    public void firstGame()
    {
        SceneManager.LoadScene("TorchGame");
    }

    public void secondGame()
    {
        SceneManager.LoadScene("SleddingGame");
    }

    //IEnumerator canvasSwitch()
    //{
    //    startCanvas.SetActive(false);
    //    // will need to change this value depending on how long the animation runs for
    //    yield return new WaitForSeconds(2);
    //    characterSelectCanvas.SetActive(true);
    //    PlayerManager.SetActive(true);
    //}
}
