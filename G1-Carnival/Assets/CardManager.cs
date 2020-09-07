using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public bool hovered { get; set; } = false;
    public bool flipped { get; set; } = false; 

    private GameObject currentCard;

    Vector3 defauldPosition = new Vector3 ();
    Vector3 defauldScale = new Vector3 ();

    private Animator animator;
    private int cardFlipped = 0;

    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start ()
    {
        animator = gameObject.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {

        if ( flipped )
        {
            if ( cardFlipped < 1 )
            {
                StartCoroutine (FlipCard ());
                cardFlipped++;
                Debug.Log(cardFlipped);
            }
        }

        if ( !hovered && currentCard != null)
        {
            currentCard.transform.localScale = defauldScale;
            currentCard.transform.position = defauldPosition;
        }


    }

    public void  CardAnimationController (string _currentCardName)
    {

        if ( _currentCardName != null )
        {
            currentCard = GameObject.FindGameObjectWithTag (_currentCardName);
            defauldPosition = currentCard.transform.position;
            defauldScale = currentCard.transform.localScale;
        }

        if ( hovered )
        {
            currentCard.transform.localScale = new Vector3 (1.5f , 1.5f , 1.5f);
        }

    }

    IEnumerator FlipCard ()
    {
        if ( currentCard != null)
        {
            float tiltAroundZ = Input.GetAxis ("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis ("Vertical") * tiltAngle;
            //animator.SetBool ("cardFaceUp" , true);
            //currentCard.transform.Rotate (180 , currentCard.transform.rotation.y , currentCard.transform.rotation.z);
            Quaternion target = Quaternion.Euler (tiltAroundX , 0 , tiltAroundZ);
            currentCard.transform.rotation = Quaternion.Slerp (currentCard.transform.rotation , target , Time.deltaTime * smooth);
        }
        yield return new WaitForSeconds (2f);
        //animator.SetBool ("cardFaceUp" , false);
        //animator.SetBool ("cardFaceDown" , true);
        yield return new WaitForSeconds (2f);
        //animator.SetBool ("cardFaceUp" , false);
        //animator.SetBool ("cardFaceDown" , false);
        cardFlipped = 0;
        flipped = false;
        Debug.Log(cardFlipped);
    }

}
