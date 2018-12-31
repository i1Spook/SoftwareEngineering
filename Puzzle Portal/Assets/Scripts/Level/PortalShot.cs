using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShot : MonoBehaviour
{

    public GameObject PortalReference;

    


    // Use this for initialization
    void Start()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
        {
            if (item.name != name)
            {
                PortalReference = item;
                break;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D CollisionWith)
    {
        float winkel;
        Quaternion Rotation;

        if (CollisionWith.gameObject.tag.ToUpper() == "PORTALWALL")
        {
            Rotation = CollisionWith.gameObject.transform.rotation;
            winkel = Rotation.eulerAngles.z;
            PortalReference.transform.position = transform.position;

            var angle = 180f;
            if (((winkel == 90) || (winkel == -90)) && (transform.right.y > 0f))
            {
                angle = 0f;
            }
            else if ((winkel == 0) && (transform.right.x > 0f))
            {
                angle = 0f;
            }

            if (PortalReference.name == "NewOrangePortal")
            {
                OrangePortalScript.PortalCreated = true;
            }
            else if (PortalReference.name == "NewBluePortal")
            {
                BluePortalScript.PortalCreated = true;
            }
            PortalReference.transform.rotation = Rotation * Quaternion.Euler(0, 0, angle);
        }

        CallResetShot();
        Destroy(gameObject);
    }


    void CallResetShot()
    {
        if (tag == "BluePortal")
        {
            ItemInputHandler.ResetShot(true, false);
        }
        else if (tag == "OrangePortal")
        {
            ItemInputHandler.ResetShot(false, true);
        }
    }
}
