using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInstructions : MonoBehaviour
{
    public GameObject UI;
    static int p1Ready, p2Ready, p3Ready, p4Ready;
    public Animator transition;
    bool isReady = false;

    Scene currentScene;
    string sceneName;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    public void Ready(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //RumbleManager.instance.RumblePulse(0.25f, 1f, 0.25f);
            if(sceneName == "04.5-InstructionsHideSmash" || sceneName == "07-InstructionsTorch 1" || sceneName == "10-InstructionsAlligator 1" || sceneName == "13.5-InstructionsCatchUp"
                || sceneName == "17-InstructionsSled" || sceneName == "21-HatInfo" || sceneName == "21.5-InstructionsShowdown" || sceneName == "5.1-InstructionsHideSmashMini"
                || sceneName == "6.1-InstructionsTorchMini" || sceneName == "7.1-InstructionsAlligatorMini" || sceneName == "8.1-InstructionsSledMini")
            {
                GetComponent<PlayerMovement>().rumbleFunction(0.25f, 1f, 0.25f);
                if (this.gameObject.transform.GetChild(0).gameObject.tag == "Player 1")
                {
                    UI = GameObject.Find("P1");
                    p1Ready = 1;
                    updateUI(UI);

                }
                if (this.gameObject.transform.GetChild(0).gameObject.tag == "Player 2")
                {
                    UI = GameObject.Find("P2");
                    p2Ready = 1;
                    updateUI(UI);
                }
                if (this.gameObject.transform.GetChild(0).gameObject.tag == "Player 3")
                {
                    UI = GameObject.Find("P3");
                    p3Ready = 1;
                    updateUI(UI);
                }
                if (this.gameObject.transform.GetChild(0).gameObject.tag == "Player 4")
                {
                    UI = GameObject.Find("P4");
                    p4Ready = 1;
                    updateUI(UI);
                }
            }
            

        }
    }

    void updateUI(GameObject UI)
    {
        UI.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Ready");

        if (sceneName == "5.1-InstructionsHideSmashMini" || sceneName == "6.1-InstructionsTorchMini" || sceneName == "7.1-InstructionsAlligatorMini" || sceneName == "8.1-InstructionsSledMini")
        {
            if (p1Ready + p2Ready + p3Ready + p4Ready == 2 && !isReady)
            {
                p1Ready = 0;
                p2Ready = 0;
                p3Ready = 0;
                p4Ready = 0;

                transition = GameObject.Find("TransitionCanvas").GetComponent<Animator>();
                StartCoroutine(sceneTransition());
            }
        }
        else
        {
            if (p1Ready + p2Ready + p3Ready + p4Ready == 4 && !isReady)
            {
                p1Ready = 0;
                p2Ready = 0;
                p3Ready = 0;
                p4Ready = 0;

                transition = GameObject.Find("TransitionCanvas").GetComponent<Animator>();
                StartCoroutine(sceneTransition());
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
