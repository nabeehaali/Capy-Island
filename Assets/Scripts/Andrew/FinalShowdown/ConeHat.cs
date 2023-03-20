using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ConeHat : MonoBehaviour
{
    float timer, ability;
    private PlayerInputActions playerControls;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Hats")
        {
            if (ability > 0.5f && timer < 1f)//
            {
                timer += Time.deltaTime;
                gameObject.GetComponent<PlayerMovement>().speed = 30;

                

            }
            else
            {
                gameObject.GetComponent<PlayerMovement>().speed = 20;
                timer = 0;
            }
        }
    }

    public void fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        ability = context.ReadValue<float>();

    }
}
