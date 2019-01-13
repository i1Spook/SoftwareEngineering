using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
  // Changes the player and weapon sprites upon entering a collision box

  public Sprite playerBodyBefore;
  public Sprite playerBodyAfter;

  public Sprite playerArmsBefore;
  public Sprite playerArmsAfter;

  private SpriteRenderer spriteRendererPlayer;
  private SpriteRenderer spriteRendererGun;

  void Start()
  {
    gameObject.GetComponent<ItemInputHandler>().enabled = false;

    spriteRendererPlayer = GetComponent<SpriteRenderer>();
    spriteRendererGun = GameObject.FindGameObjectWithTag("PortalGunOnPlayer").GetComponent<SpriteRenderer>();

    if (spriteRendererPlayer.sprite == null)
    {
      spriteRendererPlayer.sprite = playerBodyBefore;
    }
  }

  void ChangeSprite()
  {
    if (spriteRendererPlayer.sprite == playerBodyBefore)
    {
      spriteRendererPlayer.sprite = playerBodyAfter;
      spriteRendererGun.sprite = playerArmsAfter;

      gameObject.GetComponent<ItemInputHandler>().enabled = true;
    }
    else
    {
      spriteRendererPlayer.sprite = playerBodyBefore;
      spriteRendererGun.sprite = playerArmsBefore;

      gameObject.GetComponent<ItemInputHandler>().enabled = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag.ToUpper() == "PORTALGUN")
    {      
      FindObjectOfType<AudioManager>().PlayAt("ItemPickUp");

      ChangeSprite();

      GameObject.FindGameObjectWithTag("PortalGun").SetActive(false);
    }
  }
}
