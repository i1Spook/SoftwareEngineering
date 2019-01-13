using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
  // Provides functionality to the ContinueGame Button,
  // which is only active after the StartGame Button has been pressed

  GameObject titleScreen;
  GameObject continueButton;
  GameObject player;

  void Start ()
  {
    continueButton = GameObject.FindGameObjectWithTag("Continue");
    continueButton.GetComponent<BoxCollider2D>().enabled = false;
    continueButton.GetComponent<Text>().enabled = false;

    player = GameObject.FindGameObjectWithTag("Player");

    titleScreen = GameObject.FindGameObjectWithTag("TitleScreen");
  }

	void Update ()
  {
    if (StartGame.startGameButtonPressed)
    {
      continueButton.GetComponent<Text>().enabled = true;
      continueButton.GetComponent<BoxCollider2D>().enabled = true;
    }
  }

  void OnMouseDown()
  {
    TitleScreenController.titleScreenActive = false;

    player.GetComponent<Controller>().enabled = true;

    titleScreen.gameObject.SetActive(false);
  }
}
