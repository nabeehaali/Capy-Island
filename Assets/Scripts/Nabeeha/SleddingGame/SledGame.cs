using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledGame : MonoBehaviour
{
    [SerializeField] int magnitude;
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRender;
    PlayerMovement playermove;

    bool isPush = false;

    void Start()
    {
        playermove = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        _trailRender = GetComponent<TrailRenderer>();

        
    }
    void FixedUpdate()
    {
        if(isPush)
        {
            _rigidbody.AddForce(transform.forward * magnitude, ForceMode.VelocityChange); //can also try impulse
        }
    }

    public void Push(InputAction.CallbackContext context)
    {
        StartCoroutine(pushMotion());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            _trailRender.emitting = false;
            playermove.speed = 20;
            magnitude = 3;
            GameObject.Find("SceneSettings").GetComponent<SledSceneSetup>().playerOrder.Add(this.gameObject.tag);
        }
    }

    IEnumerator pushMotion()
    {
        isPush = true;
        yield return new WaitForSeconds(0.05f);
        isPush = false;
    }
}
