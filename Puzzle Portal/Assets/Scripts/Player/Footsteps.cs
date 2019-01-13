using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

  
  Controller cc;
	// Use this for initialization
	void Start () {
    cc = GetComponent<Controller>();  
	}
	
	// Update is called once per frame
	void Update () {
    if (cc.grounded == true && cc.maxSpeed == 10f && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && GetComponent<AudioSource>().isPlaying == false)
    {
      GetComponent<AudioSource>().volume = Random.Range(0.3f, 0.5f);
      GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.1f);
      GetComponent<AudioSource>().Play();
    }
	}
}
