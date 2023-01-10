using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorPlayerMovement : MonoBehaviour
{
    PlayerInputActions playerInput;
    Vector2 moveInput;
    Vector2 currentInput;
    private Vector2 smoothInput;
    public float moveSmoothTime = 0.2f;
    public float playerSpeed = 2.0f;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2.SmoothDamp(currentInput, moveInput, ref smoothInput, moveSmoothTime);
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * playerSpeed;
        if(moveInput.magnitude > 0.2)
        {
            transform.Translate(move);
            //transform.rotation = Quaternion.LookRotation(move);
            //transform.LookAt(transform.position + move);
        }
        
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
