using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSmash : MonoBehaviour
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
            //other.transform.Find("Eating Effect").gameObject.SetActive(true); //Add a little particle effect
            Destroy(other.gameObject);
            playerScore++;
        }
    }
}
