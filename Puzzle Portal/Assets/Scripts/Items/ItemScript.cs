using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject Player;

    bool GotKeycard = false;

    public static GameObject LastHit;
    public static bool LastHitToggle = false;

    static GameObject ItemInHand;
    public static bool ItemInHandToggle;
    public static GameObject ArmLocation;

    public static bool InLight = false;

    public static bool AtItem = false;

    void Start()
    {
        ArmLocation = Player.gameObject.transform.GetChild(0).gameObject;
    }

    public static void ItemInteraction(bool ItemButtonPressed , bool ThrowButtonPressed)
    {
        if (ItemButtonPressed && LastHitToggle && !ItemInHandToggle)
        {
            LastHit.gameObject.transform.position = ArmLocation.transform.GetChild(0).gameObject.transform.position;
            LastHit.transform.SetParent(ArmLocation.gameObject.transform);

            LastHit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic ;
            LastHit.GetComponent<Rigidbody2D>().gravityScale = 0;

            ItemInHand = LastHit;
            ItemInHandToggle = true;
        }
        else if (ItemButtonPressed && ItemInHandToggle)
        {
            ItemInHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ItemInHand.GetComponent<Rigidbody2D>().gravityScale = 1;

            ItemInHand.transform.parent = null;
            ItemInHandToggle = false;
        }

        if (ThrowButtonPressed && ItemInHandToggle)
        {
            ItemInHand.transform.parent = null;
            ItemInHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ItemInHand.GetComponent<Rigidbody2D>().gravityScale = 1;


            ItemInputHandler.FireObject(ItemInHand, 500);
            ItemInHand = null;
            ItemInHandToggle = false;

            ItemInputHandler.ItemWasShot = true;
        }
    }

    void OnTriggerEnter2D(Collider2D CollisionWith)
    {
        if (CollisionWith.gameObject.tag.ToUpper() == "KEYCARD")
        {
            GotKeycard = true;
            CollisionWith.gameObject.SetActive(false);
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "ITEM")
        {
            AtItem = true;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
        {
            InLight = true;
        }

            LastHit = CollisionWith.gameObject;
        LastHitToggle = true;
    }

    void OnTriggerExit2D(Collider2D CollisionWith)
    {        
        if (CollisionWith.gameObject.tag.ToUpper() == "ITEM")
        {
            AtItem = false;
            LastHit = null;
            LastHitToggle = false;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
        {
            InLight = false;
        }
        //LastHit = null;
        //LastHitToggle = false;
        //AtItem = false;
    }
}
