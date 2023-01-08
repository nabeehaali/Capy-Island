using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class testmove : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    Vector2 _movement;

    [SerializeField] float speed = 4;
    
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(_movement.x, 0, _movement.y).normalized;
        transform.LookAt(transform.position + new Vector3(move.x, 0, move.z));
        playerRigidbody.AddForce(move * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void testmovement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
}
