using System.Collections;
using UnityEngine;

public class FreeFallController : MonoBehaviour
{
    [SerializeField]
    private GameObject sitBase;
    private GameObject player;
    [SerializeField]
    private GameObject sit;
    [SerializeField]
    private float maxYPosition;
    [SerializeField]
    private float minYPosition;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip chainClip, impactClip;
    [SerializeField]
    private Animator animator;

    private bool clipIsPlaying = false;

    private float timeBeforeFall = 5f;
    public bool slideUp { get; set; } = false;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        audioSource = gameObject.GetComponent<AudioSource> ();
        animator = gameObject.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {

        if ( slideUp && sit.transform.position.y < maxYPosition )
        {
            player.transform.position = sitBase.transform.position;
            player.transform.rotation = sitBase.transform.rotation;
            player.transform.parent = sitBase.transform;
            StartCoroutine (SlideUP ());
        }
        else
        {
            StopCoroutine (SlideUP());
        }


        if ( sit.transform.position.y >= maxYPosition )

        {
            //sit.transform.position = new Vector3 ();
            slideUp = false;
            if ( audioSource.isPlaying )
            {
                audioSource.Stop ();
            }
            StartCoroutine (TheFall());
        }
    }

    IEnumerator SlideUP ()
    {
        Vector3 destination = new Vector3 (sit.transform.position.x , maxYPosition , sit.transform.position.z);
        if ( !audioSource.isPlaying )
        {
            audioSource.clip = chainClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds (2f);
        sit.transform.position = Vector3.MoveTowards (sit.transform.position , destination , 0.2f);
    }

    //Free fall timer 
    IEnumerator TheFall ()
    {
        //Vector3 fallEase = new Vector3 (sit.transform.position.x , maxYPosition-50 , sit.transform.position.z);
        yield return new WaitForSeconds (timeBeforeFall-4);
        //rotate angle 

        if ( sitBase.transform.rotation.x >= 0 )
        {
            animator.SetBool ("tiltForward" , true);
            animator.SetBool ("tiltBackward" , false);
            if ( !audioSource.isPlaying )
                audioSource.PlayOneShot (chainClip);
            if ( audioSource.isPlaying )
                audioSource.Stop ();

        }
        //yield return new WaitForSeconds (timeBeforeFall-2);

        Debug.Log ("fall");
        //sit.transform.position = Vector3.MoveTowards (sit.transform.position , fallEase , 0.8f);
        
        Vector3 fallDestination = new Vector3 (sit.transform.position.x , 0 , sit.transform.position.z);

        yield return new WaitForSeconds (timeBeforeFall-2);
        sit.transform.position = Vector3.MoveTowards (sit.transform.position , fallDestination , 1f);
        if ( fallDestination.y%2 < sit.transform.position.y)
        {
            if ( !audioSource.isPlaying )
            {
                audioSource.PlayOneShot (impactClip);
            }
        }

        //yield return new WaitForSeconds (0.5f);

        //if ( sitBase.transform.rotation.x > 1 )
        //{
        //    animator.SetBool ("tiltForward" , false);
        //    animator.SetBool ("tiltBackward" , true);
        //    if ( !audioSource.isPlaying )
        //        audioSource.PlayOneShot (chainClip);
        //    if ( audioSource.isPlaying )
        //        audioSource.Stop ();
        //}

    }


}
