using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBehaviour : MonoBehaviour
{
    float timer, timer2 = 0;
    public float coolDown, destroyTime = 0;
    bool triggerBool;
    public GameObject enemyPlayer, player;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        triggerBool = false;
        audio = GetComponent<AudioSource>();
        destroyTime = 5;
        coolDown = 2.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;
        if (timer2 > destroyTime)
        {
            Destroy(this.gameObject);
        }


        if (triggerBool == true) 
        {
            triggeredSlow();
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player" && other.tag != player.tag)
        {

            enemyPlayer = other.gameObject;
            triggerBool = true;
            

        }

    }
    void triggeredSlow()
    {
        timer += Time.deltaTime;
        if (timer < coolDown)
        {
            //enemyPlayer.transform.parent.GetComponent<PlayerMovement>().speed = 0.01f;
            //Debug.Log("Hit" + enemyPlayer.tag);
            enemyPlayer.GetComponent<Rigidbody>().isKinematic = true;
            audio.Play(); // Add an eating sound
            transform.Find("Eating Effect").gameObject.SetActive(true); //Add a little particle effect
            //transform.Find("Eating Effect").gameObject.SetActive(true); //Add a little particle effect
            if(enemyPlayer.transform.parent.GetComponent<PlayerMovement>().playermovement == Vector2.zero)
            {
                enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingIdle");
            }
            else
            {
                enemyPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isEatingRun");
            }
            

        }
        else if (timer > coolDown && timer < destroyTime)
        {
            //enemyPlayer.transform.parent.GetComponent<PlayerMovement>().speed = 30f;
            enemyPlayer.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(gameObject);

            //eating animation
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingIdle");
            enemyPlayer.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("isEatingRun");
        }
    }

}

