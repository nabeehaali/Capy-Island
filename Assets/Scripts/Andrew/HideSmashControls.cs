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
        
        if (firing > 0.5f)
        {
            smashed = true;
        }
        
        
    }

    public void fire(InputAction.CallbackContext context)
    {
        fireButton = context.ReadValue<float>();
    }

}
