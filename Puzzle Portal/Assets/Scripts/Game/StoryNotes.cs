using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNotes : MonoBehaviour
{
  // Pops up a story note whenever the player stands at it

  public GameObject StoryNote;

  void Start ()
  {
    StoryNote.gameObject.SetActive(false);
  }

  void OnTriggerEnter2D(Collider2D CollidedWith)
  {
    StoryNote.gameObject.SetActive(true);
  }

  void OnTriggerExit2D(Collider2D CollidedWith)
  {
    StoryNote.gameObject.SetActive(false);
  }
}
