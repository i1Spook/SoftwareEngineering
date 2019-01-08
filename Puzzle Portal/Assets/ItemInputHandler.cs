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

    public float projectileForce = 500;

    public static bool ItemWasShot;
    // Use this for initialization
    void Start()
    {
        BlueFired = false;
        OrangeFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        ItemWasShot = false;

        bool ItemButtonPressed = Input.GetKeyDown(KeyCode.E);
        bool LeftMouseButtonPressed = Input.GetMouseButtonDown(0);
        bool RightMouseButtonPressed = Input.GetMouseButtonDown(1);

        ItemScript.ItemInteraction(ItemButtonPressed, LeftMouseButtonPressed || RightMouseButtonPressed);

        if (!ItemWasShot)
        {
            ShootPortal(LeftMouseButtonPressed, RightMouseButtonPressed);
        }
    }

    void ShootPortal(bool ShotFiredBlue, bool ShotFiredOrange)
    {
        float Lifespan = 5;

        if (!ItemScript.ItemInHandToggle && ShotFiredBlue && (!BlueFired || timerBlue > Lifespan))
        {
            FindObjectOfType<AudioManager>().PlayAt("BlueShot");
            BlueFired = true;
            GameObject BlueShot = Instantiate(BlueOriginal);
            BlueShot.GetComponent<Rigidbody2D>().transform.position =ArmLocation.transform.position;
            BlueShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            FireObject(BlueShot, projectileForce);

            timerBlue = 0;
        }

        if (!ItemScript.ItemInHandToggle && ShotFiredOrange && (!OrangeFired || timerOrange > Lifespan))
        {
            FindObjectOfType<AudioManager>().PlayAt("OrangeShot");
            OrangeFired = true;
            GameObject OrangeShot = Instantiate(OrangeOriginal);
            OrangeShot.GetComponent<Rigidbody2D>().transform.position = transform.position;
            OrangeShot.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            FireObject(OrangeShot, projectileForce);

            timerOrange = 0;
        }

        timerBlue += Time.deltaTime;
        timerOrange += Time.deltaTime;

    }

    public static void FireObject(GameObject Object, float projectileForce)
    {
        Vector2 Pointer;
        Pointer.x = -AimAtMouse.MousePositionRead.y;
        Pointer.y = AimAtMouse.MousePositionRead.x;

        Object.transform.right = Pointer;
        Object.GetComponent<Rigidbody2D>().AddForce(AimAtMouse.MousePositionRead.normalized * projectileForce);
    }

    public static void ResetShot(bool BlueHit, bool OrangeHit)
    {
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
