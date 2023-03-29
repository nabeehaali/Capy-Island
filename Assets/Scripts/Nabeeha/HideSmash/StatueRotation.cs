using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float total;
    GameObject player1, player2, player3, player4;
    public Material redMat, originalMat;
    public Transform idolStart;
    public Camera mainCam;

    public float idolRotateInterval, minDistanceToDIE;
    private float timer;
    int rand;


    void Start()
    {

        //idolRotateInterval = 2.5f;
        //minDistanceToDIE = 15f;

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
            Transform visionIndicator = collision.transform.Find("VisionIndicator");
            Renderer mat = visionIndicator.GetChild(0).gameObject.GetComponent<MeshRenderer>();

            float dist = Vector3.Distance(idolStart.position, collision.transform.position);
           

            if (dist <= minDistanceToDIE)
            {
                //Debug.Log("Player Visible DEAD");
                StartCoroutine(stunPlayer(collision.gameObject, collision.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials[0]));
                visionIndicator.LookAt(mainCam.transform);
                visionIndicator.gameObject.SetActive(true);
                mat.material = redMat;
            }
            else if (dist > minDistanceToDIE)
            {
                //Debug.Log("Player Visible NOT IN RANGE");

                //GameObject.FindGameObjectWithTag("IdolLight").GetComponent<Light>().color = new Color(50, 0, 0);
                //collision.transform.Find("VisionIndicator").LookAt(mainCam.transform);

                //collision.transform.Find("VisionIndicator").gameObject.SetActive(true);
                mat.material = originalMat;
                if (rayCastCheck(idolStart, collision.transform) == true)
                {
                    

                    visionIndicator.gameObject.SetActive(true);
                    collision.transform.Find("VisionIndicator").LookAt(mainCam.transform);
                }
                else
                {
                    
                    collision.transform.Find("VisionIndicator").gameObject.SetActive(false);
                }

                
            }
            //else 
            //{
                //Debug.Log("Player Hidden");
                

                //collision.transform.Find("VisionIndicator").gameObject.SetActive(false);
            //}


            
        }

    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player 1" || collision.tag == "Player 2" || collision.tag == "Player 3" || collision.tag == "Player 4")
        {
            //collision.transform.Find("VisionIndicator").gameObject.SetActive(false);
            collision.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 20;
            collision.transform.Find("VisionIndicator").gameObject.SetActive(false);
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
                Debug.Log(hit.collider.name);
                return true;
            }

        
       
        return false;
    }

    IEnumerator stunPlayer(GameObject player, Material originalMat)
    {
        player.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 0;

        yield return new WaitForSeconds(2f);
        
        player.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 20;
    }

}
