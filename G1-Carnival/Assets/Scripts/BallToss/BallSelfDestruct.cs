using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelfDestruct : MonoBehaviour
{
    // Destroys BallPrefab after 15s
    void Start()
    {
        Destroy(gameObject, 15f);
    }
}
