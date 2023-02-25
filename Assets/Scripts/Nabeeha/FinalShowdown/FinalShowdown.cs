using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalShowdown : MonoBehaviour
{
    [SerializeField] List<GameObject> otherPlayers;
    float dist1, dist2, dist3;
    public float proximity;

    //FinalsShowdownPlayerSettings finalshowdownplayersettings;
    FinalShowdownControls finalshowdowncontrols;
    public int magnitude;

    public static bool isDeadP1, isDeadP2, isDeadP3, isDeadP4;
    //public bool hatsOff;
    //public int hitCountP1 = 0, hitCountP2 = 0, hitCountP3 = 0, hitCountP4 = 0;
    //int listIndexP1 = 0, listIndexP2 = 0, listIndexP3 = 0, listIndexP4 = 0;
    void Start()
    {
        finalshowdowncontrols = transform.parent.gameObject.GetComponent<FinalShowdownControls>();
        //finalshowdownplayersettings = GameObject.Find("PlayerSettings").GetComponent<FinalsShowdownPlayerSettings>();

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

        //might need to put this in fixedupdate!
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
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP1++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP1();

            }
            if (collision.gameObject.tag == "Player 2")
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP2++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP2();
            }
            if (collision.gameObject.tag == "Player 3")
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP3++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP3();
            }
            if (collision.gameObject.tag == "Player 4")
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP4++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP4();
            }
            
        }
    }
}
