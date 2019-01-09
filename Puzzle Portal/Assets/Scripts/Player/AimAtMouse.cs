using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
    Animator anim;
    public static Vector3 MousePositionRead;
  
    static Vector3 PlayerPosition;
    Vector3 Rotation;
    float angle;

    bool FlippedArm = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponentInParent<Animator>();
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

        Rotation.z = (angle > 90 || angle < -90) ? 180 - angle : angle;

        Rotation.y = (angle > 90 || angle < -90) ? 180f : 0;

        anim.SetBool("FacingRight", MousePositionRead.x > 0 ? true : false);

        transform.rotation = Quaternion.Euler(Rotation);
    }
    
//void FlipArm()
//    {
//        GameObject Arm = gameObject.transform.GetChild(0).gameObject;

//        Arm.GetComponent<SpriteRenderer>().flipX = !Arm.GetComponent<SpriteRenderer>().flipX;

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

