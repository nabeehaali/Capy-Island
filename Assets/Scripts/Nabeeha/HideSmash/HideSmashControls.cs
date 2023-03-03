using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls : MonoBehaviour
{
    private PlayerInputActions playerControls;
    public bool smashed,isPush;
    int playerScore;

    float fireButton;
    bool action;
    public int magnitude;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Awake()
    {
        //_rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
        smashed = false;
        isPush = false;
    }

    // Update is called once per frame

    public void Hit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            smashed = true;
        }
        else if (context.canceled)
        {
            smashed = false;
        }
    }

    void FixedUpdate()
    {
        /*if (isPush)
        {
            _rigidbody.AddForce(gameObject.transform.GetChild(0).gameObject.transform.forward * magnitude, ForceMode.VelocityChange); //can also try impulse
        }

        float firing = fireButton;*/
    }

    public void Push(InputAction.CallbackContext context)
    {
        StartCoroutine(pushMotion());
    }

    IEnumerator pushMotion()
    {
        isPush = true;
        yield return new WaitForSeconds(0.05f);
        isPush = false;
    }

}
