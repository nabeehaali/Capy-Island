using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class HideSmash2 : MonoBehaviour
//{
//    public int playerScore;
//    bool smashed;
//    // Start is called before the first frame update
//    void Start()
//    {
//        playerScore = 0;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().smashed) //&& firing > 0.5
//        {
//            destroyIdol(other);
//            Debug.Log(playerScore);
//        }
//    }

//    private void destroyIdol(Collider other) 
//    {
//        if (other.gameObject.activeInHierarchy) 
//        {
//            Destroy(other.gameObject);
//            playerScore++;
//        }


//    }
//}
public class HideSmash2 : MonoBehaviour
{
    public int playerScore;
    bool smashed;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Vase")
        {

            Debug.Log("In Vase space");
        }
        if (other.gameObject.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().isPush == true)
        {

            Destroy(other.gameObject);
            playerScore++;
        }
    }
}