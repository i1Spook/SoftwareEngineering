using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortingPlayer : MonoBehaviour
{

    public GameObject ThisPortal;
    public GameObject OtherPortal;

    public uint Speed = 1;

    Vector3 velocityObject = new Vector3();
    Vector2 direction = new Vector2();
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
        if ((Object.gameObject.tag == "Player") || (Object.gameObject.tag == "Portable"))
        {
            StartCoroutine(Teleport(Object));

        }
    }


    IEnumerator Teleport(Collider2D toBePorted)
    {
        if (OtherPortal.GetComponent<BoxCollider2D>().enabled == true)
        {
            OtherPortal.GetComponent<BoxCollider2D>().enabled = !OtherPortal.GetComponent<BoxCollider2D>().enabled;
        }



        Rigidbody2D PortedObject = toBePorted.attachedRigidbody;
        velocityObject = PortedObject.velocity;

        //if (velocityObject.magnitude > Speed)
        //{
        //    GameObject PlayerCopy = Instantiate(toBePorted.gameObject);

        //    PlayerCopy.GetComponent<BoxCollider2D>().enabled = false;
        //    PlayerCopy.GetComponent<Rigidbody2D>().velocity = velocityObject;
        //    Destroy(PlayerCopy, 2);
        //}

        var angle = OtherPortal.transform.eulerAngles.z;

        direction = RotateVelocityVector(angle, velocityObject);


        PortedObject.transform.position = new Vector2(OtherPortal.transform.position.x, OtherPortal.transform.position.y);
        PortedObject.velocity = direction;


        yield return new WaitForSeconds(10/10);

        if (OtherPortal.GetComponent<BoxCollider2D>().enabled == false)
        {
            OtherPortal.GetComponent<BoxCollider2D>().enabled = !OtherPortal.GetComponent<BoxCollider2D>().enabled;
        }
    }
      
    private static Vector3 RotateVelocityVector (float angle, Vector3 velocityObject)
    {
        Vector3 directionVelocity = new Vector3();
        if (angle != 0)
        {
            directionVelocity.x = -(velocityObject.x * Mathf.Cos(angle) - velocityObject.y * Mathf.Sin(angle));
            directionVelocity.y = (velocityObject.x * Mathf.Sin(angle) + velocityObject.y * Mathf.Cos(angle));
        }
        else
        {
            directionVelocity = -velocityObject;
        }
        return directionVelocity;
    }
}