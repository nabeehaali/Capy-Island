using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls2 : MonoBehaviour
{
    public bool smashed;

    public void Smash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(SmashHit());
            //smashed = true;
        }
        else if (context.canceled)
        {
           //smashed = false;
        }
    }

    IEnumerator SmashHit()
    {
        smashed = true;
        yield return new WaitForSeconds(0.5f);
        smashed = false;
    }
}
