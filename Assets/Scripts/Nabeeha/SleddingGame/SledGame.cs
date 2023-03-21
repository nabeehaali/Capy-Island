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




        if (collision.gameObject.name == "piece1")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece1 = false;
        }
        if (collision.gameObject.name == "piece2")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece2 = false;
        }
        if (collision.gameObject.name == "piece3")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece3 = false;
        }
        if (collision.gameObject.name == "piece4")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece4 = false;
        }
        if (collision.gameObject.name == "piece5")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece5 = false;
        }
        if (collision.gameObject.name == "piece6")
        {
            //Debug.Log(gameObject.name + "Onnn this piece");
            piece6 = false;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        //if player is off the whole iceberg (all pieces)
        if (collision.gameObject.name == "piece1")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece1 = true;
        }
        if (collision.gameObject.name == "piece2")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece2 = true;
        }
        if (collision.gameObject.name == "piece3")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece3 = true;
        }
        if (collision.gameObject.name == "piece4")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece4 = true;
        }
        if (collision.gameObject.name == "piece5")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece5 = true;
        }
        if (collision.gameObject.name == "piece6")
        {
            //Debug.Log(gameObject.name + "Offf this piece");
            piece6 = true;
        }
    }
}
