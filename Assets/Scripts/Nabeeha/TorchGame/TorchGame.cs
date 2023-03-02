using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGame : MonoBehaviour
{
    public GameObject[] firePrefabs;
    public TorchControls torchcontrols;
 
    void Start()
    {
        torchcontrols = transform.parent.gameObject.GetComponent<TorchControls>();
    }

    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        //activate torch
        if (collision.gameObject.tag == "Torch" && torchcontrols.canLight == true)
        {
            if(this.gameObject.tag == "Player 1")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[0], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 2")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[1], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 3")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[2], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 4")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[3], collision.gameObject.transform);
                }
            }
        }

        //blow out torch
        if (collision.gameObject.tag == "Torch" && torchcontrols.canBlow == true)
        {
            if (this.gameObject.tag == "Player 1")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P1Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);

                    }
                }

            }
            else if (this.gameObject.tag == "Player 2")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P2Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                    }
                }

            }
            else if (this.gameObject.tag == "Player 3")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P3Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                    }
                }

            }
            else if (this.gameObject.tag == "Player 4")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P4Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                    }
                }

            }
        }
    }
}
