using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    
    public GameObject ballPrefab;
    public bool leverActive { get; set; } = false;

    // Spawns a ballPrefab when lever is active.

    private void Update()
    {
        if (leverActive)
        {
            Instantiate(ballPrefab, transform);
            leverActive = false;            
        }
    }
}