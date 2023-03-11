using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolSpawn2 : MonoBehaviour
{
    List<Vector3> locationVectors = new List<Vector3> ();
    List<GameObject> idolInstances = new List<GameObject>();
    public GameObject idolPrefab;
    int vecMinZ;
    float timer, timer2, idolRespawnTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        vecMinZ = 5;
        idolRespawnTime = 4;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        //Debug.Log(timer2);
        GameObject[] vases = GameObject.FindGameObjectsWithTag("Vase");

        if (timer > idolRespawnTime || vases.Length < 2) 
        {
            int randomVecX = Random.Range(-25, 32);
            int randomVecZ = Random.Range(vecMinZ, 39);
            Instantiate(idolPrefab, new Vector3(randomVecX, 3.5f, randomVecZ), Quaternion.identity);
            timer = 0f;
        }

        if (timer2 >= 15) 
        {
            idolRespawnTime -= 0.5f;
            if (vecMinZ < 30) 
            {
                vecMinZ += 10;
            }
            timer2 = 0;
        }
    }
}
