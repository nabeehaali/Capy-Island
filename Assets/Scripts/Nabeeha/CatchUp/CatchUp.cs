using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatchUp : MonoBehaviour
{
    public static int numHatsCollected;
   


    private void OnCollisionEnter(Collision collision)
    {
        //make a torchControls script that deals with button handling for digging and add the bool value to this if statement
        if (collision.gameObject.tag == "BaseHat")
        {
            //increase y value of collided GO by X amount
            //if y value of collided object reaches certain amt or higher, then destory (better yet, try to add a projectile to add it to the player's head)
            Destroy(collision.gameObject);
            numHatsCollected += 1;

            //after destroying, instantiate it under hats group in player GO
        }
    }
}
