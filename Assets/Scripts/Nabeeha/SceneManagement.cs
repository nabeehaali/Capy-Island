using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator transition;
    private void Start()
    {
        transition.SetTrigger("FadeOut");
    }
    /*public Animator camAnim;
    public GameObject startCanvas, characterSelectCanvas, PlayerManager;
    private void Update()
    {
        if (Input.GetButtonDown("StartL") && Input.GetButtonDown("StartR"))
        {
            SceneManager.LoadScene("CharacterSelect");
        }
    }
    public void CameraShift()
    {
        camAnim.SetTrigger("Start");
        StartCoroutine(canvasSwitch());    
    }*/
    public void firstGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("TorchGame");
    }

    public void secondGame()
    {
        SceneManager.LoadScene("SledGame");
    }

    public void catchUp()
    {
        SceneManager.LoadScene("CatchUp");
    }

    public void beginning()
    {
        SceneManager.LoadScene(0);
        
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
    }

    public void mingameSelect()
    {
        SceneManager.LoadScene("04-MinigameSelection");
    }

    public void HideSmashGame()
    {
        SceneManager.LoadScene("5.1-InstructionsHideSmashMini");
    }

    public void TorchTakeOverGame()
    {
        SceneManager.LoadScene("6.1-InstructionsTorchMini");
    }

    public void GatorTagGame()
    {
        SceneManager.LoadScene("7.1-InstructionsAlligatorMini");
    }

    public void SlipSledGame()
    {
        SceneManager.LoadScene("8.1-InstructionsSledMini");
    }

    public void AdventureMode()
    {
        SceneManager.LoadScene("02-IntroStory");
    }

    public void MinigameMode()
    {
        SceneManager.LoadScene("03-CharacterSelectMini");
    }
}
