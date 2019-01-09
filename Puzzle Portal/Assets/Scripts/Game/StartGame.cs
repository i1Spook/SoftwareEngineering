using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

  GameObject titleScreen;
  GameObject player;
  GameObject startGameButton;

  public static bool startGameButtonPressed; 

  void Start ()
  {
    titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");
    startGameButton = GameObject.FindGameObjectWithTag("TitleStartGame");

    player = GameObject.FindGameObjectWithTag("Player");
    Debug.Log(player);
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
