using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanPosition : MonoBehaviour
{
    public GameObject Shelf;
    public WinGame winGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<Shelf.transform.position.y)
        {
            Debug.Log(gameObject.name);
            winGame.cansLeft--;

        }
        
    }
}
