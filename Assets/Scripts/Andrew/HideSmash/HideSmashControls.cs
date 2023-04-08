using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls : MonoBehaviour
{
    Scene currentScene;
    string sceneName;

    public bool isPush, rumbleOn;
    int gamepadId;

    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        rumbleOn = false;
        isPush = false;
        animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void Update()
    {
       
            
           
    }

    public void Push(InputAction.CallbackContext context)
    {
        if(sceneName == "5.2-HideSmash" || sceneName == "05-HideSmash")
        {
            StartCoroutine(pushMotion());
        }
        
    }

    IEnumerator pushMotion()
    {

        if (GetComponent<PlayerMovement>().playermovement == Vector2.zero)
        {
            animator.SetTrigger("isHittingIdle");
            yield return new WaitForSeconds(0.25f);
            animator.ResetTrigger("isHittingIdle");
        }
        else
        {
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            animator.SetTrigger("isHittingWalk");
            yield return new WaitForSeconds(0.7f);
            animator.ResetTrigger("isHittingWalk");
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        }
        
        isPush = true;
       
        yield return new WaitForSeconds(0.25f);
        isPush = false;
    }
    

}