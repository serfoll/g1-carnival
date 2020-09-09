using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public bool flipped { get; set; } = false; 

    private GameObject currentCard;
    private GameObject previousCard;


    private Animator currentCardAnimator;
    private Animator previousCardAnimator;
    
    private int cardFlipped = 0;
    private int cardsCheck = 0;

    private void FixedUpdate ()
    {

        if ( flipped )
        {
            FlipCardFaceUp ();
        }

        if ( currentCard != null && previousCard != null && cardFlipped == 2)
        {
            CheckMatchingCard ();
        }
        else
        {
            Debug.Log ("FaceDown");
            StopCoroutine (CardsFaceDown ());
        }
    }


    //Get the current card(card picked by the player ) 
    //if there's already a card the set it to the previous card. 
    public void  GetCurrenCard (string _currentCardName)
    {

        if ( _currentCardName != null && currentCard == null)
        {
            currentCard = GameObject.Find(_currentCardName);
            currentCardAnimator = currentCard.GetComponent<Animator>();
            //Debug.Log (currentCard.name);
        }

        else if ( _currentCardName != null && _currentCardName != currentCard.name && currentCard != null )
        {
            previousCard = currentCard;
            previousCardAnimator = previousCard.GetComponent<Animator> ();
            currentCard = GameObject.Find (_currentCardName);
            currentCardAnimator = currentCard.GetComponent<Animator> ();
            //Debug.LogWarning ("PrevCard: " + previousCard.name +
            //    "\n CurrCardd: " + currentCard.name);
        }

    }

    
    //If the player has picked two cards check to see if the cards match by using their tags
    void CheckMatchingCard ()
    {
        //If the cards don't match turn them face down
        if ( currentCard.tag != previousCard.tag)
        {
            Debug.LogWarning ("Cards don't match");
            StartCoroutine (CardsFaceDown ());
        }
    }

    void FlipCardFaceUp ()
    {
        if ( currentCardAnimator != null && !currentCardAnimator.GetBool ("cardFaceUp") )
        {
            if ( cardFlipped < 2 )
            {
                currentCardAnimator.SetBool ("cardFaceUp" , true);
                cardFlipped++;
            }
            Debug.Log ("Cards flipped: " + cardFlipped);
        }
        
    }

   
    IEnumerator CardsFaceDown ()
    {
        yield return new WaitForSeconds (2f);

        currentCardAnimator.SetBool ("cardFaceUp" , false);
        previousCardAnimator.SetBool ("cardFaceUp" , false);
        currentCardAnimator.SetBool ("cardFaceDown" , true);
        previousCardAnimator.SetBool ("cardFaceDown" , true);
        yield return new WaitForSeconds (1f);

        cardFlipped = 0;
        currentCardAnimator.SetBool ("cardFaceDown" , false);
        previousCardAnimator.SetBool ("cardFaceDown" , false);

        currentCard = null;
        previousCard = null;

        Debug.Log ("Cards Flipped: " + currentCard);
        Debug.Log ("Cards check: " + previousCard);
    }


}
