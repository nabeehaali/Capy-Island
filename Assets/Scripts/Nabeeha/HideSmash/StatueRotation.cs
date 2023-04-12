using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float total;
    bool stunCheck, lightOn;
    GameObject player1, player2, player3, player4;
    public Material redMat, originalMat;
    public Transform idolStart;
    public Camera mainCam;
    Animator animator;

    public float idolRotateInterval, minDistanceToDIE;
    private float timer;
    int rand;

    float animSpeed = 1;

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

        StartCoroutine(beginRotation());
        //StartCoroutine(turnLightOn());

    }

    // Update is called once per frame
    void Update()
    {
        //rotation();

    }

    void rotation() 
    {
        transform.Rotate(0, speed, 0f, Space.Self);

        timer += Time.deltaTime;

        if (timer > idolRotateInterval) 
        {
            rand = Random.Range(1, 2);
            //if (rand == 3 || rand == 4)
            //{
            //    lightOn = !lightOn;
            //    gameObject.transform.Find("Light").gameObject.SetActive(lightOn);
            //}
            //Debug.Log(rand);
            timer = 0;
        }

        total += speed;
        //Debug.Log(transform.eulerAngles.y - 90);

        if (total > 90 || rand == 1)
        {
            speed = -speed;
        }
        else if (total < -90 || rand == 2)
        {
            speed = speed;
        }

       
 
    }

    private void OnTriggerStay(Collider collision)
    {

        

        if  (collision.tag == "Player 1" || collision.tag == "Player 2" || collision.tag == "Player 3" || collision.tag == "Player 4")
        {
            Transform visionIndicator = collision.transform.Find("VisionIndicator");
            Renderer mat = visionIndicator.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            animator = gameObject.GetComponent<Animator>();

            float dist = Vector3.Distance(idolStart.position, collision.transform.position);


            if (rayCastCheck(idolStart, collision.transform) == true)
            {

                //Debug.Log("Player Visible DEAD");
                if (stunCheck == false) {
                    visionIndicator.gameObject.SetActive(true);
                    visionIndicator.LookAt(mainCam.transform);
                    StartCoroutine(stunPlayer(collision.gameObject));
                }
                //visionIndicator.gameObject.SetActive(false);


                
                

                
                mat.material = originalMat;

            }
            else 
            {
                visionIndicator.gameObject.SetActive(false);
            }
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
                return true;
            }

        return false;
    }

    IEnumerator stunPlayer(GameObject player)
    {
        //gameObject.transform.Find("Light").GetComponent<AudioSource>().Play();
        //Stunned
        player.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 0;
        player.transform.parent.GetComponent<PlayerMovement>().rumbleFunction(0.2f, 0.2f, 0.3f);
        yield return new WaitForSeconds(1f);

        //Slowed
        //player.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 5;
        //animator.SetBool("isWalking", true);
        //yield return new WaitForSeconds(2f);
        //animator.SetBool("isWalking", false);
        //animator.SetBool("isRunning", true);

        //Free
        player.transform.Find("VisionIndicator").gameObject.SetActive(false);
        player.gameObject.transform.parent.GetComponent<PlayerMovement>().speed = 20;
        stunCheck = true;
        yield return new WaitForSeconds(4f);
        
        stunCheck = false;



    }

    IEnumerator changeAnimSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            animSpeed += 0.5f;
            GetComponent<Animator>().SetFloat("Speed", animSpeed);
        }
        
    }
    public IEnumerator turnLightOn()
    {
        while(true)
        {
            yield return new WaitForSeconds(4f);
            gameObject.transform.Find("Light").gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
            gameObject.transform.Find("Light").gameObject.SetActive(false);
        }
    }

    IEnumerator beginRotation()
    {
        yield return new WaitForSeconds(6);
        GetComponent<Animator>().enabled = true;
        StartCoroutine(turnLightOn());
        StartCoroutine(changeAnimSpeed());
    }
}
