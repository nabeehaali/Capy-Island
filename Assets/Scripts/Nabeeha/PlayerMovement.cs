using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Users;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public Vector2 playermovement;

    public float speed;
    public float rotationSpeed;
    [SerializeField] public Animator animator;

    void Start()
    {
        playerRigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(playermovement.x, 0f, playermovement.y);
        movement.Normalize();
        movement *= speed;

        //if (movement != Vector3.zero)
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        //    gameObject.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(gameObject.transform.GetChild(0).transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //    animator.SetBool("isWalking", true);
        //}
        //else
        //{
        //    animator.SetBool("isWalking", false);
        //}

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "08-TorchGame" || sceneName == "Hats" || sceneName == "05-HideSmash" 
            || sceneName == "14-CatchUp" || sceneName == "22-FinalShowdown" || sceneName == "MovementTest" 
            || sceneName == "AmyAnimtest" || sceneName == "TestUI" || sceneName == "MoveSoundDev" || sceneName == "5.2-HideSmash")
        {
            //Debug.Log("I am using velocity movement");
            playerRigidbody.velocity = movement;

            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                gameObject.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(gameObject.transform.GetChild(0).transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else if (sceneName == "18-SledGame" || sceneName == "11-AlligatorGame" || sceneName == "AlligatorGameDev")
        {
            //Debug.Log("I am using force movement");
            playerRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Impulse);

            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                gameObject.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(gameObject.transform.GetChild(0).transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        
        //add elseif for progress scenes, no movement, but they can only rotate

    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>();
    }

    public void rumbleFunction(float lowFreq, float highFreq, float duration)
    {
        //Debug.Log(InputSystem.GetDevice<Gamepad>().deviceId);
        PlayerInput input = this.GetComponent<PlayerInput>();
        RumbleManager.instance.RumblePulse(lowFreq, highFreq, duration, input.GetDevice<Gamepad>());

    }

}
