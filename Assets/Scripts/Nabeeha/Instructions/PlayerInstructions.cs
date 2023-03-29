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
    void Start()
    {
        transition = GameObject.Find("TransitionCanvas").GetComponent<Animator>();
    }
    public void Ready(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(this.gameObject.transform.GetChild(0).gameObject.tag == "Player 1")
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

    void updateUI(GameObject UI)
    {
        UI.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Ready");

        if(p1Ready + p2Ready + p3Ready + p4Ready == 4 && !isReady)
        {
            p1Ready = 0;
            p2Ready = 0;
            p3Ready = 0;
            p4Ready = 0;

            StartCoroutine(sceneTransition());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator sceneTransition()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
