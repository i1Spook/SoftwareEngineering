using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShot : MonoBehaviour
{

    public GameObject DropPrefabReference;


    // Use this for initialization
    void Start()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
        {
            if (item.name == "NewOrangePortal" || item.name == "NewBluePortal")
            {
                DropPrefabReference = item;
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
            FindObjectOfType<AudioManager>().PlayAt("PortalWallHit2");

            Rotation = CollisionWith.gameObject.transform.rotation;
            winkel = Rotation.eulerAngles.z;
            DropPrefabReference.transform.position = transform.position;

            var angle = 180f;
            if (((winkel == 90) || (winkel == -90)) && (transform.right.y > 0f))
            {
                angle = 0f;
            }
            else if ((winkel == 0) && (transform.right.x > 0f))
            {
                angle = 0f;
            }

            DropPrefabReference.GetComponent<PortalScript>().PortalCreated = true;

            DropPrefabReference.transform.rotation = Rotation * Quaternion.Euler(0, 0, angle);
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
