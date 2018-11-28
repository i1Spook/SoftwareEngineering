//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BluePortal : MonoBehaviour
//{

//    public GameObject ThisPortal;
//    public GameObject OtherPortal;

//    public float velo;

//    public bool disableThisPortal = false;

//    float Cooldown = 0f;

//    Vector2 OtherPortalUpVector;

//    // Use this for initialization
//    void Start()
//    {
//        PortingPlayer Blue = new PortingPlayer();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//    }

//    public void OnTriggerEnter2D(Collider2D Object)
//    {
//        //Check if Object is allowed to teleport
//        if ((Object.gameObject.tag == "Player") || (Object.gameObject.tag == "Portable"))
//        {
//            StartCoroutine(Teleport(Object));
//        }
//    }


//    IEnumerator Teleport(Collider2D toBePorted)
//    {
//        velo = toBePorted.attachedRigidbody.velocity.magnitude;

//        if (velo > 30)
//        {
//           toBePorted.attachedRigidbody.AddForce(-toBePorted.attachedRigidbody.velocity);
//        }

        
//            OtherPortal.GetComponent<BoxCollider2D>().enabled = false;

//            OtherPortalUpVector = OtherPortal.transform.up;

//            toBePorted.transform.position = new Vector2(OtherPortal.transform.position.x, OtherPortal.transform.position.y);

//            toBePorted.attachedRigidbody.velocity = OtherPortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;
        

//        //OtherPortal.GetComponent<BoxCollider2D>().enabled = false;

//        //OtherPortalUpVector = OtherPortal.transform.up;

//        //toBePorted.transform.position = new Vector2(OtherPortal.transform.position.x, OtherPortal.transform.position.y);

//        //toBePorted.attachedRigidbody.velocity = OtherPortalUpVector * toBePorted.attachedRigidbody.velocity.magnitude;

//        yield return new WaitForSeconds(Cooldown);

//        //OtherPortal.GetComponent<BoxCollider2D>().enabled = true;
//    }

//    public  void OnTriggerExit2D ()
//    {
//        ThisPortal.GetComponent<BoxCollider2D>().enabled = true;
//    }
//}