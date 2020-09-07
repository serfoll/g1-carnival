using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactSound : MonoBehaviour
{
    public AudioClip ballBounce;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ballBounce);
        }
    }
}
