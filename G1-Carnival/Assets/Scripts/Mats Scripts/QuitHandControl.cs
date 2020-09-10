using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitHandControl : MonoBehaviour
{
    // Quits the game
    public void QuitTheGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
