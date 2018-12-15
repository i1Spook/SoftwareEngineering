
﻿using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float maxSpeed;
	public static bool facingRight;
	Rigidbody2D rb;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius=0.2f;
	public LayerMask whatIsGround;
	
	public float jumpForce = 400f;

	// Use this for initialization
	void Start ()
	{
        maxSpeed = 10f;
        facingRight = true;
	    rb = GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		
		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (((AimAtMouse.MousePositionRead.x-rb.position.x)>0) && !facingRight) {
			Flip ();
		} else if (((AimAtMouse.MousePositionRead.x-rb.position.x)<0) && facingRight) {
			Flip ();         
        }
	}
	
	void Update()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce(new Vector2 (0, jumpForce));
		}
			
	}

	 void Flip()
	{
		facingRight = !facingRight;

        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        //transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().flipY = !transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().flipY;

        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //   transform.localScale = theScale;
    }
}
