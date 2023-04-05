using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSmash : MonoBehaviour
{
    public int playerScore;
    CapySoundTrigger soundTrigger;
    // Start is called before the first frame update
    void Start()
    {
        soundTrigger = GetComponent<CapySoundTrigger>();
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
            soundTrigger.PlayHit();
            Destroy(other.gameObject);
            playerScore++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.root.CompareTag("Player"))
        {
            soundTrigger.PlayChirp();
        }
    }
}