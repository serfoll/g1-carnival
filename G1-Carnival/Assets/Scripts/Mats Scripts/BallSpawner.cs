using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public bool leverActive { get; set; } = false;


    private void Update()
    {
        if (leverActive)
        {
            Instantiate(ballPrefab, transform);
            leverActive = false;            
        }
    }
}