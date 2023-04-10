using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class WizardHat : Hat
{
    private PlayerInputActions playerControls;
    public GameObject bolt;

    int index;
    Vector3 movement;
    public Transform shootTransform;
    float timer, ability;
    public float coolDown;

    GameObject shield;
    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        index = 0;
        coolDown = 0.75f;
        
    }

    private void Start()
    {
        GetComponent<PlayerMovement>().speed = 30;
        shootTransform = transform.GetChild(0).GetChild(6).transform;
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
                if (ability > 0.5 && timer > coolDown)
                {
                    //movement = new Vector3(gameObject.GetComponent<PlayerMovement>().playermovement.x, -9.81f, gameObject.GetComponent<PlayerMovement>().playermovement.y);
                    GameObject boltInstance = Instantiate(bolt, shootTransform.position, transform.GetChild(0).rotation); //Quaternion.identity
                    boltInstance.GetComponent<BoltBehaviour>().player = transform.GetChild(0).gameObject;
                    boltInstance.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * 100, ForceMode.Impulse);
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
