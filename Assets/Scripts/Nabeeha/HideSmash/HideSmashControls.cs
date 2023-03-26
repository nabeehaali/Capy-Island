using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public bool smashed,isPush;
    int playerScore;

    float fireButton;
    bool action;
    public int magnitude;
    private Rigidbody _rigidbody;

    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        //_rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
        smashed = false;
        isPush = false;

        animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void Hit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            smashed = true;
        }
        else if (context.canceled)
        {
            smashed = false;
        }
    }

    public void Push(InputAction.CallbackContext context)
    {
        StartCoroutine(pushMotion());
    }

    IEnumerator pushMotion()
    {
        isPush = true;
        if(GetComponent<PlayerMovement>().playermovement == Vector2.zero)
        {
            animator.SetBool("isHittingIdle", true);
            //isPush = true;
            yield return new WaitForSeconds(1);
            animator.SetBool("isHittingIdle", false);
        }
        else
        {
            //set speed to 0 
            animator.SetBool("isHittingWalk", true);
            //isPush = true;
            yield return new WaitForSeconds(1);
            animator.SetBool("isHittingWalk", false);
            //return speed to regular value
        }
       //yield return new WaitForSeconds(0.05f);
        isPush = false;
    }

}
