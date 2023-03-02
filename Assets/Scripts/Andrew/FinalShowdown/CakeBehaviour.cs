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
        coolDown = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;
        if (timer2 > destroyTime)
        {
            Destroy(this.gameObject);
        }


        if (triggerBool) 
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
            enemyPlayer.transform.parent.GetComponent<PlayerMovement>().speed = 4;

            audio.Play(); // Add an eating sound
            transform.Find("Eating Effect").gameObject.SetActive(true); //Add a little particle effect

        }
        else if (timer > coolDown && timer < destroyTime)
        {
            enemyPlayer.transform.parent.GetComponent<PlayerMovement>().speed = 15.17f;
            Destroy(gameObject);
        }
    }

}

