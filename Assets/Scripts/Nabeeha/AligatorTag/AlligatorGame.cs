using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorGame : MonoBehaviour
{
    public AlligatorControls playerScript;

    void Start()
    {
        playerScript = gameObject.GetComponentInParent<AlligatorControls>();
        Debug.Log(playerScript);
    }

    private void OnTriggerStay(Collider other)
    {
        // making sure the alligator is biting, play is not already bit
        if (other.tag == "Alligator"
            && other.GetComponent<AlligatorBrain>().rise
            && !playerScript.isBit)
        {
            Debug.Log("BIT!");
            playerScript.Bit();
        }
    }

    // both used to check if the player object is in contact w/ the crown hitbox, data stored in player script
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Alligator Crown") playerScript.canSteal = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Alligator Crown") playerScript.canSteal = false;
    }
}
