using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitHandControl : MonoBehaviour
{
    // Quits the game
    public void QuitTheGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
