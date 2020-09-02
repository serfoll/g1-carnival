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

    float timeBeforeFall = 5f;
    public bool slideUp { get; set; } = false;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if ( slideUp && sit.transform.position.y < maxYPosition )
        {



            player.transform.position = sitBase.transform.position;
            player.transform.rotation = sitBase.transform.rotation;
            player.transform.parent = sitBase.transform;

            Vector3 destination = new Vector3 (sit.transform.position.x, maxYPosition, sit.transform.position.z);

            sit.transform.position = Vector3.MoveTowards (sit.transform.position, destination , 0.2f );

        }


        if ( sit.transform.position.y >= maxYPosition )

        {
            //sit.transform.position = new Vector3 ();
            slideUp = false;
            StartCoroutine (TheFall());
        }
    }

    IEnumerator TheFall ()
    {
        Vector3 fallEase = new Vector3 (sit.transform.position.x , maxYPosition-50 , sit.transform.position.z);
        yield return new WaitForSeconds (timeBeforeFall);
        Debug.Log ("fall");
        sit.transform.position = Vector3.MoveTowards (sit.transform.position , fallEase , 1f);
        yield return new WaitForSeconds (timeBeforeFall);
        Vector3 fallDestination = new Vector3 (sit.transform.position.x , 0 , sit.transform.position.z);
        sit.transform.position = Vector3.MoveTowards (sit.transform.position , fallDestination , 2f);
    }


}
