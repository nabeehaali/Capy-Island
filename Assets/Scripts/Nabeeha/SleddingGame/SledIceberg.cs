using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledIceberg : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int dropInterval;
    [SerializeField] float shakeSpeed = 1f;
    [SerializeField] float shakeAmount = 1f;
    public GameObject water;
    int randPiece;
    bool isWait = true;
    bool isActivate = false;
    Vector3 target;

    private void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Iceberg").Length; i++)
        {
            Physics.IgnoreCollision(GameObject.FindGameObjectsWithTag("Iceberg")[i].gameObject.GetComponent<MeshCollider>(), water.GetComponent<MeshCollider>());
        }
        StartCoroutine(dropPiece());
    }

    private void Update()
    {
        if (!isWait)
        {
            Activate();
        }

        if(transform.GetChild(0).name == "Outer" || transform.GetChild(0).name == "Inner")
        {
            if (transform.GetChild(0).childCount == 0)
            {
                Destroy((transform.GetChild(0).gameObject));
            }
        }
    }

    private void Activate()
    {

        if (transform.GetChild(0).name == "Outer" || transform.GetChild(0).name == "Inner")
        {
            transform.GetChild(0).GetChild(randPiece).localPosition = new Vector3(transform.GetChild(0).GetChild(randPiece).localPosition.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, transform.GetChild(0).GetChild(randPiece).localPosition.y, transform.GetChild(0).GetChild(randPiece).localPosition.z);
            if (!isActivate)
            {
                StartCoroutine(shake());
            }
            else
            {
                Move();
            }
        }

    }

    private void Move()
    {
        if (transform.GetChild(0).name == "Outer" || transform.GetChild(0).name == "Inner")
        {
            transform.GetChild(0).GetChild(randPiece).localPosition = Vector3.MoveTowards(transform.GetChild(0).GetChild(randPiece).localPosition, target, moveSpeed * Time.deltaTime);
            if (transform.GetChild(0).GetChild(randPiece).localPosition == target)
            {
                Destroy(transform.GetChild(0).GetChild(randPiece).gameObject);
                StartCoroutine(dropPiece());
            }
        }
    }
    IEnumerator shake()
    {
        yield return new WaitForSeconds(1);
        isActivate = true;
    }

    IEnumerator dropPiece()
    {
        isActivate = false;
        isWait = true;
        yield return new WaitForSeconds(dropInterval);
        if (transform.childCount > 1)
        {
            if (transform.GetChild(0).name == "Outer" || transform.GetChild(0).name == "Inner")
            {
                randPiece = Random.Range(0, transform.GetChild(0).childCount);
                target = new Vector3(transform.GetChild(0).GetChild(randPiece).localPosition.x, 120f, transform.GetChild(0).GetChild(randPiece).localPosition.z);
                isWait = false;    
            }
        }
    }

}