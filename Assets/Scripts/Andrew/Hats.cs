using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hats : MonoBehaviour
{
    public GameObject baseHatPrefab;
    public GameObject wizardHatPrefab;
    public GameObject chefHatPrefab;

    private GameObject currentObj, oldObj;

    public List<GameObject> hats;
    public int numHats;
    // Start is called before the first frame update
    void Start()
    {
        numHats = 3;  // Subject to change, should be the number of hats passed from the last scene

        // When the script is started, it instantiates the number of hats (numHats) and attaches them together
        for (int i = 0; i < numHats; i++)
        {
            currentObj = Instantiate(baseHatPrefab, new Vector3(0, i * 0.6f, 0), Quaternion.identity);
            if (oldObj != null) {
                currentObj.GetComponent<HingeJoint>().connectedBody = oldObj.GetComponent<Rigidbody>();
            }
            else {
                currentObj.GetComponent<HingeJoint>().connectedBody = GameObject.Find("HingeBase").GetComponent<Rigidbody>();
            }

            oldObj = currentObj;
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
