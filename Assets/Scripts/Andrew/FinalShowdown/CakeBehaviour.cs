using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBehaviour : MonoBehaviour
{
    float timer, timer2 = 0;
    public float coolDown, destroyTime = 0;
    bool triggerBool;
    public bool destroy;
    public GameObject enemyPlayer, player;
    AudioSource audio;
    bool isSlow = false;
    // Start is called before the first frame update
    void Start()
    {
        triggerBool = false;
        audio = GetComponent<AudioSource>();

        StartCoroutine(removeCake());
    }

    // Update is called once per frame
    void Update()
    {
        //timer2 += Time.deltaTime;
        //if (timer2 > destroyTime && triggerBool == false)
        //{
        //    //if (enemyPlayer) 
        //    //{
        //        enemyPlayer.GetComponent<Rigidbody>().isKinematic = false;
        //    //}
        //    Destroy(gameObject);
        //}


        //if (triggerBool == true) 
        //{
        //    triggeredSlow();
        //    timer += Time.deltaTime;
        //}
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent) {
            if (other.transform.parent.tag == "Player" && other.tag != player.tag)
            {

                enemyPlayer = other.gameObject;
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
                {
                    if (GameObject.FindGameObjectsWithTag("Player")[i] != enemyPlayer)
                    {
                        Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectsWithTag("Player")[i].transform.GetChild(0).GetComponent<MeshCollider>());
                    }
                }
                
                triggerBool = true;

                gameObject.transform.parent = enemyPlayer.transform;

                StopAllCoroutines();
                StartCoroutine(freezePlayer());
            }

        }
        

    }

    IEnumerator removeCake()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator freezePlayer()
    {
        enemyPlayer.GetComponent<Rigidbody>().isKinematic = true;
        audio.Play();
        transform.Find("Eating Effect").gameObject.SetActive(true);
        if (enemyPlayer.transform.parent.GetComponent<PlayerMovement>().playermovement == Vector2.zero)
        {
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingIdle");
        }
        else
        {
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingRun");
        }
        yield return new WaitForSeconds(coolDown);
        enemyPlayer.GetComponent<Rigidbody>().isKinematic = false;
        //eating animation
        enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingIdle");
        enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingRun");
        
        Destroy(gameObject);
    }

    void triggeredSlow()
    {
        
        if (timer < coolDown)
        {

            enemyPlayer.GetComponent<Rigidbody>().isKinematic = true;
            audio.Play(); // Add an eating sound
            transform.Find("Eating Effect").gameObject.SetActive(true); //Add a little particle effect

            if(enemyPlayer.transform.parent.GetComponent<PlayerMovement>().playermovement == Vector2.zero)
            {
                enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingIdle");
            }
            else
            {
                enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingRun");
            }
            

        }
        else if (timer > coolDown)
        {
            //enemyPlayer.transform.parent.GetComponent<PlayerMovement>().speed = 30f;
            enemyPlayer.GetComponent<Rigidbody>().isKinematic = false;

            //eating animation
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingIdle");
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingRun");


            //triggerBool = false;
            // Destroys THIS gameObject
            Destroy(gameObject);

            
        }
    }

}

