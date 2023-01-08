using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledIceberg : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int dropInterval;
    public GameObject water;
    int randPiece;
    bool isWait = true;
    Vector3 target;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Physics.IgnoreCollision(transform.GetChild(i).gameObject.GetComponent<BoxCollider>(), water.GetComponent<MeshCollider>());
        }
        StartCoroutine(dropPiece());
    }

    private void Update()
    {
        if(!isWait)
        {
            Move();
        }
    }

    private void Move()
    {
        //Debug.Log(transform.GetChild(randPiece).position);
        transform.GetChild(randPiece).position = Vector3.MoveTowards(transform.GetChild(randPiece).position, target, moveSpeed * Time.deltaTime);
        if (transform.GetChild(randPiece).position == target)
        {
            Destroy(transform.GetChild(randPiece).gameObject);
            StartCoroutine(dropPiece());
        }
    }

    IEnumerator dropPiece()
    {
        isWait = true;
        yield return new WaitForSeconds(dropInterval);
        if(transform.childCount > 1)
        {
            randPiece = Random.Range(0, transform.childCount - 1);
            target = new Vector3(transform.GetChild(randPiece).position.x, -10f, transform.GetChild(randPiece).position.z);
            isWait = false;
        }
    }















    /*public GameObject water;
    public int timeInterval;
    Vector3 target, current;
    public float speed = 10f;
    int randPiece;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Physics.IgnoreCollision(transform.GetChild(i).gameObject.GetComponent<BoxCollider>(), water.GetComponent<MeshCollider>());
        }
        //StartCoroutine(fallIceberg());
        //randPiece = Random.Range(0, transform.childCount);
        target = new Vector3(0, 0, 0);
        StartCoroutine(dropIceberg());
    }

    IEnumerator fallIceberg()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            int randPiece = Random.Range(0, transform.childCount);
           
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                transform.GetChild(randPiece).gameObject.GetComponent<Rigidbody>().useGravity = true;
                yield return new WaitForSeconds(2);
                Destroy(transform.GetChild(randPiece).gameObject);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        //new strat: tranform that child gameobject 1.5units down
        //only use physics for tilt effect??
    }

    IEnumerator dropIceberg()
    {
        /*var step = speed * Time.deltaTime;
        yield return new WaitForSeconds(timeInterval);
        randPiece = Random.Range(0, transform.childCount);
            
        while (transform.childCount > 0)
        {
            //current = transform.GetChild(randPiece).position;
            target = new Vector3(transform.GetChild(randPiece).position.x, -1.5f, transform.GetChild(randPiece).position.z);
            Debug.Log(target);
            transform.GetChild(randPiece).position = Vector3.MoveTowards(transform.GetChild(randPiece).position, target, step);
            yield return null;

            //if (current == target)
            //{
            //    Destroy(transform.GetChild(randPiece).gameObject);
            //}
            
        }
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (transform.childCount > 0)
            {
                randPiece = Random.Range(0, transform.childCount);
                target = new Vector3(transform.GetChild(randPiece).position.x, -1.5f, transform.GetChild(randPiece).position.z);
                Debug.Log(target);
            }
            while (transform.childCount > 0)
            {
                current = transform.GetChild(randPiece).position;
                var step = speed * Time.deltaTime;
                transform.GetChild(randPiece).position = Vector3.MoveTowards(transform.GetChild(randPiece).position, target, step);
                yield return null;

                if (current == target)
                {
                    Destroy(transform.GetChild(randPiece).gameObject);
                }
            }
        }
    }*/

}