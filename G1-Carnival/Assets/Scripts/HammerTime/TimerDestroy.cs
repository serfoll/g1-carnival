using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField] private float time;
    
    //Destroy gameobject after X Seconds
    void Start()
    {
        Destroy(gameObject, time);
    }
}
    

