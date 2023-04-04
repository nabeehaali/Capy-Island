using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;
    private Gamepad pad1,pad2,pad3,pad4;

    private Coroutine stopRumbleAfterCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        //pad = InputSystem.AddDevice<Gamepad>();
    }

    public void RumblePulse(float lowFreq, float highFreq, float duration, Gamepad pad)
    {
        //pad = InputSystem.GetDevice<Gamepad>();
        if (pad != null)
        {
            Debug.Log(pad.device.deviceId);
            pad.SetMotorSpeeds(lowFreq, highFreq);
            stopRumbleAfterCoroutine = StartCoroutine(stopRumble(duration, pad));
        }



    }

    private IEnumerator stopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0, 0);
    }
}
