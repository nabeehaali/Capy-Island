using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ConeHat : Hat
{
    float ability;
    float timer = 0;
    bool flag;
    private PlayerInputActions playerControls;
    public ParticleSystem speedParticles;
    bool stars = false;
    GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        setSpeedNormal();
        flag = false;
    }

    private void OnEnable()
    {
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
            if (ability > 0.5f)//
            {
                flag = true;
                
            }

            if (flag == true)
            {
                StartCoroutine(speedAbility());
            }
            
        }
    }

    public void fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        ability = context.ReadValue<float>();

    }
    IEnumerator speedAbility() 
    {
        
        timer += Time.deltaTime;
        if (timer < 1f)//
        {
            gameObject.GetComponent<PlayerMovement>().speed = 55;
            if(!stars)
            {
                Instantiate(speedParticles, gameObject.transform.GetChild(0));
                stars = true;
            }
            
            gameObject.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = true;

        }
        else if(timer > 1 && timer < 3)
        {
            gameObject.GetComponent<PlayerMovement>().speed = 30;
            gameObject.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = false;
            
        }
        else if (timer > 3)
        {
            timer = 0;
            flag = false;
            stars = false;
            yield return null;
        }
    }
    public void setSpeedNormal() 
    {
        gameObject.GetComponent<PlayerMovement>().speed = 30;
        gameObject.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = false;
    }

    public void setShieldNormal()
    {
        shield.SetActive(false);
    }
}
