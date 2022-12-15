using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        playerRigidbody.velocity = movement;

        if(GameObject.FindGameObjectsWithTag("Cube").Length == 0)
        {
            SceneManager.LoadScene("2DMovement");
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        playermovement = context.ReadValue<Vector2>() * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            Destroy(collision.gameObject);
        }
    }
}
