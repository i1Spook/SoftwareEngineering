using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNotes : MonoBehaviour
{
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
