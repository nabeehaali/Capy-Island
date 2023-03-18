using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalShowdownControls : MonoBehaviour
{
    //public int magnitude;
    private Rigidbody _rigidbody;

    public bool canPush = false;
    public int index;
    //public Animator animator;

    void Start()
    {
        _rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        index = 0;
    }
    void FixedUpdate()
    {
        //if (canPush)
        //{
        //    _rigidbody.AddForce(gameObject.transform.GetChild(0).gameObject.transform.forward * magnitude, ForceMode.VelocityChange);
        //    canPush = false;//can also try impulse
        //}
    }

    public void NextHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            index++;
        }
    }

    public void PrevHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            index--;
        }
    }

    public void Push(InputAction.CallbackContext context)
    {
        //StartCoroutine(pushMotion());
        //if (context.started)
        //{
        //animator.SetBool("isHitting", true);


        //}
        if (context.performed)
        {
            canPush = true;
        }
        else if (context.canceled)
        {
            canPush = false;
            //animator.SetBool("isHitting", false);
        }
    }

    /*IEnumerator pushMotion()
    {
        canPush = true;
        yield return new WaitForSeconds(0.05f);
        canPush = false;
    }*/
}
