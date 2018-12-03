using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public bool KeyCardFound = false;
    public bool Test = false;

    void OnTriggerEnter2D(Collider2D CollisionWith)
    {
        if (CollisionWith.gameObject.tag.ToUpper() == "KEYCARD")
        {
            //AUTO PICKUP at the moment
            KeyCardFound = true;
            Destroy(CollisionWith.gameObject);
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "FIREEXT")
        {
            Test = true;
            //Call Item Pickup Routine
            CollisionWith.gameObject.transform.Rotate(0, 0, 90);
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "ITEM")
        {
            //Call Item Pickup Routine
        }
    }
}
