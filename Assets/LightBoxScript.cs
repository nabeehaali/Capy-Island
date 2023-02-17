using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoxScript : MonoBehaviour
{
    bool inBounds;
    public Transform idol;
    
    // Start is called before the first frame update
    void Start()
    {
        inBounds = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("Entered");
        if (Physics.Raycast(idol.position, (collision.transform.position - idol.position), out RaycastHit hit)) //Just checks to see if it can hit the player
        {

            Debug.DrawRay(idol.position, (collision.transform.position - idol.position), Color.black);
            Debug.Log(hit.transform.parent.tag);

            if (hit.transform.parent.tag == "Player")
            {

                inBounds = true;

            }
            else
            {

                inBounds = false;

            }

            //Debug.Log(inBounds);
        }


        if (collision.transform.parent.tag == "Player" && inBounds == true)
        {
            
           // Debug.Log("Turned red");
           

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.parent.tag == "Player")
        {
            //Debug.Log("Turned White");
            this.GetComponentInParent<Light>().color = new Color(205, 209, 13);

        }

    }
}
