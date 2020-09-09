using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{

    public GameObject YouWinText;
    public GameObject[] cans;
    public GameObject shelf;
    public int cansLeft;


    void Start ()
    {
        cans = GameObject.FindGameObjectsWithTag ("Can");
        cansLeft = cans.Length;
    }

    void Update ()
    {
    }


}
