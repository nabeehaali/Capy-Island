using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolSmashLaser : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField]
    private Transform startingPos;
    public float length;
    public Vector3 newPos;
    public int speed;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        StartCoroutine(generatePos());
    }

    void Update()
    {
        lr.SetPosition(0, startingPos.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward + newPos, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

            }
            if (hit.transform.parent.tag == "Player")
            {
                hit.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            //currentPos = newPos;
            //float step = speed * Time.deltaTime; //replace 30 with any speed value
            //lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), transform.forward * length + newPos, step));
            lr.SetPosition(1, transform.forward * length + newPos);
        }

        float step = speed * Time.deltaTime;
        lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), transform.forward + newPos, step));
    }

    IEnumerator generatePos()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            newPos = new Vector3(0, Random.Range(-0.5f, -1.5f),0 );
        }
        
    }
}

//generate random value for y offset and do a vector movetowards in that area