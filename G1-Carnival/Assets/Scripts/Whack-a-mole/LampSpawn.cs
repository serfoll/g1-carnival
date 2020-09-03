using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSpawn : MonoBehaviour
{
    [SerializeField] private GameObject lamp;
    [SerializeField] float timer;
    [SerializeField] int waitingTime;

    void Update()

    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            GameObject temp = Instantiate(lamp, transform);

            timer = 0;
            temp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            
        }
    }

}

