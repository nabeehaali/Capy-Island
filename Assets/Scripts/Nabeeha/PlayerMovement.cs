using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        if (movement != Vector3.zero)
        {
            //CHECK WITH GROUP TO SEE WHICH ROTATION LOOKS BETTER
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            gameObject.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(gameObject.transform.GetChild(0).transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            //gameObject.transform.GetChild(0).transform.LookAt(gameObject.transform.GetChild(0).transform.position + new Vector3(movement.x, 0, movement.z));

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "08-TorchGame" || sceneName == "Hats" || sceneName == "05-HideSmashNab" || sceneName == "14-CatchUp" || sceneName == "22-FinalShowdown" || sceneName == "MovementTest" || sceneName == "AmyAnimtest" || sceneName == "TestUI")
        {
            //Debug.Log("I am using velocity movement");
            playerRigidbody.velocity = movement;
            //playerRigidbody.velocity = movement.normalized + new Vector3(0.0f, playerRigidbody.velocity.y, 0.0f);
        }
        else if (sceneName == "18-SledGame" || sceneName == "AligatorTag" || sceneName == "11-AlligatorGame")
        {
            //Debug.Log("I am using force movement");
            playerRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
        }
        
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>();
    }

}
