using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int Time = 5;
  
    void Update()
    {

        Destroy(gameObject, Time);

    }
}
