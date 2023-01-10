using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledGame : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRender;
    PlayerMovement playermove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playermove = transform.parent.gameObject.GetComponent<PlayerMovement>();
        _trailRender = GetComponent<TrailRenderer>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            _trailRender.emitting = false;
            _rigidbody.drag = 4;
            GameObject.Find("SceneSettings").GetComponent<SledSceneSetup>().playerOrder.Add(this.gameObject.tag);
        }
    }
}
