using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    Vector3 defauldScale = new Vector3 ();

    public bool hovered { get; set; } = false;

    private void Start ()
    {
        defauldScale = transform.localScale;
    }

    private void Update ()
    {
        if ( hovered )
        {
            transform.localScale = new Vector3 (1.5f , 1.5f , 1.5f);
        }
        else
        {
            transform.localScale = defauldScale;
        }
    }

}
