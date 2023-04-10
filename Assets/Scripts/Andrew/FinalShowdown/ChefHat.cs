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

    GameObject shield;
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

    private void OnEnable()
    {
        GetComponent<PlayerMovement>().speed = 30;
        shield = gameObject.transform.GetChild(0).Find("Shield").gameObject;
        setShieldNormal();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

      
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "22-FinalShowdown" || sceneName == "Hats")
        {
            shield = gameObject.transform.GetChild(0).Find("Shield").gameObject;
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

    public void setShieldNormal()
    {
        shield.SetActive(false);
    }
}
