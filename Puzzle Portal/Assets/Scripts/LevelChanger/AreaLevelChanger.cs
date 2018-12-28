using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaLevelChanger : MonoBehaviour {

  //LevelChanger changer;
  [SerializeField] private string newLevel;

  void OnTriggerEnder2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      //changer.FadeToNextLevel();
      SceneManager.LoadScene(newLevel);
    }
  }
}
