using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    
    // Get AudioSource component
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
