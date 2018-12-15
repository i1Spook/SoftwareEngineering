using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject Player;

    bool GotKeycard;

    public static GameObject LastHit;

    static GameObject ItemInHand;
    public static bool ItemInHandToggle;
    public GameObject ArmLocation;
    static GameObject ArmLocationStatic;

    public static uint SmokeCounter;
    public static bool InLight;
    public static bool Illuminated;

    GameObject ThisTurret;
    bool AtTurret;

    public static bool AtItem;

    void Start()
    {
        GotKeycard = false;
        SmokeCounter = 0;
        InLight = false;
        Illuminated = false;
        AtItem = false;
        AtTurret = false;

        ArmLocationStatic = ArmLocation;
    }

    void Update()
    {
        InLight = (SmokeCounter < 3) && (Illuminated) ? true : false;
        if (AtTurret && Input.GetKeyDown(KeyCode.Q))
        {
            TurnOffTurret();
        }
    }

    void TurnOffTurret()
    {
        ThisTurret.GetComponent<TurretSkriptFinal>().Active = false;
    }

    public static void ItemInteraction(bool ItemButtonPressed, bool ThrowButtonPressed)
    {
        if (ItemButtonPressed && AtItem && !ItemInHandToggle)
        {
            LastHit.gameObject.transform.position = ArmLocationStatic.transform.position;
            LastHit.transform.SetParent(ArmLocationStatic.gameObject.transform);

            LastHit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
            LastHit = CollisionWith.gameObject;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
        {
            Illuminated = true;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "SMOKE")
        {
            SmokeCounter++;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "DEATH")
        {

            Illuminated = false;
            RestartLevel.Restart();
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
        {
            AtTurret = true;
            ThisTurret = CollisionWith.gameObject;
        }


    }

    void OnTriggerExit2D(Collider2D CollisionWith)
    {
        if (CollisionWith.gameObject.tag.ToUpper() == "ITEM")
        {
            AtItem = false;
            LastHit = null;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
        {
            Illuminated = false;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "SMOKE")
        {
            SmokeCounter--;
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
        {
            AtTurret = false;
            ThisTurret = null;
        }
    }
}
