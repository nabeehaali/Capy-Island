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

    public TMP_Text hatsCollected;
    void Start()
    {
        numHats = DisasterSceneSetup.p1HatsOff + DisasterSceneSetup.p2HatsOff + DisasterSceneSetup.p3HatsOff + DisasterSceneSetup.p4HatsOff;
        Debug.Log(numHats);

        for(int i = 0; i < numHats; i++)
        {
            GameObject hat = Instantiate(Hat, new Vector3(Random.Range(-60, 60), 1, Random.Range(-34, 34)), Quaternion.identity);
            hat.GetComponent<SphereCollider>().radius = 0.006f;
            hat.GetComponent<SphereCollider>().center = new Vector3(0, 0.005f, 0);
        }
    }

    void Update()
    {
        if(CatchUp.numHatsCollected == numHats)
        {
            Debug.Log("The game ends here!");
        }

        hatsCollected.SetText("Hats Collected: " + CatchUp.numHatsCollected + " / " + numHats);
    }
}
