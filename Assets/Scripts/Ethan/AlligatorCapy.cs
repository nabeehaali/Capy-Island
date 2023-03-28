using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorCapy : MonoBehaviour
{
    public AlligatorPlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = gameObject.GetComponentInParent<AlligatorPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // making sure the alligator is biting, play is not already bit
        if (other.tag == "Alligator"
            && other.GetComponent<AlligatorBrain>().rise
            && !playerScript.isBit)
        {
            playerScript.Bit();
        }
    }

    // both used to check if the player object is in contact w/ the crown hitbox, data stored in player script
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered:" + other);
        if (other.tag == "Alligator Crown") playerScript.canSteal = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Alligator Crown") playerScript.canSteal = false;
    }
}
