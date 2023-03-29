using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;
    private Gamepad pad;

    private Coroutine stopRumbleAfterCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float lowFreq, float highFreq, float duration)
    {
        pad = Gamepad.current;

        if(pad != null)
        {
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
