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
            Physics.IgnoreCollision(transform.GetChild(i).gameObject.GetComponent<MeshCollider>(), water.GetComponent<MeshCollider>());
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
        
        transform.GetChild(randPiece).localPosition = Vector3.MoveTowards(transform.GetChild(randPiece).localPosition, target, moveSpeed * Time.deltaTime);
        if (transform.GetChild(randPiece).localPosition == target)
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
            target = new Vector3(transform.GetChild(randPiece).localPosition.x, -12f, transform.GetChild(randPiece).localPosition.z);
            isWait = false;
        }
    }

}