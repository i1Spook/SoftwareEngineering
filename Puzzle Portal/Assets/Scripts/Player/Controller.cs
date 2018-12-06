
﻿using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float maxSpeed;
	bool facingRight;
	Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
        maxSpeed = 10f;
        facingRight = true;
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{		
		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	 void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
	    transform.localScale = theScale;
	}
}