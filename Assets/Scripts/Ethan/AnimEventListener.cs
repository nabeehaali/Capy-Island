using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventListener : MonoBehaviour
{
    // not a huge fan that this script exists but seemed cleaner than having 2 seperate audio scripts ¯\_(?)_/¯
    [SerializeField]
    public CapySoundTrigger trigger;

    public void Awake()
    {
        trigger = gameObject.GetComponentInParent<CapySoundTrigger>();
    }

    private void Step()
    {
        trigger.PlayStep();
    }
}
