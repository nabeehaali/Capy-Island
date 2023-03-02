using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    Vector2 playermovement;

    public float speed;
    public Animator animator;

    void Start()
    {
        playerRigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(playermovement.x, -9.81f, playermovement.y);

        gameObject.transform.GetChild(0).transform.LookAt(gameObject.transform.GetChild(0).transform.position + new Vector3(movement.x, 0, movement.z));

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "TorchGame" || sceneName == "Hats" || sceneName == "HideSmashNab" || sceneName == "CatchUp" || sceneName == "FinalShowdown" || sceneName == "MovementTest" || sceneName == "AmyAnimtest")
        {
            //Debug.Log("I am using velocity movement");
            playerRigidbody.velocity = movement;
        }
        else if (sceneName == "SledGame" || sceneName == "AligatorTag" || sceneName == "AlligatorGame")
        {
            //Debug.Log("I am using force movement");
            playerRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
        }
        
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>() * speed;
        animator.SetBool("isWalking", true);
        
        //gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("IdleToWalk");

        if(context.canceled)
        {
            animator.SetBool("isWalking", false);
            //gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("WalkToIdle");
        }
    }

}
