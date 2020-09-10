using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIfGrabbed : MonoBehaviour
{

    
        public float speed = 40;
        public GameObject bullet;
        public Transform barrel;
        public AudioSource audioSource;
        public AudioClip audioClip;

        public void Fire()
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation); //Instantierar ny kula
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward; //Sätter kulans riktning åt samma håll som barrel
            audioSource.PlayOneShot(audioClip); //Ljud spelas vid skott
            Destroy(spawnedBullet, 2);
        }


        
    
}
