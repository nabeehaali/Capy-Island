using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChefHat : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public GameObject cake;
    public List<GameObject> hats;

    int index;

    float timer;
    float fireButton;

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

        float firing = fireButton;

        timer += Time.deltaTime;

      
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Hats")
        {
            if (fireButton > 0.5 && timer > 0.5)
            {
                GameObject cakeInstance = Instantiate(cake, transform.GetChild(0).transform.position, Quaternion.identity);
                cakeInstance.GetComponent<CakeBehaviour>().player = transform.GetChild(0).gameObject;
                
                timer = 0;

            }
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        fireButton = context.ReadValue<float>();
        
    }
}
