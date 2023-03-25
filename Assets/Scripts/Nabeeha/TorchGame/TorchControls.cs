using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorchControls : MonoBehaviour
{
    public bool canLight;
    public bool canBlow;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LightUp(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            StartCoroutine(Light());
            //canLight = true;
        }
        else if(context.canceled)
        {
            StopCoroutine(Light());
            //canLight = false;
        }
    }

    public void BlowOut(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Blow());
            //canBlow = true;
        }
        else if (context.canceled)
        {
            StopCoroutine(Blow());
            //canBlow = false;
        }
    }

    IEnumerator Light()
    {
        canLight = true;
        yield return new WaitForSeconds(0.5f);
        canLight = false;
    }

    IEnumerator Blow()
    {
        canBlow = true;
        yield return new WaitForSeconds(0.5f);
        canBlow = false;
    }
}
