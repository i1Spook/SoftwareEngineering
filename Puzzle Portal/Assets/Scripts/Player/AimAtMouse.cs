using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
  Animator playerSpriteAnimator;

  public static Vector3 MousePositionRead;

  static Vector3 PlayerPosition;

  Vector3 Rotation;

  float angle;

  bool FlippedArm = false;

  // Use this for initialization
  void Start()
  {
    playerSpriteAnimator = GetComponentInParent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    //Gets Mouse Position in Screen Coordinates
    MousePositionRead = Input.mousePosition;
    MousePositionRead.z = 5.23f; //The distance between the camera and object

    //Converts the Mouse position into world coordinates
    PlayerPosition = Camera.main.WorldToScreenPoint(transform.position);

    //Calculates the vector between the player and the mouse and sets it
    MousePositionRead.x = MousePositionRead.x - PlayerPosition.x;
    MousePositionRead.y = MousePositionRead.y - PlayerPosition.y;

    //Calculates the angle the arm has to be turned to
    angle = Mathf.Atan2(MousePositionRead.y, MousePositionRead.x) * Mathf.Rad2Deg;

    //Makes sure the Arm is working in both directions
    Rotation.z = (angle > 90 || angle < -90) ? 180 - angle : angle;
    Rotation.y = (angle > 90 || angle < -90) ? 180f : 0;

    //Sets the Animator variable so it knows if the player is looking to the left or right
    playerSpriteAnimator.SetBool("FacingRight", MousePositionRead.x > 0 ? true : false);

    //Rotates the arm to point at the mouse
    transform.rotation = Quaternion.Euler(Rotation);
  }
}

