using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChefHat : Hat
{
    private PlayerInputActions playerControls;
    public GameObject cake;
    public List<GameObject> hats;


    int index;

    float timer;
    float ability;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        timer = 0;
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
                if (ability > 0.5f && timer > 0.5f)
                {

                    GameObject cakeInstance = Instantiate(cake, transform.GetChild(0).transform.position, Quaternion.identity);
                    cakeInstance.GetComponent<CakeBehaviour>().player = transform.GetChild(0).gameObject;



                    timer = 0;

                }
            }
            
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        
        ability = context.ReadValue<float>();

        Debug.Log(ability);
    }
}
