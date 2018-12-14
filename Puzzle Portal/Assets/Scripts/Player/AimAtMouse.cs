﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
   
    public static Vector3 MousePositionRead;
  
    static Vector3 PlayerPosition;
    Vector3 Rotation;
    float angle;

    bool FlippedArm = false;

  

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MousePositionRead = Input.mousePosition;
        MousePositionRead.z = 5.23f; //The distance between the camera and object
        PlayerPosition = Camera.main.WorldToScreenPoint(transform.position);
        MousePositionRead.x = MousePositionRead.x - PlayerPosition.x;
        MousePositionRead.y = MousePositionRead.y - PlayerPosition.y;
        angle = Mathf.Atan2(MousePositionRead.y, MousePositionRead.x) * Mathf.Rad2Deg;
        Rotation.z = angle;
        transform.rotation = Quaternion.Euler(Rotation);       

        if (Controller.facingRight && FlippedArm)
        {
            FlipArm();
        }
        else if (!Controller.facingRight && !FlippedArm)
        {
            FlipArm();

        }
    }
    

void FlipArm()
    {
        GameObject Arm = gameObject.transform.GetChild(0).gameObject;
        Vector3 theScale = Arm.transform.localScale;
        theScale.x *= -1;
        theScale.y *= -1;
        Arm.transform.localScale = theScale;
        FlippedArm = !FlippedArm;
    }


    

}
