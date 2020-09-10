using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClownSpawner : MonoBehaviour
{
  [SerializeField] public GameObject clowns;
  [SerializeField] public Transform clownSpawn ;
  public Scoremanager scoreManager;
  public AudioClip hereWeGo;

    // Initiate GameObject at Targeted transform position and maintain it's rotation
    public void Spawn()
    {
        GameObject.Instantiate(clowns, clownSpawn.transform.position, Quaternion.identity);
        scoreManager.score = 0;
        GameObject.Find("AudioManager").GetComponent<Sound>().audioSource.PlayOneShot(hereWeGo);
    }
   
}



