using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalShowdown : MonoBehaviour
{
    [SerializeField] List<GameObject> otherPlayers;
    [SerializeField] Material redMaterial;
    public Material originalMaterialP1, originalMaterialP2, originalMaterialP3, originalMaterialP4;
    float dist1, dist2, dist3;
    public float proximity;

    CapySoundTrigger soundTrigger;

    //FinalsShowdownPlayerSettings finalshowdownplayersettings;
    FinalShowdownControls finalshowdowncontrols;
    public int magnitude;

    public static bool isDeadP1, isDeadP2, isDeadP3, isDeadP4;
    //public bool hatsOff;
    //public int hitCountP1 = 0, hitCountP2 = 0, hitCountP3 = 0, hitCountP4 = 0;
    //int listIndexP1 = 0, listIndexP2 = 0, listIndexP3 = 0, listIndexP4 = 0;
    void Start()
    {
        soundTrigger = GetComponent<CapySoundTrigger>();
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

        originalMaterialP1 = GameObject.FindGameObjectWithTag("Player 1").transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0];
        originalMaterialP2 = GameObject.FindGameObjectWithTag("Player 2").transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0];
        originalMaterialP3 = GameObject.FindGameObjectWithTag("Player 3").transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0];
        originalMaterialP4 = GameObject.FindGameObjectWithTag("Player 4").transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0];
    }

    void Update()
    {
        dist1 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[0].transform.position);
        dist2 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[1].transform.position);
        dist3 = Vector3.Distance(this.gameObject.transform.position, otherPlayers[2].transform.position);

       
    }

    private void FixedUpdate()
    {
        //might need to put this in fixedupdate!
        if ((dist1 < proximity || dist2 < proximity || dist3 < proximity) && finalshowdowncontrols.canPush == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.VelocityChange);
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(finalshowdowncontrols != null)
        {
            if (gameObject.tag == "Player 1" && collision.gameObject.tag == "Bolt" && collision.gameObject.GetComponent<BoltBehaviour>().player != gameObject && gameObject.transform.Find("Shield").gameObject.activeSelf == false)
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP1++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP1();
                StartCoroutine(playerHit(gameObject, originalMaterialP1));

            }
            if (gameObject.tag == "Player 2" && collision.gameObject.tag == "Bolt" && collision.gameObject.GetComponent<BoltBehaviour>().player != gameObject && gameObject.transform.Find("Shield").gameObject.activeSelf == false)
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP2++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP2();
                StartCoroutine(playerHit(gameObject, originalMaterialP2));
            }
            if (gameObject.tag == "Player 3" && collision.gameObject.tag == "Bolt" && collision.gameObject.GetComponent<BoltBehaviour>().player != gameObject && gameObject.transform.Find("Shield").gameObject.activeSelf == false)
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP3++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP3();
                StartCoroutine(playerHit(gameObject, originalMaterialP3));
            }
            if (gameObject.tag == "Player 4" && collision.gameObject.tag == "Bolt" && collision.gameObject.GetComponent<BoltBehaviour>().player != gameObject && gameObject.transform.Find("Shield").gameObject.activeSelf == false)
            {
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP4++;
                GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP4();
                StartCoroutine(playerHit(gameObject, originalMaterialP4));
            }

            if (finalshowdowncontrols.canPush == true)
            {
                
                
                if (collision.gameObject.tag == "Player 1" && collision.gameObject.transform.Find("Shield").gameObject.activeSelf == false) //!&& 
                {
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP1++;
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP1();
                    StartCoroutine(playerHit(collision.gameObject, originalMaterialP1));

                }
                if (collision.gameObject.tag == "Player 2" && collision.gameObject.transform.Find("Shield").gameObject.activeSelf == false)
                {
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP2++;
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP2();
                    StartCoroutine(playerHit(collision.gameObject, originalMaterialP2));
                }
                if (collision.gameObject.tag == "Player 3" && collision.gameObject.transform.Find("Shield").gameObject.activeSelf == false)
                {
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP3++;
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP3();
                    StartCoroutine(playerHit(collision.gameObject, originalMaterialP3));
                }
                if (collision.gameObject.tag == "Player 4" && collision.gameObject.transform.Find("Shield").gameObject.activeSelf == false)
                {
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hitCountP4++;
                    GameObject.Find("SceneSetup").GetComponent<FinalsShowdownSceneSetup>().hatTrackingP4();
                    StartCoroutine(playerHit(collision.gameObject, originalMaterialP4));
                }


                //if (collision.gameobject.tag == "bolt")
                /*{
                if this player tag == p1, p2, p3, p4, make them do the same functions as above!
                }*/
            }
        }
        
    }

    public IEnumerator playerHit(GameObject player, Material originalMat)
    {
        soundTrigger.PlayHit();
        //Debug.Log(player.transform.GetChild(0));
        var renderer = player.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();

        //Debug.Log(player.transform.parent.tag);
        player.transform.parent.GetComponent<PlayerMovement>().rumbleFunction(0.5f,0.5f,0.5f);
        Material[] materials = renderer.sharedMaterials; 
        materials[0] = redMaterial;
        renderer.sharedMaterials = materials;
        yield return new WaitForSeconds(0.5f);
        materials[0] = originalMat;
        renderer.sharedMaterials = materials;

        finalshowdowncontrols.canPush = false;
    }
}
