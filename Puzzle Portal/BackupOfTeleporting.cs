//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PortingPlayer : MonoBehaviour
//{

//    public GameObject ThisPortal;
//    public GameObject OtherPortal;

//    Vector3 velocityObject = new Vector3();
//    Vector2 direction = new Vector2();
//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void OnTriggerEnter2D(Collider2D other)
//    {
//        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Portable"))
//        {
//            StartCoroutine(Teleport(other));

//        }
//    }


//    IEnumerator Teleport(Collider2D toBePorted)
//    {
//        if (OtherPortal.GetComponent<BoxCollider2D>().enabled == true)
//        {
//            OtherPortal.GetComponent<BoxCollider2D>().enabled = !OtherPortal.GetComponent<BoxCollider2D>().enabled;
//        }

//        Rigidbody2D PortedObject = toBePorted.attachedRigidbody;
//        velocityObject = PortedObject.velocity;

//        var angle = OtherPortal.transform.eulerAngles.z;

//        if (angle != 0)
//        {
//            direction.x = (velocityObject.x * Mathf.Cos(angle) - velocityObject.y * Mathf.Sin(angle));
//            direction.y = (velocityObject.x * Mathf.Sin(angle) + velocityObject.y * Mathf.Cos(angle));
//        }
//        else
//        {
//            direction = -velocityObject;
//        }


//        PortedObject.transform.position = new Vector2(OtherPortal.transform.position.x, OtherPortal.transform.position.y);
//        PortedObject.velocity = direction;

//        yield return new WaitForSeconds(10 / 10);

//        if (OtherPortal.GetComponent<BoxCollider2D>().enabled == false)
//        {
//            OtherPortal.GetComponent<BoxCollider2D>().enabled = !OtherPortal.GetComponent<BoxCollider2D>().enabled;
//        }
//    }
//}