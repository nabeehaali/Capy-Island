using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HatSpawn : MonoBehaviour
{
    public GameObject Hat;
    int numHats;
    float timePassed;

    //bool nextRound = false;

    public TMP_Text hatsCollected;
    void Start()
    {
        numHats = Random.Range(10, 20);
        Debug.Log(numHats);

        //spawn 50% of the hats to start
        for(int i = 0; i < numHats; i++)
        {
            GameObject hat = Instantiate(Hat, new Vector3(Random.Range(-60, 60), 1, Random.Range(-34, 34)), Quaternion.identity);
            hat.GetComponent<SphereCollider>().radius = 0.006f;
            hat.GetComponent<SphereCollider>().center = new Vector3(0, 0.005f, 0);
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        //once 20s has passed
        //if(timePassed > 20)
        //{
        //    if(!nextRound)
        //    {
        //        StartCoroutine(moreHats());
        //        nextRound = true;
        //    }
        //    //instantiate more hats at a longer rate
        //}

        if(CatchUp.numHatsCollected == numHats)
        {
            Debug.Log("The game ends here!");
        }

        hatsCollected.SetText("Hats Collected: " + CatchUp.numHatsCollected + " / " + numHats);
        
    }

    /*IEnumerator moreHats()
    {
        while (GameObject.FindGameObjectsWithTag("BaseHat").Length + CatchUp.numHatsCollected != numHats)
        {
            for (int i = 0; i < 2; i++)
            {
                //instantiate on the water plane, and drop off on the shoreline
                GameObject hat = Instantiate(Hat, new Vector3(Random.Range(-60, 60), 1, Random.Range(-34, 34)), Quaternion.identity);
                hat.GetComponent<SphereCollider>().radius = 0.006f;
                hat.GetComponent<SphereCollider>().center = new Vector3(0, 0.005f, 0);
            }
            
            yield return new WaitForSeconds(5);
        }
        
    }*/
}
