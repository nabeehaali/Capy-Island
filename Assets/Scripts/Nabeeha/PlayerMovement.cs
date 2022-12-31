using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    Vector2 playermovement;

    [SerializeField] float speed;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(playermovement.x, 0, playermovement.y);
        transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        playerRigidbody.velocity = movement;
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>() * speed;
    }

}
