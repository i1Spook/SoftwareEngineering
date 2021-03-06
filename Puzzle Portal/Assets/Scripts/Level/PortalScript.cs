﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
  public GameObject BluePortal;
  public GameObject OrangePortal;

  public float velo;

  public bool disableThisPortal;
  public bool PortalCreated;

  Vector2 OtherPortalUpVector;

  GameObject OtherPortal;

  // Use this for initialization
  void Start()
  {
    disableThisPortal = false;

    PortalCreated = false;

    //Sets the reference to the other portal, important because it will be turned off
    OtherPortal = tag.ToUpper() == "BLUEPORTAL" ? OrangePortal : BluePortal;
  }

  public void OnTriggerEnter2D(Collider2D Object)
  {
    //Check if Object is allowed to be teleported
    if ((Object.gameObject.tag == "Player") || (Object.gameObject.tag == "Portable") || (Object.gameObject.tag == "Item"))
    {
      Teleport(Object.gameObject);
    }
  }


  void Teleport(GameObject toBePorted)
  {
    //Checks if this portal is enabled and both portals are made by the player
    if (!disableThisPortal && ItemScript.AllPortalsCreated)
    {
      //Disable the Orange Portal
      OtherPortal.GetComponent<PortalScript>().disableThisPortal = true;

      //Get the UpVector of the OrangePortal
      OtherPortalUpVector = OtherPortal.transform.up;

      Vector2 Position = new Vector2(OtherPortal.transform.GetChild(0).transform.position.x, OtherPortal.transform.GetChild(0).transform.position.y);

      //Teleport Object to position of otherPortal
      toBePorted.transform.position = Position;

      FindObjectOfType<AudioManager>().PlayAt("PortalTraversal");

      //Gives the Object it's original velocity in the Updirection of the OtherPortal
      toBePorted.GetComponent<Rigidbody2D>().velocity = OtherPortalUpVector * toBePorted.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    //Enforce lower terminal Velocity
    if (toBePorted.GetComponent<Rigidbody2D>().velocity.magnitude > 30)
    {
      toBePorted.GetComponent<Rigidbody2D>().AddForce(-toBePorted.GetComponent<Rigidbody2D>().velocity);
    }
  }

  public void OnTriggerExit2D(Collider2D CollisionObject)
  {
    //Reset this Portal
    disableThisPortal = false;
  }
}