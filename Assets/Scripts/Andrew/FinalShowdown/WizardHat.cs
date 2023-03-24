using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WizardHat : Hat
{
    private PlayerInputActions playerControls;
    public GameObject bolt;

    int index;
    Vector3 movement;
    public Transform shootTransform;
    float timer;
    float ability;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        index = 0;

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
                if (ability > 0.5 && timer > 0.5)
                {
                    movement = new Vector3(gameObject.GetComponent<PlayerMovement>().playermovement.x, -9.81f, gameObject.GetComponent<PlayerMovement>().playermovement.y);
                    GameObject boltInstance = Instantiate(bolt, shootTransform.position, transform.GetChild(0).rotation); //Quaternion.identity
                    boltInstance.GetComponent<BoltBehaviour>().player = transform.GetChild(0).gameObject;
                    boltInstance.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * 100, ForceMode.Impulse);
                    timer = 0;

                }
            }
            
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        ability = context.ReadValue<float>();

    }
}
