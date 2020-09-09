using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private Scoremanager scoreManager;
    public AudioClip hit;
 
    private void Start()
    {
        // find scoremanager and set it to scoremanager
        scoreManager = GameObject.FindWithTag("GameManager").GetComponent<Scoremanager>();    
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clown") == true)
        {
            // increase score
            scoreManager.IncrementScore();
            // play audio
            GameObject.Find("AudioManager").GetComponent<Sound>().audioSource.PlayOneShot(hit);
            // destroy object
            Destroy(other.gameObject);

        }
    }
}
