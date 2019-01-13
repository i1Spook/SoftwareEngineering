using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemScript : MonoBehaviour
{
  public GameObject Player;
  public GameObject BluePortal;
  public GameObject OrangePortal;
  public GameObject ArmLocation;

  public static bool GotKeycard;
  public static bool ItemInHandToggle;
  public static bool InLight;
  public static bool Illuminated;
  public static bool AllPortalsCreated;
  public static bool AtItem;
  public static bool PortalGunFound;

  public static int KeycardsCollected;
  public static int KeycardsNeeded;

  public static uint SmokeCounter;

  public static GameObject LastHit;

  static GameObject Keycard;
  static GameObject ItemInHand;
  static GameObject ArmLocationStatic;
  
  GameObject ThisTurret;
  bool AtTurret;

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

    int[] SpritePreset = new int[] { 0, 1, 2, 3, 5, 7, 9 };

    //En- and Disables the Portalgun depending on what level the player is
    foreach (int levelFilter in SpritePreset)
    {
      //Checks what level the player is on and then turns on and off the right sprites and bools
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
      //Check if both portals where created (so the player won't be teleported outside the level
      if (BluePortal.GetComponent<PortalScript>().PortalCreated && OrangePortal.GetComponent<PortalScript>().PortalCreated)
      {
        AllPortalsCreated = true;
      }
    }

    //Checks if the player is visible in the light based on how much smoke is around him
    InLight = (SmokeCounter < 3) && (Illuminated) ? true : false;

    //When the player is at a turret he can turn it off
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
      //Check if itembutton is pressed, the player is by an item and has nothing in his hand
      if (ItemButtonPressed && AtItem && !ItemInHandToggle)
      {
        FindObjectOfType<AudioManager>().PlayAt("ItemPickUp");

        //The Object that the player is currently on gets set as child of the player and moved to the hand
        LastHit.gameObject.transform.position = ArmLocationStatic.transform.position;
        LastHit.transform.SetParent(ArmLocationStatic.gameObject.transform);

        //The phyics and the colliders are turned off for the item
        LastHit.GetComponent<Rigidbody2D>().isKinematic = true;
        LastHit.GetComponent<Rigidbody2D>().simulated = false;

        //The item gets saved in a non temporary variable
        ItemInHand = LastHit;
        ItemInHandToggle = true;

        //Enables the hitbox of the fire extinguisher 
        if (ItemInHand.name == "FireExtinguisher")
        {
          ItemInHand.GetComponent<FireExt>().EnableFireExtHitbox();
        }
      }

      //If the itembutton is pressed and an item is in the hand it will be dropped
      else if (ItemButtonPressed && ItemInHandToggle)
      {
        FindObjectOfType<AudioManager>().PlayAt("ItemDrop");

        //Physics and colliders are turned back on
        ItemInHand.GetComponent<Rigidbody2D>().isKinematic = false;
        ItemInHand.GetComponent<Rigidbody2D>().simulated = true;

        //Item gets removed from player
        ItemInHand.transform.parent = null;
        ItemInHandToggle = false;
      }

      //If the trownbutton is pressed and an item is in the hand it will be thrown
      if (ThrowButtonPressed && ItemInHandToggle)
      {
        FindObjectOfType<AudioManager>().PlayAt("ItemThrow");

        //Item gets removed from player
        ItemInHand.transform.parent = null;

        //Physics and collider are turned back on
        ItemInHand.GetComponent<Rigidbody2D>().isKinematic = false;
        ItemInHand.GetComponent<Rigidbody2D>().simulated = true;
        
        //Passes the object to the throwing/firing method
        ItemInputHandler.FireObject(ItemInHand, 500);

        ItemInHand = null;
        ItemInHandToggle = false;

        ItemInputHandler.ItemWasShot = true;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D CollisionWith)
  {
    //Checks with what the player collided
    if (CollisionWith.gameObject.tag.ToUpper() == "KEYCARD")
    {
      //Picks up the keycard

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
      //Enables the picking up 
      AtItem = true;

      LastHit = CollisionWith.gameObject;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
    {
      //Tells the enemy if the player is touching light
      Illuminated = true;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "SMOKE")
    {
      //Checks how much smoke the player is standing in
      SmokeCounter++;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "DEATH")
    {
      //Restarts the level if the player hit anything lethal
      FindObjectOfType<AudioManager>().PlayAt("PlayerDeath");

      Illuminated = false;

      KeycardsCollected = 0;

      RestartLevel.Restart();
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
    {
      //Enables the turning off of turrets
      AtTurret = true;

      ThisTurret = CollisionWith.gameObject;
    }
    else if (CollisionWith.gameObject.name.ToUpper() == "PROJECTILE")
    {
      //Checks if the player got hit so the sound can play
      FindObjectOfType<AudioManager>().PlayAt("ProjectileHit");
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "PORTALGUN")
    {
      //Enables the portalgun as soon as the player found it
      PortalGunFound = true;

      GameObject.Find("PlayerGunSprite").GetComponent<SpriteRenderer>().enabled = true;
      GameObject.Find("PlayerHandSprite").GetComponent<SpriteRenderer>().enabled = false;
    }
  }

  void OnTriggerExit2D(Collider2D CollisionWith)
  {
    //Resets all the variables from "OnTriggerEnter2D"
    if (CollisionWith.gameObject.tag.ToUpper() == "ITEM")
    {
      //No item to pick up
      AtItem = false;

      LastHit = null;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "LIGHT")
    {
      //Not in light anymore
      Illuminated = false;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "SMOKE")
    {
      //Player is less covered by smoke
      SmokeCounter--;
    }
    else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
    {
      //Turret not nearby
      AtTurret = false;

      ThisTurret = null;
    }
  }
  public static bool AllKeycardsCollected()
  {
    int collected = KeycardsCollected;
    int needed = KeycardsNeeded;

    //Are all keycards collected?
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
