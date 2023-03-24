using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalShowdownControls : MonoBehaviour
{
    //public int magnitude;
    private Rigidbody _rigidbody;
    Hat activeHat;
    public List<Hat> enabledHats;
    private int i;

    public bool canPush = false, moveHatL = false, moveHatR = false;
    //public Animator animator;

    void Start()
    {
        _rigidbody = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        activeHat = gameObject.GetComponent<WizardHat>();

        if (gameObject.GetComponent<WizardHat>().enabled == true)
        {
            enabledHats.Add(gameObject.GetComponent<WizardHat>());
        }
        if (gameObject.GetComponent<ChefHat>().enabled == true)
        {
            enabledHats.Add(gameObject.GetComponent<ChefHat>());
        }
        if (gameObject.GetComponent<ConeHat>().enabled == true)
        {
            enabledHats.Add(gameObject.GetComponent<ConeHat>());
        }
        if (gameObject.GetComponent<HockeyHat>().enabled == true)
        {
            enabledHats.Add(gameObject.GetComponent<HockeyHat>());
        }
        //Debug.Log(enabledHats.Count);
        i = 0;
        //enabledHats[i].isActive = true;
    }
    void FixedUpdate()
    {
        //if (canPush)
        //{
        //    _rigidbody.AddForce(gameObject.transform.GetChild(0).gameObject.transform.forward * magnitude, ForceMode.VelocityChange);
        //    canPush = false;//can also try impulse
        //}

        

        if (moveHatR && enabledHats.Count > 1)
        {
            activeHat.isActive = false;
            if (i == enabledHats.Count - 1)
            {
                i = 0;
            }
            else 
            {
                i++;
            }
            
        } else if (moveHatL && enabledHats.Count > 1)
        {
            activeHat.isActive = false;
            if (i == 0)
            {
                i = enabledHats.Count - 1;
            }
            else
            {
                i--;
            }
            
        }
        
        enabledHats[i].isActive = true;



    }

    public void NextHat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveHatL = true;
        }
        else if (context.canceled)
        {
            moveHatL = false;
        }
    }

    public void PrevHat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveHatR = true;
        }
        else if (context.canceled)
        {
            moveHatR = false;
        }
    }

    public void Push(InputAction.CallbackContext context)
    {
        //StartCoroutine(pushMotion());
        //if (context.started)
        //{
        //animator.SetBool("isHitting", true);


        //}
        if (context.performed)
        {
            canPush = true;
        }
        else
        {
            canPush = false;
            //animator.SetBool("isHitting", false);
        }
    }

    public void cycleHat() 
    {
        
    }

    IEnumerator pushMotion()
    {
        canPush = true;
        yield return new WaitForSeconds(0.05f);
        canPush = false;
    }
}
