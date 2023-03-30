using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//public class HideSmashControls2 : MonoBehaviour
//{
//    public bool smashed;

//    public void Smash(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            StartCoroutine(SmashHit());
//            //smashed = true;
//        }
//        else if (context.canceled)
//        {
//           //smashed = false;
//        }
//    }

//    IEnumerator SmashHit()
//    {
//        smashed = true;
//        yield return new WaitForSeconds(0.5f);
//        smashed = false;
//    }
//}
public class HideSmashControls2 : MonoBehaviour
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

        if (GetComponent<PlayerMovement>().playermovement == Vector2.zero)
        {
            animator.SetTrigger("isHittingIdle");
            yield return new WaitForSeconds(0.25f);
            animator.ResetTrigger("isHittingIdle");
        }
        else
        {
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            animator.SetTrigger("isHittingWalk");
            yield return new WaitForSeconds(0.2f);
            animator.ResetTrigger("isHittingWalk");
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        }
        
        isPush = true;
       
        yield return new WaitForSeconds(0.25f);
        isPush = false;
    }

}