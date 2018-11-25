using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortingPlayer : MonoBehaviour
{

    public GameObject ThisPortal;
    public GameObject OtherPortal;

    float Cooldown = 1f;

    Vector2 OtherPortalUpVector;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D Object)
    {
        //Check if Object is allowed to teleport
        if ((Object.gameObject.tag == "Player") || (Object.gameObject.tag == "Portable"))
        {
            StartCoroutine(Teleport(Object));
        }
    }


    IEnumerator Teleport(Collider2D toBePorted)
    {
        //Turn Off OtherPortals BoxCollider (Time is "Cooldown")
        OtherPortal.GetComponent<BoxCollider2D>().enabled = false;

        //Get the UpVector of the OtherPortal
        OtherPortalUpVector = OtherPortal.transform.up;

        //Teleport Object to position of otherPortal
        toBePorted.transform.position = new Vector2(OtherPortal.transform.position.x, OtherPortal.transform.position.y);

        //Gives the Object it's original velocity in the Updirection of the OtherPortal
        toBePorted.attachedRigidbody.velocity = OtherPortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;

        //Cooldown for the OtherPortal BoxCollider to turn back on
        yield return new WaitForSeconds(Cooldown);

        //Turn On OtherPortals BoxCollider
        OtherPortal.GetComponent<BoxCollider2D>().enabled = true;
    }
}