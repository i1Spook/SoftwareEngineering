using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartLevel : MonoBehaviour {


	// Use this for initialization
	public static void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
