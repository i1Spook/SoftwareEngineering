using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShot : MonoBehaviour
{

    public GameObject BluePortal;
    public float winkel;
    public Vector3 Showme;
    Quaternion Rotation;
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D CollisionWith)
    {
        Rotation = CollisionWith.gameObject.transform.rotation;
        winkel = Rotation.eulerAngles.z;
        BluePortal.transform.position = transform.position;

        Showme = transform.right;

        var angle = 180f;
        if (((winkel == 90) || (winkel == -90)) && (transform.right.y > 0f))
        {
            angle = 0f;
        }
        else if ((winkel == 0) && (transform.right.x > 0f))
        {
            angle = 0f;
        }
        CallResetShot();
              

            BluePortal.transform.rotation = Rotation * Quaternion.Euler(0, 0, angle);

        Destroy(gameObject);
    }
   

    void CallResetShot ()
    {
        if (tag == "BluePortal")
        {
            AimAtMouse.ResetShot(true, false);
        }
        else if (tag == "OrangePortal")
        {
            AimAtMouse.ResetShot(false, true);
        }
    }
}
