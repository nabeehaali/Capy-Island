using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalShowdownControls : MonoBehaviour
{
    //public int magnitude;
    private Rigidbody _rigidbody;

    public bool canPush = false;

    void Start()
    {
        _rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //if (canPush)
        //{
        //    _rigidbody.AddForce(gameObject.transform.GetChild(0).gameObject.transform.forward * magnitude, ForceMode.VelocityChange);
        //    canPush = false;//can also try impulse
        //}
    }

    /*public void Push(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            canPush = true;
        }
        else if (context.canceled)
        {
            canPush = false;
        }
    }*/

    public void Push(InputAction.CallbackContext context)
    {
        StartCoroutine(pushMotion());
    }

    IEnumerator pushMotion()
    {
        canPush = true;
        yield return new WaitForSeconds(0.05f);
        canPush = false;
    }
}
