using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualDangerZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        //Debug.DrawRay(transform.position, collision.gameObject.transform.position, Color.black);

        if (collision.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("Colliding");

        }
        else 
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("Not Colliding");
        }
    }
}
