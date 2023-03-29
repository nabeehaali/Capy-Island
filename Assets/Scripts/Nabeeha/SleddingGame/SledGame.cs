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
    bool removeCam;
    public int colCount = 0;

    public static int ranking;
    private Vector3 direction;

    void Start()
    {
        //change to get parent
        _rigidbody = transform.GetComponent<Rigidbody>();
        _trailRender = transform.GetComponent<TrailRenderer>();
        ranking = 4;
        inWater = false;
        offBerg = false;
        
    }

    void Update()
    {

        if (offBerg)
        {
            gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 0;
            _rigidbody.drag = 0f;
            //change the -0.2 to something else for feel
            //direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            //_rigidbody.velocity = direction.normalized * 30 + new Vector3(0.0f, _rigidbody.velocity.y - 0.2f, 0.0f);
            //offBerg = false; //check if this is ok
        }

        if(removeCam)
        {
            GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Remove(this.gameObject.transform);
            removeCam = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = true;
            _trailRender.emitting = false;
            _rigidbody.drag = 4;
            _rigidbody.velocity = Vector3.zero;
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
            //Debug.Log("not colliding with anything");
            //GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Remove(this.gameObject.transform);
            offBerg = true;
            removeCam = true;
        }
    }
}
