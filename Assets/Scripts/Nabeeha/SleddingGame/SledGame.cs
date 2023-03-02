using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledGame : MonoBehaviour
{
    //private Rigidbody _rigidbody;
    //private TrailRenderer _trailRender;

    public bool inWater;

    public static int ranking;

    void Start()
    {
        //change to get parent
        //_rigidbody = transform.parent.GetComponent<Rigidbody>();
        //_trailRender = transform.parent.GetComponent<TrailRenderer>();
        ranking = 4;
        inWater = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("I have collided!");
            inWater = true;
            //_trailRender.emitting = false;
            //_rigidbody.drag = 4;
            //SledSceneSetup.sledpoints.Add(new MinigamePoints(this.gameObject.transform.parent.parent.parent.name, ranking));
            //ranking--;
        }
    }
}
