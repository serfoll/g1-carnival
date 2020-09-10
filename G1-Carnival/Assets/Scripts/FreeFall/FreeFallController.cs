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

    private GameObject poleHolder;
    private GameObject poleHolderLastChild;
    private bool weUp = false;
    public bool slideUp { get; set; } = false;
    public bool guardUp = false;
    private bool fallen;
    private bool fallEndCoroutine;

    void Start ()
    {
        /*
         * set player to be GameObject with the tag of player
         * set audioSource to be the AudioSource component
         * set animator to be the Animator component
        */
        player = GameObject.FindGameObjectWithTag ("Player");
        audioSource = gameObject.GetComponent<AudioSource> ();
        animator = gameObject.GetComponent<Animator> ();

        if ( animator != null )
        {
            animator.SetBool ("backToIdle" , false);
        }
    }

    void FixedUpdate ()
    {
        //if poleHolder GameObject is null assign it 
        if ( poleHolder == null)
            poleHolder = GameObject.FindGameObjectWithTag ("PoleHolder");

        //if poleHolderLastChild GameObject is null assign it
        if ( poleHolderLastChild == null )
        {
            poleHolderLastChild = poleHolder.transform.GetChild (poleHolder.transform.childCount - 1).gameObject;
        }

        /*
         * if maxYPosition is less then poleHolderLastChild position on the y axis 
         * set it to be the same value as the y position value of poleHolderLastChild - 2f
         */
        if ( maxYPosition <= poleHolderLastChild.transform.position.y )
        {
            maxYPosition = poleHolderLastChild.transform.position.y -2f;
        }

        if ( !slideUp && weUp )
        {
            StopCoroutine (SlideUP ());
            //Debug.Log ("Get Ready");
            if ( audioSource.isPlaying )
            {
                audioSource.Stop ();
            }
            StartCoroutine (TheFall ());
        }

        if ( fallen )
        {
            StopCoroutine (TheFall ());
            StartCoroutine (FallEnd ());
        }
        else
            StopCoroutine (FallEnd ());
    }

    // Update is called once per frame
    void Update ()
    {
        /*
         * if slideUp true and the y position value of seatSupport  is less than maxYposition 
         * start the Coroutine SlideUp()
         */
        if ( slideUp && seatSupport.transform.position.y < maxYPosition)
        {
            StartCoroutine (SlideUP ());
        }
        /*
         * if slideUp is false and weUp is true
         * stop the Coroutine SlideUp()
         * stop the audioSource if it's playing
         * and start the Coroutine TheFall()
         */
    }

    //slideUp IEnumerator
    IEnumerator SlideUP ()
    {
        /*
         * set animator guardUp to true
         * wait for 2f
         * move player to the choosen seatBase position
         * rotate the player to face the same direction as the seatBase
         * set the player be child of the seatBase
         * wait for 1f 
         * set guardUp to false
         * set guardDown to true
        */
        if ( animator.GetBool("backToIdle") )
        {
            animator.SetBool ("backToIdle" , true);
        }

        animator.SetBool ("guardUp" , true);
        yield return new WaitForSeconds (2f);
        player.transform.position = seatBase.transform.position;
        player.transform.rotation = seatBase.transform.rotation;
        player.transform.parent = seatBase.transform;
        yield return new WaitForSeconds (1f);
        animator.SetBool ("guardUp" , false);
        animator.SetBool ("guardDown" , true);
        /*
         * wait for 1f
         * define a varaible of type Vector3 with the name _upDestination
         * set it to be a new vector 3
         * check if the audioSource is not currently playing or not
         * set stop to the audioSource
         * assign the audioSource clip to be chainClip
         * set audioSource to play
        */
        yield return new WaitForSeconds (1f);
        Vector3 _upDestination = new Vector3 (seatSupport.transform.position.x , 
            maxYPosition , 
            seatSupport.transform.position.z);
        if ( !audioSource.isPlaying || audioSource.isPlaying)
        {
            audioSource.Stop ();
            audioSource.clip = chainClip;
            audioSource.Play ();
        }

        /*
         * wait for 1f
         * define a new float variable _upStep
         * set it to be upSpeed multiplied by Time.deltaTime
         * move the seatSupport towards _upDestination with a maxDeltaDistance of _upStep
         * if the distance between seatSupport and _upDestination is less than 0.001f
         * set slideUp to false and weUp to true
        */
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
    }//end slideUp


    //TheFall IEnumerator
    IEnumerator TheFall ()
    {
        /*
         * if the currentSeat's rotation on the x or z is greater than 0
         * setBool of the animator sitTilt to true
         * if audioSource is no playing PlayeOneShot of chainClip
        */

        if ( currentSeat.transform.rotation.x >= 0 ||
            currentSeat.transform.rotation.z >= 0 )
        {
            animator.SetBool ("sitTilt" , true);
            if ( !audioSource.isPlaying )
                audioSource.PlayOneShot (chainClip);
        }

        /*
         * define new float variable _fallStep set it to be fallSpeed multiplied with Time.delaTime
         * define new vector3 variable set to be a new vector 
         * wait for timeBeforFall 
         * stop audioSource if it is playing
        */
        float _fallStep = fallSpeed * Time.deltaTime;
        Vector3 _fallDestination = new Vector3 (seatSupport.transform.position.x , 0f , seatSupport.transform.position.z);
        yield return new WaitForSeconds (timeBeforeFall);
        if ( audioSource.isPlaying )
        {
            audioSource.Stop ();
        }
        //Move seatSupport towards _fallDestination with a maxDistanceDelta of _fallStep
        seatSupport.transform.position = Vector3.MoveTowards (
            seatSupport.transform.position ,
            _fallDestination , _fallStep);

        /*
         * define a new a GameObject _playerReturnArea 
         * wait for 0.2f
         * if the distance between seatSupport and _fallDestinaion is less than 0.001f
         * set the animator's bool sitTilt to false
         * set the animator's bool sitBack to true
        */
        if ( Vector3.Distance (seatSupport.transform.position , _fallDestination) < 0.001f )
        {
            fallen = true;
        }

    }//End TheFall

    IEnumerator FallEnd ()
    {
        GameObject _playerReturnArea = GameObject.FindGameObjectWithTag ("PlayerReturnArea");
        animator.SetBool ("sitTilt" , false);
        yield return new WaitForSeconds (0.2f);
        animator.SetBool ("sitBack" , true);

        //wait for 1.5f
        yield return new WaitForSeconds (1.5f);

        /*
         * set player position to _playerReturnArea position
         * set player rotation to _playerReturnArea rotation
        */

        player.transform.position = _playerReturnArea.transform.position;
        player.transform.rotation = _playerReturnArea.transform.rotation;

        // wait 0.2f
        yield return new WaitForSeconds (0.3f);

        /*
         * set seat base to null
         * set currentSeat to null
         * set players parent to be null
         * set weUp to false
         * set all animator bools to false except backToIdle to true
        */
        seatBase = null;
        currentSeat = null;
        player.transform.parent = null;
        weUp = false;

        animator.SetBool ("sitBack" , false);
        animator.SetBool ("guardUp" , false);
        animator.SetBool ("guardDown" , false);
        animator.SetBool ("sitTilt" , false);
        animator.SetBool ("backToIdle" , true);

        fallen = false;
    }//End FallEnd()


    //Get currentSeat and seatBase GameObjects by looping through the freeFallSeats List
    /*
     * if seatBase is null 
     * loop through freeFallSeats til seat is found
     * if seatBase is not null 
     * set slideUp to true
    */
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
        }
    }

}
