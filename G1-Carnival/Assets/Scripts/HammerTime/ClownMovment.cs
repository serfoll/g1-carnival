using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMovment: MonoBehaviour
{
    
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float delta = 3f;
   
    
    void Update()
     // Move Gameobject Position along the y axis between 0 and Delta
    {
        float y = Mathf.PingPong(speed * Time.time, delta);
        Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = pos;
    }
}
