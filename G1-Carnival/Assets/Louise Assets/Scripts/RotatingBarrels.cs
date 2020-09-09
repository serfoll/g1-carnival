using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBarrels : MonoBehaviour
{
   
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 30f) * Time.deltaTime);
        
    }
}
