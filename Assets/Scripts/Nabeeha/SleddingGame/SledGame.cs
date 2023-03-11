using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledGame : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRender;

    public bool inWater;

    public static int ranking;

    void Start()
    {
        //change to get parent
        _rigidbody = transform.GetComponent<Rigidbody>();
        _trailRender = transform.GetComponent<TrailRenderer>();
        ranking = 4;
        inWater = false;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("I have collided!");
            Debug.Log("Collision: " + gameObject.name);
            inWater = true;
            //_trailRender.emitting = false;
            //_rigidbody.drag = 4;
            SledSceneSetup.sledpoints.Add(new MinigamePoints(this.gameObject.name, ranking));
            ranking--;

            Debug.Log(ranking);
        }

        if(collision.gameObject.tag == "IcebergSmall1")
        {
            collision.gameObject.GetComponent<Animator>().Play("IcebergSink1");
        }

        if (collision.gameObject.tag == "IcebergSmall2")
        {
            collision.gameObject.GetComponent<Animator>().Play("IcebergSink2");
        }
    }
}
