using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalScript : MonoBehaviour
{

    public GameObject BluePortal;

    public GameObject OrangePortal;

    public float velo;

    public static bool disableThisPortal;

    public static bool PortalCreated;

    Vector2 OrangePortalUpVector;

    // Use this for initialization
    void Start()
    {
        disableThisPortal = false;

        PortalCreated = false;
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
        if (!disableThisPortal && ItemScript.AllPortalsCreated)
        {
            //Disable the Orange Portal
            OrangePortalScript.disableThisPortal = true;

            //Get the UpVector of the OrangePortal
            OrangePortalUpVector = OrangePortal.transform.up;

            Vector2 Position = new Vector2(OrangePortal.transform.GetChild(0).transform.position.x, OrangePortal.transform.GetChild(0).transform.position.y);

            //Teleport Object to position of otherPortal
            toBePorted.transform.position = Position;

            //Gives the Object it's original velocity in the Updirection of the OtherPortal
            toBePorted.attachedRigidbody.velocity = OrangePortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;
        }

        //Enforce lower terminal Velocity
        if (toBePorted.attachedRigidbody.velocity.magnitude > 30)
        {
            toBePorted.attachedRigidbody.AddForce(-toBePorted.attachedRigidbody.velocity);
        }
    }

    public void OnTriggerExit2D(Collider2D CollisionObject)
    {
        //Reset this Portal
        disableThisPortal = false;
    }
}