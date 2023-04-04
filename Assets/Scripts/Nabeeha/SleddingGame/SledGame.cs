using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SledGame : MonoBehaviour
{
    private GameObject playerParent;
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRender;

    public ParticleSystem splashParticles;

    public bool inWater;
    public bool offBerg;
    bool removeCam;
    bool addCam;
    public int colCount = 0;

    public static int ranking;
    private Vector3 direction;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        playerParent = transform.parent.gameObject;
        _rigidbody = transform.GetComponent<Rigidbody>();
        _trailRender = transform.GetComponent<TrailRenderer>();
        ranking = 4;
        inWater = false;
        offBerg = false;
        addCam = false;

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    void Update()
    {
        if (GetComponent<BoxCollider>() == null)
        {
            removeCam = true;
        }

        if (_rigidbody.velocity.magnitude > 30)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * 30;
        }

        if (offBerg)
        {

            //Destroy(gameObject.transform.parent.GetComponent<SledControls>()); //.enabled = false;
            playerParent.GetComponent<PlayerMovement>().speed = 0;
            _rigidbody.constraints = ~RigidbodyConstraints.FreezePosition;
            _rigidbody.drag = 0f;
            //change the -0.2 to something else for feel
            //direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            //_rigidbody.velocity = direction.normalized * 30 + new Vector3(0.0f, _rigidbody.velocity.y - 0.2f, 0.0f);
            offBerg = false; //check if this is ok

        }

        if (removeCam)
        {
            GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Remove(this.gameObject.transform);
            removeCam = false;
        }

        if (addCam)
        {
            for (int i = 0; i < GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Count; i++)
            {
                if (!GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Contains(this.gameObject.transform))
                {
                    GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Add(this.gameObject.transform);
                }
            }
            addCam = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(sceneName == "18-SledGame")
        {
            if (collision.gameObject.tag == "Water")
            {
                inWater = true;
                _trailRender.emitting = false;
                _rigidbody.drag = 4;
                _rigidbody.velocity = Vector3.zero;
                Vector3 particlePos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                Instantiate(splashParticles, particlePos, Quaternion.identity, transform);
                playerParent.GetComponent<PlayerMovement>().rumbleFunction(0.25f, 1f, 0.25f);
                SledSceneSetup.sledpoints.Add(new MinigamePoints(this.gameObject.name, ranking));
                ranking--;
            }

            if (collision.gameObject.tag == "IcebergSmall1")
            {
                collision.gameObject.GetComponent<Animator>().Play("IcebergSink1");
            }

            if (collision.gameObject.tag == "IcebergSmall2")
            {
                collision.gameObject.GetComponent<Animator>().Play("IcebergSink2");
            }

            if (collision.gameObject.tag == "IceBkg")
            {
                _rigidbody.mass = 1000;
            }

            colCount++;

            if (collision.gameObject.tag == "Iceberg")
            {
                addCam = true;
            }
        }
        

    }

    private void OnCollisionExit(Collision collision)
    {
        if (sceneName == "18-SledGame")
        {
            colCount--;
            if (colCount == 0)
            {
                //Debug.Log("not colliding with anything");
                //GameObject.Find("Main Camera").GetComponent<MultipleTargetCam>().targets.Remove(this.gameObject.transform);
                offBerg = true;
                removeCam = true;
            }
        }
    }
}
