using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallController : MonoBehaviour
{
    [SerializeField]
    private GameObject seatSupport;
    //define a list of the free fall seats4
    [SerializeField]
    List<FreeFallSeat> freeFallSeats = new List<FreeFallSeat> ();

    private GameObject seatBase;
    private GameObject currentSeat;
    private GameObject player;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip chainClip, impactClip;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float maxYPosition;
    [SerializeField]
    private float timeBeforeFall = 2f;
    [SerializeField]
    private float upSpeed = 10f;
    [SerializeField]
    private float fallSpeed = 150f;

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

        if ( guardUp || !guardUp)
        {
            animator.SetBool ("guardUp" , guardUp);

        }

    }

    // Update is called once per frame
    void Update ()
    {

        if ( slideUp && seatSupport.transform.position.y < maxYPosition)
        {
            StartCoroutine (SlideUP ());
        }

        else if ( !slideUp && weUp )
        {
            StopCoroutine (SlideUP ());
            //Debug.Log ("Get Ready");
            if ( audioSource.isPlaying )
            {
                audioSource.Stop ();
            }
            StartCoroutine (TheFall ());
        }
    }

    IEnumerator SlideUP ()
    {
        guardUp = true;
        yield return new WaitForSeconds (2f);
        player.transform.position = seatBase.transform.position;
        player.transform.rotation = seatBase.transform.rotation;
        player.transform.parent = seatBase.transform;
        yield return new WaitForSeconds (1f);
        guardUp = false;

        yield return new WaitForSeconds (1f);

        Vector3 _upDestination = new Vector3 (seatSupport.transform.position.x , poleHolderLastChild.transform.position.y , seatSupport.transform.position.z);
        if ( !audioSource.isPlaying )
        {
            audioSource.clip = chainClip;
            audioSource.Play ();
        }

        yield return new WaitForSeconds (1f);
        float _upStep = upSpeed * Time.deltaTime;
        seatSupport.transform.position = Vector3.MoveTowards (seatSupport.transform.position ,
            _upDestination , _upStep);

        if ( Vector3.Distance (seatSupport.transform.position ,
            _upDestination) < 0.001f )
        {

            slideUp = false;
            weUp = true;

        }
    }//end Slide up


    //Free fall timer 
    IEnumerator TheFall ()
    {

        //Tilt seat forward
        if ( currentSeat.transform.rotation.x >= 0 ||
            currentSeat.transform.rotation.z >= 0 )
        {
            animator.SetBool ("sitTilt" , true);
            if ( !audioSource.isPlaying )
                audioSource.PlayOneShot (chainClip);
        }

        //Start the falling process
        float _fallStep = fallSpeed * Time.deltaTime;
        Vector3 _fallDestination = new Vector3 (seatSupport.transform.position.x , 0.2f , seatSupport.transform.position.z);

        yield return new WaitForSeconds (timeBeforeFall);
        if ( audioSource.isPlaying )
        {
            audioSource.Stop ();
        }

        seatSupport.transform.position = Vector3.MoveTowards (
            seatSupport.transform.position ,
            _fallDestination , _fallStep);

        //Tilt seat backward
        if ( Vector3.Distance (seatSupport.transform.position , _fallDestination) < 20f )
        {

            animator.SetBool ("sitTilt" , false);
            yield return new WaitForSeconds (2f);

        }

        if ( Vector3.Distance (seatSupport.transform.position , _fallDestination) < 0.001f )
        {
            guardUp = true;
            GameObject _playerReturnArea = GameObject.FindGameObjectWithTag ("PlayerReturnArea");
           
            yield return new WaitForSeconds (1.5f);

            //reset player position
            if ( seatBase != null )
            {
                player.transform.position = _playerReturnArea.transform.position;
                player.transform.rotation = _playerReturnArea.transform.rotation;
            }
            
            //Reset the ride
            guardUp = false;
            seatBase = null;
            currentSeat = null;
            player.transform.parent = null;
            weUp = false;

        }

    }//End the fall


    //Get currentSeat and seatBase GameObjects by looping through the freeFallSeats List
    public void GetSeat (string _seatName)
    {
        if ( seatBase == null )
        {
            for ( int i = 0; i < freeFallSeats.Count; i++ )
            {
                if ( freeFallSeats[ i ].seatName == _seatName )
                {
                    Debug.Log (_seatName);
                    seatBase = freeFallSeats[ i ].seatBase;
                    currentSeat = freeFallSeats[ i ].seat;
                    break;//Stop loop after seat is found
                }
            }
        }

        if ( seatBase != null )
        {
            slideUp = true;
            Debug.Log (seatBase.name + "\n " + currentSeat.name);

        }
        //seatBase.transform.parent.gameObject.SetActive (false);
    }

}
