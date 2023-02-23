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

        if (sceneName == "TorchGame" || sceneName == "Hats" || sceneName == "HideSmash" || sceneName == "CatchUp" || sceneName == "FinalShowdown" || sceneName == "MovementTest")
        {
            //Debug.Log("I am using velocity movement");
            playerRigidbody.velocity = movement;
        }
        else if (sceneName == "SledGame")
        {
            //Debug.Log("I am using force movement");
            playerRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
        }
        else if (sceneName == "AligatorTag")
        {
            playerRigidbody.AddForce(movement * 5 * Time.deltaTime, ForceMode.Impulse);
        }
        
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>() * speed;
    }

}
