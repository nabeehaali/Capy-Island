using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float speed, total;
    LineRenderer eyeLaser;
    bool inBounds;
    RaycastHit IdolCast;
    
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

    void laserEyes() 
    {
        eyeLaser.SetPosition(0, this.transform.position);
        Vector3 target = new Vector3(0, 0, this.transform.position.z + 20f);
        eyeLaser.SetPosition(1, target);
    }

    void rotation() 
    {
        transform.Rotate(0, speed, 0f, Space.Self);

        //if (Physics.Raycast(transform.position, transform.forward * 1.5f, out RaycastHit hit)) //transform.TransformDirection(Vector3.forward)
        //{

        //    //Debug.Log(hit.collider.tag);


        //}

        GameObject player1 = GameObject.FindGameObjectWithTag("Player 1");
        




        total += speed;

        if (total > 90)
        {
            speed = -0.1f;
        }
        else if (total < -90)
        {
            speed = 0.1f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        
            
            
           

            //Debug.Log("Player in zone");
        
    }

    private void OnTriggerStay(Collider collision)
    {
        //Debug.DrawRay(transform.position, collision.gameObject.transform.position, Color.black);


        if (collision.gameObject.tag == "Player 1")
        {
            if (Physics.Raycast(transform.position, (collision.transform.position - transform.position), out RaycastHit hit)) //transform.forward * 1.5f
            {
                Debug.DrawRay(gameObject.transform.position, (collision.transform.position - gameObject.transform.position), Color.black);
                Debug.Log(collision.tag);
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Player 1")
                {
                    collision.gameObject.SetActive(false);

                }

            }
        }
    }
}
