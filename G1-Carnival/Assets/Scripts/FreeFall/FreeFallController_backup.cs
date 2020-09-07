using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FreeFallController_backup : MonoBehaviour
{
    [SerializeField]
    List<FreeFallSeat> freeFallSeats = new List<FreeFallSeat> ();

    private GameObject seatBase;
    private GameObject currentSeat;
    private GameObject player;
    [SerializeField]
    private GameObject seatSupport;
    [SerializeField]
    private float maxYPosition;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip chainClip, impactClip;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBeforeFall = 2f;
    [SerializeField]
    float upSpeed = 10f;
    [SerializeField]
    float fallSpeed = 150f;

    public string seatName { get; set; }

    private GameObject poleHolder, poleHolderLastChild;

    private bool weUp = false;

    public bool slideUp { get; set; } = false;

    public bool guardUp = false;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        audioSource = gameObject.GetComponent<AudioSource> ();
        animator = gameObject.GetComponent<Animator> ();
    }

    private void FixedUpdate ()
    {

        if ( poleHolder == null)
            poleHolder = GameObject.FindGameObjectWithTag ("PoleHolder");
        
        if( poleHolderLastChild == null )
        {
            poleHolderLastChild = poleHolder.transform.GetChild (poleHolder.transform.childCount - 1).gameObject;
            //Debug.Log (poleHolderLastChild);
        }
        if ( maxYPosition <= 0 )
        {
            maxYPosition = poleHolderLastChild.transform.position.y - 2f;
            //Debug.Log (maxYPosition);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if ( slideUp && seatSupport.transform.position.y < maxYPosition )
        {

            slideUp = true;

            StartCoroutine (SlideUP ());
        }

        else if ( !slideUp && weUp )
        {
            StopCoroutine (SlideUP());
            //Debug.Log ("Get Ready");
            if ( audioSource.isPlaying )
            {
                audioSource.Stop ();
            }
            StartCoroutine (TheFall());
            //Debug.Log ("Sit y: " + seatSupport.transform.position.y + "\n Max y: " + maxYPosition);
        }
    }

    IEnumerator SlideUP ()
    {
        guardUp = true;
        animator.SetBool ("guardUp", guardUp);
        yield return new WaitForSeconds (2f);
        player.transform.position = seatBase.transform.position;
        player.transform.rotation = seatBase.transform.rotation;
        player.transform.parent = seatBase.transform;
        yield return new WaitForSeconds (1f);
        guardUp = false;
        animator.SetBool ("guardUp" , guardUp);

        yield return new WaitForSeconds (1f);

        Vector3 _upDestination = new Vector3 (seatSupport.transform.position.x , poleHolderLastChild.transform.position.y , seatSupport.transform.position.z);
        if ( !audioSource.isPlaying )
        {
            audioSource.clip = chainClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds (1f);
        float _upStep = upSpeed * Time.deltaTime;
        seatSupport.transform.position = Vector3.MoveTowards (seatSupport.transform.position ,
            _upDestination , _upStep);

        //Debug.Log (_upDestination);

        if ( Vector3.Distance(seatSupport.transform.position,
            _upDestination ) < 0.001f )
        {
            //Debug.Log ("Seat y: " + seatSupport.transform.position.y + "\n Max y: " + destination*step);

            slideUp = false;
            weUp = true;

            //Debug.Log ("SlideUp: " + slideUp + "\n WeUp: " + weUp);
        }
    }//end Slide up

    //Free fall timer 
    IEnumerator TheFall ()
    {
 
        if ( currentSeat.transform.rotation.x >= 0 || 
            currentSeat.transform.rotation.z >= 0 )
        {
            animator.SetBool ("sitTilt" , true);
            //animator.SetBool ("tiltBackward" , false);
            if ( !audioSource.isPlaying )
                audioSource.PlayOneShot (chainClip);
        }



        float _fallStep = fallSpeed * Time.deltaTime;
        Vector3 _fallDestination = new Vector3 (seatSupport.transform.position.x , 1f , seatSupport.transform.position.z);

        yield return new WaitForSeconds (timeBeforeFall);
        if ( audioSource.isPlaying )
        {
            audioSource.Stop ();
        }

        seatSupport.transform.position = Vector3.MoveTowards (
            seatSupport.transform.position , 
            _fallDestination , _fallStep);
      
        if ( Vector3.Distance(seatSupport.transform.position , _fallDestination) < 20f )
        {
            animator.SetBool ("sitTilt" , false);
            Debug.Log (seatSupport.transform.position);
            weUp = false;
        }
        
        if ( Vector3.Distance (seatSupport.transform.position , _fallDestination) < 0.001f )
        {
            player.transform.parent = null;
        }

        //if ( !weUp )
        //{
        //    _fallDestination = new Vector3 (_fallDestination.x , 0 , _fallDestination.z);
        //    yield return new WaitForSeconds (1f);

        //    seatSupport.transform.position = Vector3.MoveTowards (
        //        seatSupport.transform.position ,
        //        _fallDestination , 2f * Time.deltaTime);

            
        //}
    }

}
