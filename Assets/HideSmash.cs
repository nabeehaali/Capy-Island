using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSmash : MonoBehaviour
{
    private int playerScore, oldScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        oldScore = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().smashed == true) //&& firing > 0.5
        {
            Destroy(other.gameObject, 0.5f);
            gameObject.GetComponentInParent<HideSmashControls>().smashed = false;
            Debug.Log(playerScore);
        }
    }
}
