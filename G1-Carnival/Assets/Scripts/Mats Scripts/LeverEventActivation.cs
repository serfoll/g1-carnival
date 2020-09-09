using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverEventActivation : MonoBehaviour
{
    //Angle limit to trigger if we reached limit
    public float hingeLimit = 1f;   
    //Event called on max reached
    public UnityEvent MaxLimitReached;

    private HingeJoint hinge;

    private bool activeMethod = false;


    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
        float maxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);        
        
        //Reached Max
        if (maxLimit < hingeLimit)
        {
            // To avoid spawning a mountain of balls.
            if (activeMethod != true)
            {
                MaxLimitReached.Invoke();
                activeMethod = true;
            }            
        }
        else
        {
            activeMethod = false;
        }
    }
}