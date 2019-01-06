using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
   
    public static Vector3 MousePositionRead;

    GameObject Arm;

    Vector3 ShoulderPosition;
    public Vector3 Rotation;
    public float angle;

    bool FlippedArm;


    // Use this for initialization
    void Start()
    {
        FlippedArm = false;
        Arm = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MousePositionRead = Input.mousePosition;
        MousePositionRead.z = 5.23f; //The distance between the camera and object
        ShoulderPosition = Camera.main.WorldToScreenPoint(transform.position);
        MousePositionRead.x = MousePositionRead.x - ShoulderPosition.x;
        MousePositionRead.y = MousePositionRead.y - ShoulderPosition.y;
        angle = Mathf.Atan2(MousePositionRead.y, MousePositionRead.x) * Mathf.Rad2Deg;
        Rotation.z = (angle > 90 || angle < -90) ? 180 - angle : angle;

        Rotation.y = (angle > 90 || angle < -90) ? 180f : 0;

        transform.rotation = Quaternion.Euler(Rotation);       
        
        //if ((angle > 90 || angle < -90) && !FlippedArm)
        //{
        //    Arm.GetComponent<SpriteRenderer>().flipX = !Arm.GetComponent<SpriteRenderer>().flipX;
        //    FlippedArm = true;
        //}
        //else if ((angle < 90 || angle > -90) && FlippedArm)
        //{
        //    Arm.GetComponent<SpriteRenderer>().flipX = !Arm.GetComponent<SpriteRenderer>().flipX;
        //    FlippedArm = false;
        //}
    }
    
//void FlipArm()
//    {
//        GameObject Arm = gameObject.transform.GetChild(0).gameObject;

//        //Arm.GetComponent<SpriteRenderer>().flipX = !Arm.GetComponent<SpriteRenderer>().flipX;

//        if (!FlippedArm)
//        {
//            Arm.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
//        }
//        else
//        {
//            Arm.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
//        }
//    } 
}

