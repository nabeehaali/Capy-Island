using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchUpControls : MonoBehaviour
{
    public bool canDig;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Dig(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canDig = true;
        }
        else if (context.canceled)
        {
            canDig = false;
        }
    }
}
