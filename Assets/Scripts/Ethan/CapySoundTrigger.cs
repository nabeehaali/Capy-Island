using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapySoundTrigger : MonoBehaviour
{
    public AudioClip[] chirps = new AudioClip[11];
    public AudioClip[] hits = new AudioClip[3];
    public AudioSource AudioSource;

    public void PlayChirp()
    {
        // call this whenever you want a chirp sound, ideally for interactions
        if (!AudioSource.isPlaying)
        {
            int chirp = Random.Range(0, chirps.Length - 1);
            AudioSource.clip = chirps[chirp];
            AudioSource.Play();
        }
    }

    public void PlayHit()
    {
        // call this for damage noises / when a capybara gets hit
        int hit = Random.Range(0, hits.Length - 1);
        AudioSource.clip = hits[hit];
        AudioSource.Play();
    }
}
