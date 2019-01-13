using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputHandler : MonoBehaviour
{
  public GameObject BlueOriginal;
  public GameObject OrangeOriginal;
  public GameObject ArmLocation;

  float timerBlue;
  float timerOrange;

  public static bool BlueFired;
  public static bool OrangeFired;
  public static bool ItemWasShot;

  public float projectileForce ;

  // Use this for initialization
  void Start()
  {
    BlueFired = false;

    OrangeFired = false;

    projectileForce = 500;
  }

  // Update is called once per frame
  void Update()
  {
    ItemWasShot = false;

    //Always check if a button is pressed
    bool ItemButtonPressed = Input.GetKeyDown(KeyCode.E);
    bool LeftMouseButtonPressed = Input.GetMouseButtonDown(0);
    bool RightMouseButtonPressed = Input.GetMouseButtonDown(1);

    //Calls the methods that enables interaction with items and such
    ItemScript.ItemInteraction(ItemButtonPressed, LeftMouseButtonPressed || RightMouseButtonPressed);

    if (!ItemWasShot)
    {
      ShootPortal(LeftMouseButtonPressed, RightMouseButtonPressed);
    }
  }

  void ShootPortal(bool ShotFiredBlue, bool ShotFiredOrange)
  {
    float Lifespan = 5;

    if (ItemScript.PortalGunFound)
    {
      //Checks if a shot was fires, no item is held and the shot is enabled yet
      if (!ItemScript.ItemInHandToggle && ShotFiredBlue && (!BlueFired || timerBlue > Lifespan))
      {
        FindObjectOfType<AudioManager>().PlayAt("BlueShot");

        BlueFired = true;

        //Cloning the Drop
        GameObject BlueShot = Instantiate(BlueOriginal);

        //Setting the position of the Drop
        BlueShot.GetComponent<Rigidbody2D>().transform.position = ArmLocation.transform.position;
        BlueShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        //Shoots the drop
        FireObject(BlueShot, projectileForce);

        //Sets the timer to 0 and therefore disables the blue shot until it either hits a wall or the lifespan is over
        timerBlue = 0;
      }

      if (!ItemScript.ItemInHandToggle && ShotFiredOrange && (!OrangeFired || timerOrange > Lifespan))
      {
        //Exactly the same procedure as above just for the orange portal
        FindObjectOfType<AudioManager>().PlayAt("OrangeShot");

        OrangeFired = true;

        GameObject OrangeShot = Instantiate(OrangeOriginal);

        OrangeShot.GetComponent<Rigidbody2D>().transform.position = ArmLocation.transform.position;
        OrangeShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        FireObject(OrangeShot, projectileForce);

        timerOrange = 0;
      }

      timerBlue += Time.deltaTime;
      timerOrange += Time.deltaTime;
    }
  }

  public static void FireObject(GameObject Object, float projectileForce)
  {
    Vector2 Pointer;

    //Gets the direction the player is aiming at
    Pointer.x = -AimAtMouse.MousePositionRead.y;
    Pointer.y = AimAtMouse.MousePositionRead.x;

    //Sets the direction as the objects direction and adds a force in that direction
    Object.transform.right = Pointer;
    Object.GetComponent<Rigidbody2D>().AddForce(AimAtMouse.MousePositionRead.normalized * projectileForce);
  }

  public static void ResetShot(bool BlueHit, bool OrangeHit)
  { //Enables the shots whenever it is called
    if (BlueHit)
    {
      BlueFired = false;
    }

    if (OrangeHit)
    {
      OrangeFired = false;
    }
  }
}
