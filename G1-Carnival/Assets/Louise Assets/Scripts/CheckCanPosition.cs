using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanPosition : MonoBehaviour
{

    public GameObject youWinText;
    private GameObject[] cans;
    public int cansDown;

    private void Start ()
    {
        youWinText.SetActive (false);
        cans = GameObject.FindGameObjectsWithTag ("Can");
        Debug.Log ("Cans: " + cans.Length);
    }

    void Update ()
    {
        Debug.Log (cansDown);
        if ( cansDown == cans.Length )
        {
            youWinText.SetActive (true);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if ( other.CompareTag ("Can") )
        {
            cansDown++;
        }
    }


}