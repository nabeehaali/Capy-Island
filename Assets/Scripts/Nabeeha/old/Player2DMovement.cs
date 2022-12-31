using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    //Vector2 playermovement;
    [SerializeField] float speed;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerRigidbody.velocity = playermovement;
    }

    public void move(InputAction.CallbackContext context)
    {
        playerRigidbody.velocity = context.ReadValue<Vector2>() * speed;
    }
}
