using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolSpawn : MonoBehaviour
{
    List<Vector3> locationVectors = new List<Vector3> ();
    List<GameObject> idolInstances = new List<GameObject>();
    public GameObject idolPrefab;
    float timer, timer2, idolRespawnTime;
    // Start is called before the first frame update
    void Start()
    {
        float timer = 0;
        idolRespawnTime = 4;
        locationVectors.Add(new Vector3(2,3.5f,6));
        locationVectors.Add(new Vector3(2, 3.5f, 6));
        locationVectors.Add(new Vector3(18.5f, 3.5f, 20));
        locationVectors.Add(new Vector3(-15, 3.5f, 27));
        locationVectors.Add(new Vector3(-5, 3.5f, 10));
        locationVectors.Add(new Vector3(4, 3.5f, 30));
        locationVectors.Add(new Vector3(6, 3.5f, 2));

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        Debug.Log(timer2);
        if (timer > idolRespawnTime) 
        {
            int randomVecX = Random.Range(-25, 32);
            int randomVecZ = Random.Range(5, 39);
            Instantiate(idolPrefab, new Vector3(randomVecX, 3.5f, randomVecZ), Quaternion.identity);
            timer = 0f;
        }

        if (timer2 >= 15) 
        {
            idolRespawnTime -= 0.5f;
            timer2 = 0;
        }
    }
}
