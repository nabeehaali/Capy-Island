using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlligatorPlayerScript : MonoBehaviour
{
    public bool isLeader = true;
    public int points = 0;
    public int increase = 1;
    public float interval = 10f;
    public TextMeshPro display;
    public GameObject crownObj;
    float lastUpdate = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeader && Time.time - lastUpdate > interval)
        {
            points += increase;
            lastUpdate = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // uncomment this soon!
        // display.SetText(points.ToString());
    }
}
