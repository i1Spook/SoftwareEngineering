using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float maxSpeed = 10f;
	bool facingRight = true;
	Rigidbody2D rb;
	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius=0.21f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat(("Speed"), Mathf.Abs(move));

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
