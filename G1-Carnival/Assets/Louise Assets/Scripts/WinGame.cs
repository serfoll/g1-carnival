using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
 
    
    public GameObject YouWinText;
    public GameObject[] cans;
    public int cansLeft; 

   
    void Start()
    {
        cans=GameObject.FindGameObjectsWithTag("Can");
        
        cansLeft = cans.Length;
        
    }

    void Update()
    {
        Debug.Log(cansLeft);
        if(cansLeft<=0)
        {
            YouWinText.SetActive (true);

        }

    }
    
        //  public void Win () 
         
        //  {
        //  if (Can.transform.position.y < Shelf.transform.position.y)
        //  {
        //      YouWinText.SetActive (true);
             
        //  }
        //  }
        
    
}
