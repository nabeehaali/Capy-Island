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

        if (sceneName == "22-FinalShowdown" || sceneName == "Hats")
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
            if (enabled != true) 
            {
                shield.SetActive(false);
            }
            
           
        }
    }

    public void fire(InputAction.CallbackContext context)
    {
        
        ability = context.ReadValue<float>();
        

    }
}
