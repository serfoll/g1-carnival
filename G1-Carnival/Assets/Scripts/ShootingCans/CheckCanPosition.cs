using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanPosition : MonoBehaviour
{

    public GameObject youWinText;
    private GameObject[] cans; //Lista med alla cans
    public int cansDown; 

    private void Start ()
    {
        youWinText.SetActive (false); 
        cans = GameObject.FindGameObjectsWithTag ("Can"); //Lägg till alla GameObject med tag Can i listan cans
        //Debug.Log ("Cans: " + cans.Length);
    }

    void Update ()
    {
        //Debug.Log (cansDown);
        if ( cansDown == cans.Length ) //Om alla cans som är i triggerzonen matchar antal cans i lista visas You Win-Texten
        {
            youWinText.SetActive (true);
        }
    }

    private void OnTriggerEnter (Collider other) //Räknar cans när de kolliderar med triggerZon
    {
        if ( other.CompareTag ("Can") )
        {
            cansDown++; //Öka värdet med 1
            Destroy (other.gameObject , 0.5f);
        }
    }


}