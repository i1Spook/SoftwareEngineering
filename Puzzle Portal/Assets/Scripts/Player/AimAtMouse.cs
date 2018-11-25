using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
    public GameObject BlueOriginal;
    public GameObject OrangeOriginal;
    public GameObject FollowMouse;
    public float force = 1;

    public Vector3 MousePositionRead;
    public Vector2 MousePositionActual;
    public Vector3 MyPos;
    Vector3 Rot;
    float angle;
    int PrimaryMouseButton;
    int SecondaryMouseButton;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MousePositionRead = Input.mousePosition;
        MousePositionRead.z = 5.23f; //The distance between the camera and object
        MyPos = Camera.main.WorldToScreenPoint(transform.position);
        MousePositionRead.x = MousePositionRead.x - MyPos.x;
        MousePositionRead.y = MousePositionRead.y - MyPos.y;
        angle = Mathf.Atan2(MousePositionRead.y, MousePositionRead.x) * Mathf.Rad2Deg;
        Rot.z = angle;
        transform.rotation = Quaternion.Euler(Rot);

        bool ShotFiredBlue = Input.GetMouseButtonDown(0);
        bool ShotFiredOrange = Input.GetMouseButtonDown(1);

        if (ShotFiredBlue)
        {
            GameObject BlueShot = Instantiate(BlueOriginal);
            BlueShot.GetComponent<Rigidbody2D>().transform.position = transform.position;
            BlueShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            Vector2 Pointer;
            Pointer.x = -MousePositionRead.y;
            Pointer.y = MousePositionRead.x;

            BlueShot.transform.right=Pointer ;
            BlueShot.GetComponent<Rigidbody2D>().AddForce(MousePositionRead.normalized * force);
        }

        if (ShotFiredOrange)
        {
            GameObject OrangeShot = Instantiate(OrangeOriginal);
            OrangeShot.GetComponent<Rigidbody2D>().transform.position = transform.position;
            OrangeShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            Vector2 Pointer;
            Pointer.x = -MousePositionRead.y;
            Pointer.y = MousePositionRead.x;

            OrangeShot.transform.right = Pointer;
            OrangeShot.GetComponent<Rigidbody2D>().AddForce(MousePositionRead.normalized * force);
        }
    }
}
