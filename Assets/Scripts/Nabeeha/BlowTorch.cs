using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTorch : MonoBehaviour
{
    public GameObject fire, fireLight;
    public ParticleSystem fireParticles;
    
    float increment = 0.1f;
    float particleincrement = 3.5f;

    void Start()
    {
        StartCoroutine(lightup());
    }


    void Update()
    {

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Light" && Input.GetButtonDown("Blow"))
        {
            fire.transform.localScale = new Vector3(fire.transform.localScale.x - increment, fire.transform.localScale.y - increment, fire.transform.localScale.z - increment);
            fireLight.GetComponent<Light>().intensity -= 100;

            particleincrement -= 0.5f;
            ParticleSystem.MainModule psMain = fireParticles.main;
            psMain.startLifetime = particleincrement;
        }
    }

    IEnumerator lightup()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            fire.transform.localScale = new Vector3(fire.transform.localScale.x + increment, fire.transform.localScale.y + increment, fire.transform.localScale.z + increment);
            fireLight.GetComponent<Light>().intensity += 100;
            particleincrement += 0.5f;
            ParticleSystem.MainModule psMain = fireParticles.main;
            psMain.startLifetime = particleincrement;

            if (fireLight.GetComponent<Light>().intensity >= 1000)
            {
                fireLight.GetComponent<Light>().intensity = 1000;
            }
            if (fire.transform.localScale.x > 2)
            {
                fire.transform.localScale = new Vector3(2, 2, 2);
            }
            if (particleincrement >= 3f)
            {
                particleincrement = 3f;
            }
        }
       
    }
}
