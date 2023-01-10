using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorBrain : MonoBehaviour
{
    public GameObject water;

    Bounds b;
    bool moving = true;
    public bool rise = false;
    Vector3 startPosition;
    Vector3 targetPosition;
    private float elapsedTime;
    public float moveTime = 3f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Mesh mesh = water.GetComponent<Renderer>().bounds;
        b = water.GetComponent<Renderer>().bounds;
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / moveTime;
            Vector3 mVector = Vector3.Slerp(startPosition, targetPosition, percentageComplete);
            transform.position = new Vector3(mVector.x, transform.position.y, mVector.z);

            if (percentageComplete >= 1 && !rise)
            {
                rise = true;
                animator.SetTrigger("Rise");
                StartCoroutine(resetMoveTarget());
            }
        }
        else if (!moving)
        {
            targetPosition = randomPoint(b);
            Debug.Log(targetPosition);
            moving = true;
            rise = false;
        }
    }

    Vector3 randomPoint(Bounds bound)
    {
        Vector3 newTarget = new Vector3(
            Random.Range(bound.min.x, bound.max.x),
            0,
            Random.Range(bound.min.z, bound.max.z)
            );

        return new Vector3 (b.ClosestPoint(newTarget).x, transform.position.y, b.ClosestPoint(newTarget).z);
    }

    public IEnumerator resetMoveTarget()
    {
        // TODO: Change to scale w/ animation length
        yield return new WaitForSeconds(1.0f);
        animator.ResetTrigger("Rise");
        startPosition = transform.position;
        elapsedTime = 0;
        moving = false;

    }
}
