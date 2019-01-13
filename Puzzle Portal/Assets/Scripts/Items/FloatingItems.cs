using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItems : MonoBehaviour
{
  // Simple ItemFloater moving an object up and down, 
  // allowing for customizable float strength

  float originalY;

  public float floatStrength = 1;

  void Start ()
  {
    this.originalY = transform.position.y;
	}

	void Update ()
  {
    transform.position = new Vector3(transform.position.x,
                         originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
                         transform.position.z);
  }
}
