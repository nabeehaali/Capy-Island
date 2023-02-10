using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShake : MonoBehaviour
{
    public float speed = 1f;
    public float shake = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time * speed) * shake, transform.position.y, transform.position.z);
    }
}
