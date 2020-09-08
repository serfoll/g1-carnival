using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTossPoints : MonoBehaviour
{
    public int ballPoints;
    public Text ballScore;

    public AudioClip scorePoint;
    public AudioSource audioSource;

    private void Update()
    {
        ballScore.text = ballPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            ballPoints += 1;
            audioSource.PlayOneShot(scorePoint);
        }
    }
}
