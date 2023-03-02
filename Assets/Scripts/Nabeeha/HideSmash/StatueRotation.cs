using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float total;
    GameObject player1, player2, player3, player4;
    public Transform idolStart;
    public Camera mainCam;

    public float idolRotateInterval, minDistanceToDIE;
    private float timer;
    int rand;

    void Start()
    {

        idolRotateInterval = 2.5f;
        minDistanceToDIE = 30f;

        total = 0f;
        timer = 0.0f;
        rand = 0;
        

        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player3 = GameObject.FindGameObjectWithTag("Player 3");
        player4 = GameObject.FindGameObjectWithTag("Player 4");
    }

    // Update is called once per frame
    void Update()
    {
        rotation();
 

    }

    void rotation() 
    {
        transform.Rotate(0, speed, 0f, Space.Self);

        timer += Time.deltaTime;


        if (timer > idolRotateInterval) 
        {
            rand = Random.Range(1, 4);
            //Debug.Log(rand);
            timer = 0;
        }

        total += speed;

        if (total > 90 || rand == 1)
        {
            speed = -0.1f;
        }
        else if (total < -90 || rand == 2)
        {
            speed = 0.1f;
        }
 
    }

    private void OnTriggerStay(Collider collision)
    {

        

        if  (collision.tag == "Player 1" || collision.tag == "Player 2" || collision.tag == "Player 3" || collision.tag == "Player 4")
        {
            //Debug.Log(collision.tag);
            float dist = Vector3.Distance(idolStart.position, collision.transform.position);
            //Debug.Log(collision.tag);
            if (rayCastCheck(idolStart, collision.transform) == true && dist < minDistanceToDIE)
            {
                Debug.Log("Player Visible DEAD");
                collision.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("IdolLight").GetComponent<Light>().color = new Color(200, 0, 0);
            }
            else if (rayCastCheck(idolStart, collision.transform) == true && dist > minDistanceToDIE)
            {
                Debug.Log("Player Visible NOT IN RANGE");
                GameObject.FindGameObjectWithTag("IdolLight").GetComponent<Light>().color = new Color(50, 0, 0);
                //collision.transform.Find("VisionIndicator").LookAt(mainCam.transform);

                //collision.transform.Find("VisionIndicator").gameObject.SetActive(true);
            }
            else if (rayCastCheck(idolStart, collision.transform) == false)
            {
                Debug.Log("Player Hidden");
                GameObject.FindGameObjectWithTag("IdolLight").GetComponent<Light>().color = new Color(20, 20, 10);

                //collision.transform.Find("VisionIndicator").gameObject.SetActive(false);
            }



            //GameObject.FindGameObjectWithTag("IdolLight").GetComponent<Light>().color = new Color(50, 50, 5);
            
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player 1" || collision.tag == "Player 2" || collision.tag == "Player 3" || collision.tag == "Player 4")
        {
            //collision.transform.Find("VisionIndicator").gameObject.SetActive(false);

        }
    }

    bool rayCastCheck(Transform start, Transform end)
    {
        Physics.Raycast(start.position, (end.position - start.position), out RaycastHit hit);
        Debug.DrawRay(start.position, (end.position - start.position), Color.black);

        if (hit.collider.tag == "Wall")
        { 
            return false;
        }
        else if (hit.transform.tag == "Player 1" || hit.transform.tag == "Player 2" || hit.transform.tag == "Player 3" || hit.transform.tag == "Player 4") 
        {
            return true;
        }
        return false;
    }

}
