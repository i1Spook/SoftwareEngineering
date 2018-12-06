using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePortalScript : MonoBehaviour
{

    public GameObject OrangePortal;
    public GameObject BluePortal;

    public float velo;

    public static bool disableThisPortal;

    Vector2 OtherPortalUpVector;

    // Use this for initialization
    void Start()
    {
        disableThisPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D Object)
    {
        //Check if Object is allowed to teleport
        if ((Object.gameObject.tag == "Player") || (Object.gameObject.tag == "Portable") || (Object.gameObject.tag == "Item"))
        {
            Teleport(Object);
        }
    }


    void Teleport(Collider2D toBePorted)
    {
        velo = toBePorted.attachedRigidbody.velocity.magnitude;



        if (!disableThisPortal)
        {
            //Disable the Blue Portal
            BluePortalScript.disableThisPortal = true;

            //Get the UpVector of the OtherPortal
            OtherPortalUpVector = BluePortal.transform.up;

            //Teleport Object to position of otherPortal
            toBePorted.transform.position = new Vector2(BluePortal.transform.position.x, BluePortal.transform.position.y);

            //Gives the Object it's original velocity in the Updirection of the OtherPortal
            toBePorted.attachedRigidbody.velocity = OtherPortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;
        }

        //Cooldown for the OtherPortal BoxCollider to turn back on

        if (toBePorted.attachedRigidbody.velocity.magnitude > 30)
        {
            toBePorted.attachedRigidbody.AddForce(-toBePorted.attachedRigidbody.velocity);
        }
        //Turn On OtherPortals BoxCollider
        //BluePortal.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnTriggerExit2D(Collider2D CollisionObject)
    {
        disableThisPortal = false;
    }
}