using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGame : MonoBehaviour
{
    public GameObject[] firePrefabs;
    public float fireIncrement;
    public float lightIncrement;
    public float particleIncrement;

    public TorchControls torchcontrols;
 
    void Start()
    {
        torchcontrols = transform.parent.gameObject.GetComponent<TorchControls>();
    }

    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        //activate torch
        if (collision.gameObject.tag == "Torch" && torchcontrols.canLight == true)
        {
            if(this.gameObject.tag == "Player 1")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[0], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 2")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[1], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 3")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[2], collision.gameObject.transform);
                }
            }
            else if (this.gameObject.tag == "Player 4")
            {
                if (collision.gameObject.transform.childCount < 1)
                {
                    Instantiate(firePrefabs[3], collision.gameObject.transform);
                }
            }
        }

        //blow out torch
        if (collision.gameObject.tag == "Torch" && torchcontrols.canBlow == true)
        {
            if (this.gameObject.tag == "Player 1")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P1Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);

                        ////Assumption: fireLight is first child of prefab and fireparticles are second child of prefab
                        //GameObject fire = collision.gameObject.transform.GetChild(i).gameObject;
                        //GameObject fireLight = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
                        //ParticleSystem fireParticles = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

                        //fire.transform.localScale = new Vector3(fire.transform.localScale.x - fireIncrement, fire.transform.localScale.y - fireIncrement, fire.transform.localScale.z - fireIncrement);
                        //fireLight.GetComponent<Light>().intensity -= lightIncrement;
                        //particleIncrement -= 0.05f;
                        //ParticleSystem.MainModule psMain = fireParticles.main;
                        //psMain.startLifetime = particleIncrement;

                        ////if light intensity hits 0, destory the GO
                        //if (fireLight.GetComponent<Light>().intensity <= 0)
                        //{
                        //    Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                        //}

                    }
                }

            }
            else if (this.gameObject.tag == "Player 2")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P2Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);

                        ////Assumption: fireLight is first child of prefab and fireparticles are second child of prefab
                        //GameObject fire = collision.gameObject.transform.GetChild(i).gameObject;
                        //GameObject fireLight = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
                        //ParticleSystem fireParticles = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

                        //fire.transform.localScale = new Vector3(fire.transform.localScale.x - fireIncrement, fire.transform.localScale.y - fireIncrement, fire.transform.localScale.z - fireIncrement);
                        //fireLight.GetComponent<Light>().intensity -= lightIncrement;
                        //particleIncrement -= 0.05f;
                        //ParticleSystem.MainModule psMain = fireParticles.main;
                        //psMain.startLifetime = particleIncrement;

                        ////if light intensity hits 0, destory the GO
                        //if (fireLight.GetComponent<Light>().intensity <= 0)
                        //{
                        //    Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                        //}
                    }
                }

            }
            else if (this.gameObject.tag == "Player 3")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P3Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);

                        ////Assumption: fireLight is first child of prefab and fireparticles are second child of prefab
                        //GameObject fire = collision.gameObject.transform.GetChild(i).gameObject;
                        //GameObject fireLight = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
                        //ParticleSystem fireParticles = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

                        //fire.transform.localScale = new Vector3(fire.transform.localScale.x - fireIncrement, fire.transform.localScale.y - fireIncrement, fire.transform.localScale.z - fireIncrement);
                        //fireLight.GetComponent<Light>().intensity -= lightIncrement;
                        //particleIncrement -= 0.05f;
                        //ParticleSystem.MainModule psMain = fireParticles.main;
                        //psMain.startLifetime = particleIncrement;

                        ////if light intensity hits 0, destory the GO
                        //if (fireLight.GetComponent<Light>().intensity <= 0)
                        //{
                        //    Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                        //}
                    }
                }

            }
            else if (this.gameObject.tag == "Player 4")
            {
                if (collision.gameObject.transform.childCount >= 1 && collision.gameObject.transform.GetChild(0).tag != "P4Point")
                {
                    for (int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                        ////Assumption: fireLight is first child of prefab and fireparticles are second child of prefab
                        //GameObject fire = collision.gameObject.transform.GetChild(i).gameObject;
                        //GameObject fireLight = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
                        //ParticleSystem fireParticles = collision.gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

                        //fire.transform.localScale = new Vector3(fire.transform.localScale.x - fireIncrement, fire.transform.localScale.y - fireIncrement, fire.transform.localScale.z - fireIncrement);
                        //fireLight.GetComponent<Light>().intensity -= lightIncrement;
                        //particleIncrement -= 0.05f;
                        //ParticleSystem.MainModule psMain = fireParticles.main;
                        //psMain.startLifetime = particleIncrement;

                        ////if light intensity hits 0, destory the GO
                        //if (fireLight.GetComponent<Light>().intensity <= 0)
                        //{
                        //    Destroy(collision.gameObject.transform.GetChild(i).gameObject);
                        //}
                    }
                }

            }
        }
    }
}
