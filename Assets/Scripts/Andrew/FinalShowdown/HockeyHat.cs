using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HockeyHat : Hat
{
    float timer, ability;
    private PlayerInputActions playerControls;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "FinalShowdown" || sceneName == "Hats")
        {
            if (isActive) 
            {
                if (ability > 0.5f)//&& timer > 0.5
                {
                    shield.SetActive(true);
                    timer = 0;

                }
                else
                {
                    shield.SetActive(false);
                }

            }
            
        }
    }

    public void fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        ability = context.ReadValue<float>();

    }
}
