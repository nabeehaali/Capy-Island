using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBehaviour : MonoBehaviour
{
    float timer = 0;
    public float coolDown, destroyTime = 0;
    bool triggerBool;
    public GameObject enemyPlayer, player;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        triggerBool = false;
        audio = GetComponent<AudioSource>();
        destroyTime = 2.5f;
        //coolDown = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent.tag == "Player" && other.tag != player.tag)
        {
            Debug.Log("Hit" + other.tag);
            enemyPlayer = other.gameObject;

            audio.Play();

            if (!audio.isPlaying)
            {
                Destroy(this.gameObject);
            }
            
            //timer = destroyTime;

        }

    }
}
