using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHammer : MonoBehaviour
{
    // Destroy Gameobject with Hammer Tag when enter triggerzone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer") == true)
        {
            
            Destroy(other.gameObject);

        }
    }
}
