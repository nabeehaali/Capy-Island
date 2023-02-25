using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalShowdownGround : MonoBehaviour
{
    public GameObject dustParticle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RegularHat" || collision.gameObject.tag == "Wizard" || collision.gameObject.tag == "Chef" || collision.gameObject.tag == "Hockey" || collision.gameObject.tag == "Cream")
        {
            Instantiate(dustParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        

    }
}
