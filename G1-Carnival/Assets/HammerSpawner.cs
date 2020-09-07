using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSpawner : MonoBehaviour
{
    public GameObject hammerPrefab;
    public GameObject hammerInstantPosition;
    // Start is called before the first frame update


    public void SpawnHammer ()
    {

        GameObject.Instantiate (hammerPrefab , 
            hammerInstantPosition.transform.position , 
            Quaternion.identity);
    }
}
