using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]

public class MoveInputEvent : UnityEvent<float, float> { }
public class testmove : MonoBehaviour
{
    PlayerInputActions playerInput;
    public MoveInputEvent moveInputEvent;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += OnMovePerformed;
        playerInput.Player.Move.canceled += OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
        //Debug.Log($"Move Input: {moveInput}");
    }
}
