using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaLevelChanger : MonoBehaviour
{
  public Animator animator;

  int nextLevel;

  int levelToLoad;

  public static bool initiatedLevelChange = false;
  public static int CurrentLevel { get; private set; }
  void Start()
  {
    CurrentLevel = SceneManager.GetActiveScene().buildIndex;

    nextLevel = CurrentLevel + 1;
  }
  void OnTriggerEnter2D(Collider2D CollidedWith)
  {
    if (CollidedWith.CompareTag("Player") && ItemScript.AllKeycardsCollected())
    {
      Debug.Log("Collided");
      FadeToNextLevel();
    }
  }
  public void FadeToNextLevel()
  {
    FadeToLevel(nextLevel);
  }
  public void FadeToLevel(int levelIndex)
  {
    initiatedLevelChange = true;
    levelToLoad = levelIndex;
    animator.SetTrigger("FadeOut");
  }
  public void OnFadeComplete()
  {
    SceneManager.LoadScene(levelToLoad);
  }
}
