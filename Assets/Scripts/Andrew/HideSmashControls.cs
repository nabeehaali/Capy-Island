using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public bool smashed;
    int playerScore;

    float fireButton;
    bool action;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        smashed = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        float firing = fireButton;
        
        
    }

    public void fire(InputAction.CallbackContext context)
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

}
