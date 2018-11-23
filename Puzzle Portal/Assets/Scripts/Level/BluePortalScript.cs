using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalScript : MonoBehaviour
{

    public GameObject BluePortal;
    public GameObject OrangePortal;

    public float velo;

    public static bool disableThisPortal = false;

    float Cooldown = 0f;

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

        

        if (!disableThisPortal)
        {
            //Disable the Orange Portal
            OrangePortalScript.disableThisPortal = true;

            //Turn Off OtherPortals BoxCollider (Time is "Cooldown")
            //OrangePortal.GetComponent<BoxCollider2D>().enabled = false;

            //Get the UpVector of the OtherPortal
            OtherPortalUpVector = OrangePortal.transform.up;

            //Teleport Object to position of otherPortal
            toBePorted.transform.position = new Vector2(OrangePortal.transform.position.x, OrangePortal.transform.position.y);

            //Gives the Object it's original velocity in the Updirection of the OtherPortal
            toBePorted.attachedRigidbody.velocity = OtherPortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;
        }
        //Cooldown for the OtherPortal BoxCollider to turn back on
        yield return new WaitForSeconds(Cooldown);

        if (toBePorted.attachedRigidbody.velocity.magnitude > 30)
        {
            toBePorted.attachedRigidbody.AddForce(-toBePorted.attachedRigidbody.velocity*2);
        }

        //Turn On OtherPortals BoxCollider
        //OrangePortal.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnTriggerExit2D(Collider2D CollisionObject)
    {
        disableThisPortal = false;
    }
}