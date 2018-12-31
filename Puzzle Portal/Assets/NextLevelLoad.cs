using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D Object)
    {
        if ( (Object.gameObject.tag.ToUpper() == "PLAYER") && ItemScript.GotKeycard)
        {
            LevelChanger changer = new LevelChanger();
            changer.FadeToNextLevel();
        }

    }
}
