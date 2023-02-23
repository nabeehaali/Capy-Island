using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalShowdown : MonoBehaviour
{
    [SerializeField] List<GameObject> otherPlayers;
    float dist1, dist2, dist3;
    public float proximity;

    FinalsShowdownPlayerSettings finalshowdownplayersettings;
    FinalShowdownControls finalshowdowncontrols;
    public int magnitude;

    public bool hatsOff;
    public int hitCountP1 = 0, hitCountP2 = 0, hitCountP3 = 0, hitCountP4 = 0;
    int listIndexP1 = 0, listIndexP2 = 0, listIndexP3 = 0, listIndexP4 = 0;
    void Start()
    {
        finalshowdowncontrols = transform.parent.gameObject.GetComponent<FinalShowdownControls>();
        finalshowdownplayersettings = GameObject.Find("PlayerSettings").GetComponent<FinalsShowdownPlayerSettings>();

        if (this.gameObject.tag == "Player 1")
        {
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 2"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 3"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 4"));
        }
        else if (this.gameObject.tag == "Player 2")
        {
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 1"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 3"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 4"));
        }
        else if (this.gameObject.tag == "Player 3")
        {
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 1"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 2"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 4"));
        }
        else if (this.gameObject.tag == "Player 4")
        {
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 1"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 2"));
            otherPlayers.Add(GameObject.FindGameObjectWithTag("Player 3"));
        }
    }

    void Update()
    {
        dist1 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[0].transform.position);
        dist2 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[1].transform.position);
        dist3 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[2].transform.position);

        if ((dist1 < proximity || dist2 < proximity || dist3 < proximity) && finalshowdowncontrols.canPush == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (finalshowdowncontrols.canPush == true)
        {
            
            if (collision.gameObject.tag == "Player 1")
            {
                hitCountP1++;
                if (hitCountP1 > 0 && hitCountP1 % 2 == 0)
                {
                    
                    if (listIndexP1 == finalshowdownplayersettings.hatsOrderP1.Count - 1)
                    {
                        Debug.Log("P1 is out!");
                        finalshowdownplayersettings.hatsOrderP1[listIndexP1].transform.parent.gameObject.SetActive(false);
                        //move player up
                    }
                    else
                    {
                        Destroy(finalshowdownplayersettings.hatsOrderP1[listIndexP1]);
                        //projectile motion
                        listIndexP1++;
                    }
                }
                    
            }
            if (collision.gameObject.tag == "Player 2")
            {
                hitCountP2++;
                if (hitCountP2 > 0 && hitCountP2 % 2 == 0)
                {
                    
                    if (listIndexP2 == finalshowdownplayersettings.hatsOrderP2.Count - 1)
                    {
                        Debug.Log("P2 is out!");
                        finalshowdownplayersettings.hatsOrderP2[listIndexP2].transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        Destroy(finalshowdownplayersettings.hatsOrderP2[listIndexP2]);
                        listIndexP2++;
                    }
                }
            }
            if (collision.gameObject.tag == "Player 3")
            {
                hitCountP3++;
                if (hitCountP3 > 0 && hitCountP3 % 2 == 0)
                {
                    
                    if (listIndexP3 == finalshowdownplayersettings.hatsOrderP3.Count - 1)
                    {
                        Debug.Log("P3 is out!");
                        finalshowdownplayersettings.hatsOrderP3[listIndexP3].transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        Destroy(finalshowdownplayersettings.hatsOrderP3[listIndexP3]);
                        listIndexP3++;
                    }
                }
            }
            if (collision.gameObject.tag == "Player 4")
            {
                hitCountP4++;
                if (hitCountP4 > 0 && hitCountP4 % 2 == 0)
                {
                    
                    if (listIndexP4 == finalshowdownplayersettings.hatsOrderP4.Count - 1)
                    {
                        Debug.Log("P4 is out!");
                        finalshowdownplayersettings.hatsOrderP4[listIndexP4].transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        Destroy(finalshowdownplayersettings.hatsOrderP4[listIndexP4]);
                        listIndexP4++;
                    }
                }
            }
            
        }
    }
}
