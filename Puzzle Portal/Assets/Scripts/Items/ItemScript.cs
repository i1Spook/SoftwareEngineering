using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemScript : MonoBehaviour
{
    public GameObject Player;

    public static bool GotKeycard;
    public static int KeycardsCollected;
    public static int KeycardsNeeded;

    public GameObject BluePortal;
    public GameObject OrangePortal;

    static GameObject Keycard;

    public static GameObject LastHit;

    static GameObject ItemInHand;
    public static bool ItemInHandToggle;
    public GameObject ArmLocation;
    static GameObject ArmLocationStatic;

    public static bool AllPortalsCreated;

    public static uint SmokeCounter;
    public static bool InLight;
    public static bool Illuminated;

    GameObject ThisTurret;
    bool AtTurret;

    public static bool AtItem;

    public static bool PortalGunFound;

    void Start()
    {
        GotKeycard = false;
        SmokeCounter = 0;
        InLight = false;
        Illuminated = false;
        AtItem = false;
        AtTurret = false;

        AllPortalsCreated = false;

        ItemInHand = null;
        ItemInHandToggle = false;

        KeycardsNeeded = GameObject.FindGameObjectsWithTag("Keycard").Length;
        Debug.Log("Needed" + KeycardsNeeded);
        Debug.Log("Collected" + KeycardsCollected);

        ArmLocationStatic = ArmLocation;

        PortalGunFound = false;

        int[] SpritePreset = new int[] { 0, 1 ,2, 3 ,5,7,9};

        foreach (int levelFilter in SpritePreset)
        {
            if (SceneManager.GetActiveScene().buildIndex == levelFilter)
            {
                GameObject.Find("PlayerGunSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("PlayerHandSprite").GetComponent<SpriteRenderer>().enabled = true;
                PortalGunFound = false;
                break;
            }
            else
            {
                GameObject.Find("PlayerGunSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("PlayerHandSprite").GetComponent<SpriteRenderer>().enabled = false;
                PortalGunFound = true;
            }
        }

    }

    void Update()
    {
        if (PortalGunFound)
        {
            if (BluePortal.GetComponent<PortalScript>().PortalCreated && OrangePortal.GetComponent<PortalScript>().PortalCreated)
            {
                AllPortalsCreated = true;
            }
        }

        InLight = (SmokeCounter < 3) && (Illuminated) ? true : false;
        if (AtTurret && Input.GetKeyDown(KeyCode.Q))
        {
            FindObjectOfType<AudioManager>().PlayAt("TurretExplosion");
            TurnOffTurret();
        }
    }

    void TurnOffTurret()
    {
        ThisTurret.GetComponent<TurretSkriptFinal>().Active = false;
    }

    public static void ItemInteraction(bool ItemButtonPressed, bool ThrowButtonPressed)
    {
        if (PortalGunFound)
        {
            if (ItemButtonPressed && AtItem && !ItemInHandToggle)
            {
                FindObjectOfType<AudioManager>().PlayAt("ItemPickUp");
                LastHit.gameObject.transform.position = ArmLocationStatic.transform.position;
                LastHit.transform.SetParent(ArmLocationStatic.gameObject.transform);

                LastHit.GetComponent<Rigidbody2D>().isKinematic = true;
                LastHit.GetComponent<Rigidbody2D>().simulated = false;

                ItemInHand = LastHit;
                ItemInHandToggle = true;

                if(ItemInHand.name == "FireExtinguisher")
                {
                    ItemInHand.GetComponent<FireExt>().EnableFireExtHitbox();
                }
            }
            else if (ItemButtonPressed && ItemInHandToggle)
            {
                FindObjectOfType<AudioManager>().PlayAt("ItemDrop");
                ItemInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                ItemInHand.GetComponent<Rigidbody2D>().simulated = true;

                ItemInHand.transform.parent = null;
                ItemInHandToggle = false;
            }

            if (ThrowButtonPressed && ItemInHandToggle)
            {
                FindObjectOfType<AudioManager>().PlayAt("ItemThrow");
                ItemInHand.transform.parent = null;
                ItemInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                ItemInHand.GetComponent<Rigidbody2D>().simulated = true;


                ItemInputHandler.FireObject(ItemInHand, 500);
                ItemInHand = null;
                ItemInHandToggle = false;

                ItemInputHandler.ItemWasShot = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D CollisionWith)
    {
        if (CollisionWith.gameObject.tag.ToUpper() == "KEYCARD")
        {
          FindObjectOfType<AudioManager>().PlayAt("KeycardPickUp");
          GotKeycard = true;
          KeycardsCollected++;

            if (KeycardsCollected == KeycardsNeeded)
            {
                GotKeycard = true;
            }

          Debug.Log("Needed" + KeycardsNeeded);
          Debug.Log("Collected" + KeycardsCollected);
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
          FindObjectOfType<AudioManager>().PlayAt("PlayerDeath");
          Illuminated = false;
          KeycardsCollected = 0;
          RestartLevel.Restart();
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
        {
          AtTurret = true;
          ThisTurret = CollisionWith.gameObject;
        }
        else if (CollisionWith.gameObject.name.ToUpper() == "PROJECTILE")
        {
          FindObjectOfType<AudioManager>().PlayAt("ProjectileHit");
        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "PORTALGUN")
        {
            PortalGunFound = true;
            GameObject.Find("PlayerGunSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("PlayerHandSprite").GetComponent<SpriteRenderer>().enabled = false;
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
    public static bool AllKeycardsCollected()
    {
      int collected = KeycardsCollected;
      int needed = KeycardsNeeded;

      if (collected == needed)
      {
        Debug.Log("Fade successful");
        Debug.Log("Needed" + needed);
        Debug.Log("Collected" + collected);

      KeycardsCollected = 0;

        return true;
      }
      else
      {
        FindObjectOfType<AudioManager>().PlayAt("Error");
        Debug.Log("Not enough Keycards");
        return false;
      }
    }
}
