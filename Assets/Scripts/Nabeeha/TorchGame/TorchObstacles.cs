using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchObstacles : MonoBehaviour
{
    public GameObject[] obstacleList;
    public int numObstaclesStart;

    Vector3 randomPos;

    float timePassed;
    
    void Start()
    {
        //instantiate some obstacles to start
        for (int i = 0; i < numObstaclesStart; i++)
        {
            int option = Random.Range(0, obstacleList.Length);
            randomPos = new Vector3(Random.Range(-60, 60), 3, Random.Range(0, 87));
            Instantiate(obstacleList[option], randomPos, Quaternion.identity, gameObject.transform);
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 30)
        {
            //shake the roof (or camera)
        }
        //check if 30s has passed
        //if yes, shake the roof

        //check if 45s has passed
        //if yes, shake the roof, instantiate some blocks on the roof (has gravity so they fall on the ground)
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if it collides with the ground, start coroutine where you wait a couple of seconds, then destroy GO
        //!!this code might have to go in the prefab of the rock GO!!
        //to keep the first obstacles and only manipulate the ones after, create a unique GO with its own properties and instantiate that one
    }
}
