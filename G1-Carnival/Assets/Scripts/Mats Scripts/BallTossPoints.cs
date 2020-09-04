using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTossPoints : MonoBehaviour
{
    public int ballPoints;
    public Text ballScore;

    private void Update()
    {
        ballScore.text = ballPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            ballPoints += 10;
        }
    }
}
