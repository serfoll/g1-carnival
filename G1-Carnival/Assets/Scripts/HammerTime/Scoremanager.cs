using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoremanager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
     public int score;
   
    // Adds 1 Point to Score Canvas
    public void  IncrementScore()
    {
        score++;   
    }
    // Set scoretext to score
    public void FixedUpdate()
    {
        scoreText.text = score.ToString();
    }
}
