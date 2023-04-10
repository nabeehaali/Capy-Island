using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HockeyHat : Hat
{
    float timer = 0;
    float ability;
    public float shieldLength;
    bool flag;
    private PlayerInputActions playerControls;
    GameObject shield;
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        GetComponent<PlayerMovement>().speed = 30;
        setShieldNormal();
        flag = false;
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
        


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "22-FinalShowdown" || sceneName == "Hats")
        {
            shield = gameObject.transform.GetChild(0).Find("Shield").gameObject;
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
        if (timer < shieldLength)//
        {
            shield.SetActive(true);
        }
        else if (timer > shieldLength && timer < shieldLength * 1.4f)
        {
            shield.SetActive(false);
            
        }
        else if (timer > shieldLength * 1.4f) 
        {
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
