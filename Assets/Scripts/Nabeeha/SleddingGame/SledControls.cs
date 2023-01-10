using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledControls : MonoBehaviour
{
    public int magnitude;
    private Rigidbody _rigidbody;

    bool isPush = false;

    void Start()
    {
        _rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isPush)
        {
            _rigidbody.AddForce(gameObject.transform.GetChild(0).gameObject.transform.forward * magnitude, ForceMode.VelocityChange); //can also try impulse
        }
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
