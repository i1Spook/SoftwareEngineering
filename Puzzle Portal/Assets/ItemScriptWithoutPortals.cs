using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptWithoutPortals : MonoBehaviour
{
    public GameObject Player;

    public bool GotKeycard;
    public static int KeycardsCollected;
    public static int KeycardsNeeded;

    static GameObject Keycard;

    public static GameObject LastHit;

    static GameObject ItemInHand;
    public static bool ItemInHandToggle;
    public GameObject ArmLocation;
    static GameObject ArmLocationStatic;

    public static bool AtItem;

    void Start()
    {
        GotKeycard = false;
        
        ItemInHand = null;
        ItemInHandToggle = false;

        KeycardsNeeded = GameObject.FindGameObjectsWithTag("Keycard").Length;
        Debug.Log("Needed" + KeycardsNeeded);
        Debug.Log("Collected" + KeycardsCollected);

        ArmLocationStatic = ArmLocation;
    }

    void Update()
    {
       
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
              
        else if (CollisionWith.gameObject.tag.ToUpper() == "DEATH")
        {
            FindObjectOfType<AudioManager>().PlayAt("PlayerDeath");

            KeycardsCollected = 0;
            RestartLevel.Restart();
        }
        else if (CollisionWith.gameObject.name.ToUpper() == "PROJECTILE")
        {
            FindObjectOfType<AudioManager>().PlayAt("ProjectileHit");
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

        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "SMOKE")
        {

        }
        else if (CollisionWith.gameObject.tag.ToUpper() == "TURRET")
        {
    
        }
    }
    public static bool AllKeycardsCollected()
    {
        int collected = ItemScript.KeycardsCollected;
        int needed = KeycardsNeeded;

        if (collected == needed)
        {
            Debug.Log("Fade successful");
            Debug.Log("Needed" + needed);
            Debug.Log("Collected" + collected);



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
