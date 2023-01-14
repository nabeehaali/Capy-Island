using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSpawn : MonoBehaviour
{
    public GameObject Hat;
    int numHats;
    float timePassed;

    bool nextRound = false;
    void Start()
    {
        numHats = Random.Range(10, 20);
        Debug.Log(numHats);

        //spawn 50% of the hats to start
        for(int i = 0; i < numHats * 0.5; i++)
        {
            Instantiate(Hat, new Vector3(Random.Range(-60, 60), 0, Random.Range(-34, 34)), Quaternion.identity);
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        //once 20s has passed
        if(timePassed > 20)
        {
            if(!nextRound)
            {
                StartCoroutine(moreHats());
                nextRound = true;
            }
            //instantiate more hats at a longer rate
        }

        if(CatchUp.numHatsCollected == numHats)
        {
            Debug.Log("The game ends here!");
        }
        
    }

    IEnumerator moreHats()
    {
        while (GameObject.FindGameObjectsWithTag("BaseHat").Length + CatchUp.numHatsCollected != numHats)
        {
            for (int i = 0; i < 2; i++)
            {
                //instantiate on the water plane, and drop off on the shoreline
                Instantiate(Hat, new Vector3(Random.Range(-60, 60), 0, Random.Range(-34, 34)), Quaternion.identity);
            }
            
            yield return new WaitForSeconds(5);
        }
        
    }
}
