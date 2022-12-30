using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float speed, total;
    LineRenderer eyeLaser;
    RaycastHit IdolCast;
    public Transform target;
    
    void Start()
    {
        eyeLaser = GetComponent<LineRenderer>();
        speed = 0.1f;
        total = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotation();
        //Physics.Linecast(this.transform.position, this.transform.position + new Vector3(0, 0, 3.0f));
       
        //eyeLaser.SetPosition(0, gameObject.transform.position);
        //eyeLaser.SetPosition(1, gameObject.transform.position + new Vector3(0, 0, 3.0f));
        //laserEyes();
    }

    void laserEyes() {
        eyeLaser.SetPosition(0, this.transform.position);
        Vector3 target = new Vector3(0, 0, this.transform.position.z + 20f);
        eyeLaser.SetPosition(1, target);
    }

    void rotation() {
        transform.Rotate(0, speed, 0f, Space.Self);
        
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit)) //transform.TransformDirection(Vector3.forward)
        {
            Debug.Log("Hit Wall");
        }
        Debug.Log(transform.position);
        Debug.Log(-transform.forward);



        total += speed;
        //Debug.Log(total);
        if (total > 90)
        {
            speed = -0.1f;
        }
        else if (total < -90)
        {
            speed = 0.1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Destroy(collision.gameObject);
        }
    }
}
