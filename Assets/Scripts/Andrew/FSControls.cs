using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FSControls : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public GameObject cake, lightning;
    public List<GameObject> hats;

    int index;

    float timer;
    float fireButton, nextButton, prevButton;

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

        if (nextButton > 0.5f) {
            
            if (index == hats.Count - 1) {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        if (prevButton > 0.5f)
        {
            if (index == 0)
            {
                index = hats.Count - 1;
            }
            else 
            {
                index--;
            }
            
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Hats") {
            if (fireButton > 0.5 && timer > 0.5) {
                //if (hats[index].tag == "Chef") 
                //{
                //    Instantiate(cake, transform.position, Quaternion.identity);
                //}
                //else if (hats[index].tag == "Wizard") 
                //{
                //    GameObject bolt = Instantiate(lightning, transform.position, Quaternion.identity);
                //    bolt.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 14);

                //}
                //else if (hats[index].tag == "Hockey")
                //{
                //}
                //else if (hats[index].tag == "")
                //{
                //}

                GameObject bolt = Instantiate(lightning, transform.position, Quaternion.identity);
                bolt.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 14);
                timer = 0;


            }
        }

    }

    public void fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        fireButton = context.ReadValue<float>();
    }

    public void nextHat(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        nextButton = context.ReadValue<float>();
    }
    public void prevHat(InputAction.CallbackContext context)
    {
        //Debug.Log("Is working");
        prevButton = context.ReadValue<float>();
    }
}
