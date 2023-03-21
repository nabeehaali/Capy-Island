using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledGame : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRender;

    public bool inWater;
    public bool offBerg;
    public bool piece1, piece2, piece3, piece4, piece5, piece6;
    public int colCount = 0;

    public static int ranking;

    void Start()
    {
        //change to get parent
        _rigidbody = transform.GetComponent<Rigidbody>();
        _trailRender = transform.GetComponent<TrailRenderer>();
        ranking = 4;
        inWater = false;
        offBerg = false;
        piece1 = false;
    }

    void Update()
    {
        /*if (piece1 = true && piece2 == true && piece3 == true && piece4 == true && piece5 == true && piece6 == true)
        {
            offBerg = true;
        }*/

        if (offBerg)
        {
            gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = true;
            _trailRender.emitting = false;
            _rigidbody.drag = 4;
            SledSceneSetup.sledpoints.Add(new MinigamePoints(this.gameObject.name, ranking));
            ranking--;
        }

        if(collision.gameObject.tag == "IcebergSmall1")
        {
            collision.gameObject.GetComponent<Animator>().Play("IcebergSink1");
        }

        if (collision.gameObject.tag == "IcebergSmall2")
        {
            collision.gameObject.GetComponent<Animator>().Play("IcebergSink2");
        }

        if (collision.gameObject.tag == "IceBkg")
        {
            _rigidbody.mass = 1000;
        }

        colCount++;

    }

    private void OnCollisionExit(Collision collision)
    {
        colCount--;
        if (colCount == 0)
        {
            Debug.Log("not colliding with anything");
            offBerg = true;
            _rigidbody.mass = 1000000;
        }
    }
}
