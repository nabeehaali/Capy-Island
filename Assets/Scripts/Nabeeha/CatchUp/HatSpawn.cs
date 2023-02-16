using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HatSpawn : MonoBehaviour
{
    public GameObject Hat;
    int numHats;
    //float timePassed;

    //bool nextRound = false;

    
    void Start()
    {
        numHats = DisasterSceneSetup.p1HatsOff + DisasterSceneSetup.p2HatsOff + DisasterSceneSetup.p3HatsOff + DisasterSceneSetup.p4HatsOff;
        //Debug.Log(numHats);

        for(int i = 0; i < numHats; i++)
        {
            //change this random range to be in bounds of mesh
            GameObject hat = Instantiate(Hat, new Vector3(Random.Range(-60, 60), Random.Range(-0.8f, 0), Random.Range(-27, 34)), Quaternion.identity);
            hat.GetComponent<SphereCollider>().radius = 0.006f;
            hat.GetComponent<SphereCollider>().center = new Vector3(0, 0.005f, 0);
            Physics.IgnoreCollision(hat.GetComponent<SphereCollider>(), GameObject.Find("Sand").GetComponent<MeshCollider>());
        }
    }
}
