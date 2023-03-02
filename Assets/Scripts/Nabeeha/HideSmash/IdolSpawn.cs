using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolSpawn : MonoBehaviour
{
    List<Vector3> locationVectors = new List<Vector3> ();
    List<GameObject> idolInstances = new List<GameObject>();
    public GameObject[] idolPrefabs;
    int vecMinZ;
    float timer, timer2, idolRespawnTime;
    bool isUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        vecMinZ = 5;
        idolRespawnTime = 4;

        for(int i = 0; i < 8; i++)
        {
            int randomVecX = Random.Range(-25, 32);
            int randomVecZ = Random.Range(vecMinZ, 39);
            int randPrefab = Random.Range(0, idolPrefabs.Length - 1);
            Instantiate(idolPrefabs[randPrefab], new Vector3(randomVecX, 3.5f, randomVecZ), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        //Debug.Log(timer2);
        GameObject[] vases = GameObject.FindGameObjectsWithTag("Vase");

        if (timer > idolRespawnTime || vases.Length < 2) 
        {
            int randomVecX = Random.Range(-25, 32);
            int randomVecZ = Random.Range(vecMinZ, 39);
            int randPrefab = Random.Range(0, idolPrefabs.Length - 1);
            Instantiate(idolPrefabs[randPrefab], new Vector3(randomVecX, 3.5f, randomVecZ), Quaternion.identity);
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
        }*/

        GameObject[] vases = GameObject.FindGameObjectsWithTag("Vase");

        if (vases.Length < 6)
        {
            if(isUpdate)
            {
                isUpdate = false;
                if (vecMinZ < 30)
                {
                    vecMinZ += 2;
                }
                StartCoroutine(spawnVase());
            }
        }
    }

    IEnumerator spawnVase()
    {
        yield return new WaitForSeconds(1f);
        int randomVecX = Random.Range(-25, 32);
        int randomVecZ = Random.Range(vecMinZ, 39);
        int randPrefab = Random.Range(0, idolPrefabs.Length - 1);
        Instantiate(idolPrefabs[randPrefab], new Vector3(randomVecX, 3.5f, randomVecZ), Quaternion.identity);
        isUpdate = true;
    }
}
