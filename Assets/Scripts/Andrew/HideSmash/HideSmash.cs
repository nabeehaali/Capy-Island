using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSmash : MonoBehaviour
{
    public int playerScore;
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
        if (other.gameObject.tag == "Vase" && gameObject.GetComponentInParent<HideSmashControls>().isPush == true)
        {

            Destroy(other.gameObject);
            playerScore++;
        }
    }
}