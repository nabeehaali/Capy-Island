using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChefHat : Hat
{
    private PlayerInputActions playerControls;
    public GameObject cake;

    public List<GameObject> cakeList;


    int index;

    float timer, ability;
    public float cakeCooldown;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        timer = 0;
        index = 0;
        List<GameObject> cakeList = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

      
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "22-FinalShowdown" || sceneName == "Hats")
        {
            if (ability > 0.5f && timer > cakeCooldown)
            {
                GameObject cakeInstance = Instantiate(cake, transform.GetChild(0).transform.position, Quaternion.identity);
                
                cakeInstance.GetComponent<CakeBehaviour>().player = transform.GetChild(0).gameObject;

                timer = 0;

            }
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        
        ability = context.ReadValue<float>();

        
    }
}
