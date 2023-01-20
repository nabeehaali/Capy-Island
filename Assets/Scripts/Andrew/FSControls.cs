using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FSControls : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public GameObject cake;

    float fireButton;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    

    // Update is called once per frame
    void Update()
    {
        float firing = fireButton;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Hats") {
            if (fireButton > 0.5) {
                Instantiate(cake, transform.position, Quaternion.identity);

            }
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        Debug.Log("Is working");
        fireButton = context.ReadValue<float>();
    }
}
