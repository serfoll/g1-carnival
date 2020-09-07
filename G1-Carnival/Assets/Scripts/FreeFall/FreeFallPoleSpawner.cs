using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallPoleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject polePrefab;
    [SerializeField]
    private float poleBase;
    [SerializeField]
    private int nrOfPolesToSpawn;
    [SerializeField]
    private float startPosition;

    [SerializeField]
    private Vector3 spawnOrigin;
    private Vector3 spawnPosition;
    private GameObject poleHolder;
    


    private void Start ()
    {
        poleHolder = new GameObject ();
        poleHolder.name = "PoleHolder";
        poleHolder.tag = "PoleHolder";

        for ( int i = 0; i < nrOfPolesToSpawn; i++ )
        {
            if ( i == 0 )
                spawnPosition = new Vector2 (0,startPosition);
            else
                spawnPosition = spawnPosition + new Vector3 (0 , spawnOrigin.y, 0);

            GameObject _polePrefab = GameObject.Instantiate (polePrefab ,
            spawnOrigin + spawnPosition, Quaternion.identity);

            _polePrefab.name = "Pole_" + spawnPosition.y;
            _polePrefab.transform.parent = poleHolder.transform;
        }

    }


}
