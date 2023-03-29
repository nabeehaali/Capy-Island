using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HockeyHat : Hat
{
    float timer = 0;
    float ability;
    bool flag;
    private PlayerInputActions playerControls;
    public GameObject shield;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        shield = gameObject.transform.GetChild(0).GetChild(7).gameObject;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "22-FinalShowdown" || sceneName == "Hats")
        {
           
            if (ability > 0.5f)
            {
                flag = true;

            }

            if (flag == true)
            {
                StartCoroutine(shieldAbility());
            }


        }
    }
    public void fire(InputAction.CallbackContext context)
    {

        ability = context.ReadValue<float>();


    }

    IEnumerator shieldAbility()
    {

        timer += Time.deltaTime;
        if (timer < 1f)//
        {
            shield.SetActive(true);
        }
        else if (timer > 1 && timer < 3)
        {
            shield.SetActive(false);
            
        }
        else if (timer > 3) {
            timer = 0;
            flag = false;
            yield return null;
        }
        
    }
    public void setShieldNormal()
    {
        shield.SetActive(false);
    }

    
}
