using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatchUpBreak : MonoBehaviour
{
    public TMP_Text sceneText;
    public string newText1 = "Oh No...";
    public string newText2 = "Something has happened on Capy Island!";
    public string newText3 = "Something has happened on Capy Island!";
    void Start()
    {
        sceneText.gameObject.SetActive(false);
        StartCoroutine(textChange());
    }

    IEnumerator textChange()
    {
        yield return new WaitForSeconds(2);
        sceneText.SetText(newText1);
        sceneText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        sceneText.SetText(newText2);
        yield return new WaitForSeconds(3);
        sceneText.SetText(newText3);
        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
