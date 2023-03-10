using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class newMove : MonoBehaviour
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
        transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Nothing")
        {
            //Debug.Log("I am using velocity movement");
            playerRigidbody.velocity = movement;
        }
        else if (sceneName == "SleddingGame")
        {
            //Debug.Log("I am using force movement");
            playerRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>() * speed;
    }

}
