using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CapySoundTrigger : MonoBehaviour
{
    public AudioClip[] chirps = new AudioClip[11];
    public AudioClip[] hits = new AudioClip[3];

    public AudioClip[] groundSteps = new AudioClip[4];

    public AudioClip waterMove;
    public AudioClip[] waterStops = new AudioClip[3];

    public AudioClip iceMove;
    public AudioClip[] iceStops = new AudioClip[4];

    public AudioSource capyAudio;
    public AudioSource movementAudio;
    public string moveType;

    public PlayerMovement playerMove;
    bool playerStopped = true;

    public void Start()
    {
        playerMove = gameObject.GetComponentInParent<PlayerMovement>();
    }

    public void FixedUpdate()
    {
        float mag = playerMove.playermovement.magnitude;

        if (moveType == "water")
        {
            if (mag > 0 && playerStopped)
            {
                movementAudio.loop = true;
                movementAudio.clip = waterMove;
                movementAudio.Play();
                playerStopped = false;
            }
            else if (mag == 0 && !playerStopped)
            {
                // playing the stopping sound effect
                playerStopped = true;
                movementAudio.loop = false;
                movementAudio.Stop();

                int waterStop = Random.Range(0, waterStops.Length - 1);
                movementAudio.clip = waterStops[waterStop];
                movementAudio.Play();
            }
        }
        else if (moveType == "ice")
        {
            // ice SFX
            if (mag > 0 && playerStopped)
            {
                movementAudio.loop = true;
                movementAudio.clip = iceMove;
                movementAudio.Play();
                playerStopped = false;
            }
            else if (mag == 0 && !playerStopped)
            {
                // playing the stopping sound effect
                playerStopped = true;
                movementAudio.loop = false;
                movementAudio.Stop();

                int iceStop = Random.Range(0, iceStops.Length - 1);
                movementAudio.clip = iceStops[iceStop];
                movementAudio.Play();
            }
        }
    }

    public void PlayChirp()
    {
        // call this whenever you want a chirp sound, ideally for interactions
        if (!capyAudio.isPlaying)
        {
            int chirp = Random.Range(0, chirps.Length - 1);
            capyAudio.clip = chirps[chirp];
            capyAudio.Play();
        }
    }

    public void PlayHit()
    {
        // call this for damage noises / when a capybara gets hit
        int hit = Random.Range(0, hits.Length - 1);
        capyAudio.clip = hits[hit];
        capyAudio.Play();
    }

    public void PlayStep()
    {
        if (moveType != "ice" && moveType != "water")
        {
            float mag = playerMove.playermovement.magnitude;
            float randomPitch = Random.Range(0.90f, 1.10f);
            int ground = Random.Range(0, groundSteps.Length - 1);
            movementAudio.clip = groundSteps[ground];
            movementAudio.pitch = randomPitch;
            movementAudio.Play();
        }
    }


    /**
    * Creates a sub clip from an audio clip based off of the start time
    * and the stop time. The new clip will have the same frequency as
    * the original.
    */
    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
}
