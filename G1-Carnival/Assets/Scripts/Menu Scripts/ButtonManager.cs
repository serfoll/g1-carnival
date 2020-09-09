using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Loads new scene when button on UI is pressed.
    public void ButtonMoveScene(string level)
    {
        SceneManager.LoadScene(level);
    }

}
