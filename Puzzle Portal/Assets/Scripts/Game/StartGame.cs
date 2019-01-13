using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
  // Provides functionality to the StartGame Button,
  // such as disabling itself after click and enabling the character controls

  GameObject titleScreen;
  GameObject player;
  GameObject startGameButton;

  public static bool startGameButtonPressed; 

  void Start ()
  {
    titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");

    startGameButton = GameObject.FindGameObjectWithTag("TitleStartGame");

    player = GameObject.FindGameObjectWithTag("Player");
  }

  void OnMouseDown()
  {
    startGameButtonPressed = true;
    TitleScreenController.titleScreenActive = false; 

    player.GetComponent<Controller>().enabled = true;

    startGameButton.GetComponent<Text>().enabled = false;
    startGameButton.GetComponent<BoxCollider2D>().enabled = false;

    titleScreen.gameObject.SetActive(false);
  }
}
