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
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.parent.tag == "Player" && collision.transform.tag != player.tag)
        {
            Debug.Log("Hit" + collision.transform.tag);
            enemyPlayer = collision.gameObject;

            audio.Play();

            if (!audio.isPlaying)
            {
                Destroy(this.gameObject);
            }

            //timer = destroyTime;

        }

    }
}
