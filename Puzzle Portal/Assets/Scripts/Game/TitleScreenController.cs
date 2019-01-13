using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
  // Handles the logic for the TitleScreen/ PauseScreen
  // Upon starting the game for the first time, the TitleScreen turns into the PauseScreen
  // Disables player control during pause

  GameObject titleScreen;
  GameObject player;
  GameObject Camera;

  public static TitleScreenController instance;

  public static bool titleScreenActive;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");

    player = GameObject.FindGameObjectWithTag("Player");

    if (AreaLevelChanger.CurrentLevel == 0)
    {
      titleScreen.gameObject.SetActive(true);
      titleScreenActive = true;

      player.GetComponent<Controller>().enabled = false;
    }
  }

  void Update()
  {
    Camera = GameObject.FindGameObjectWithTag("MainCamera");

    transform.position = Camera.transform.position;
    transform.position = Camera.transform.position;

    if (StartGame.startGameButtonPressed)
    {
      if (Input.GetKeyDown(KeyCode.Escape) && titleScreenActive == false)
      {
        titleScreen.gameObject.SetActive(true);
        titleScreenActive = true;

        player.GetComponent<Controller>().enabled = false;
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && titleScreenActive == true)
      {
        titleScreen.gameObject.SetActive(false);
        titleScreenActive = false;

        player.GetComponent<Controller>().enabled = true;
      }
    }
  }
}
