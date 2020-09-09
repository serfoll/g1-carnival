using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHammer : MonoBehaviour
{
    [SerializeField] private GameObject hammer;
    [SerializeField] private Transform hammerSpawn;
     public AudioClip clip;


    // Initiate GameObject at Targeted transform position and maintain it's rotation
    public void Spawn()
    {
       GameObject.Instantiate(hammer, hammerSpawn.transform.position, transform.rotation);
        // play audio
        GameObject.Find("AudioManager").GetComponent<Sound>().audioSource.PlayOneShot(clip);
    }
}
