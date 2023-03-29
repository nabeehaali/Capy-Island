using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HideSmashControls : MonoBehaviour
{

    public bool isPush;

    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        isPush = false;
        animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void Push(InputAction.CallbackContext context)
    {
        StartCoroutine(pushMotion());
    }

    IEnumerator pushMotion()
    {
        
        if(GetComponent<PlayerMovement>().playermovement == Vector2.zero)
        {
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            animator.SetTrigger("isHittingIdle");
            yield return new WaitForSeconds(1.2f);
            animator.ResetTrigger("isHittingIdle");
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            animator.SetTrigger("isHittingWalk");
            yield return new WaitForSeconds(0.9f);
            animator.ResetTrigger("isHittingWalk");
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        }
        isPush = true;
        yield return new WaitForSeconds(0.05f);
        isPush = false;
    }

}
