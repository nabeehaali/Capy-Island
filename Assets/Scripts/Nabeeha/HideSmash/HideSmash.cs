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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().smashed == true) //&& firing > 0.5
        {
            Destroy(other.gameObject);
            playerScore++;
            //destroyIdol(other);
            Debug.Log(playerScore);
        }
    }

    private void destroyIdol(Collider other) 
    {
        if (other.gameObject.activeInHierarchy) 
        {
            Destroy(other.gameObject);
            playerScore++;
        }
        
        
    }
}
