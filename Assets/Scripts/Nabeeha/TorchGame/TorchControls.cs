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
            canLight = true;
        }
        else if(context.canceled)
        {
            canLight = false;
        }
    }

    public void BlowOut(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canBlow = true;
        }
        else if (context.canceled)
        {
            canBlow = false;
        }
    }
}
