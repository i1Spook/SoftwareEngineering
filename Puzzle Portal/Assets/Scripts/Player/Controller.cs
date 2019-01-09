
using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float maxSpeed;
    public float jumpForce = 400f;
    public static bool facingRight;
    public bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    float groundRadius = 0.01f;

    Rigidbody2D rb;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        maxSpeed = 10f;

        facingRight = true;

        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", rb.velocity.x);

        float regulatedSpeed;

		if (move * maxSpeed > 2) {
			regulatedSpeed = 2;
		} else {
			regulatedSpeed = move * maxSpeed;
		}
			

        if (grounded == false)
        {
			rb.AddForce(new Vector2(regulatedSpeed, 0));
        }
        else
        {
            rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        }


        //Flipping Logic
        if (((AimAtMouse.MousePositionRead.x - rb.position.x) > 0) && !facingRight)
        {
            Flip();
        }
        else if (((AimAtMouse.MousePositionRead.x - rb.position.x) < 0) && facingRight)
        {

            Flip();
        }
    }

    void Update()
    {
        //Jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            //FindObjectOfType<AudioManager>().Play("Jump");
            rb.AddForce(new Vector2(0, jumpForce));
        }

    }

    /// <summary>
    /// Fliping the Sprite
    /// </summary>
	void Flip()
    {
        facingRight = !facingRight;

        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}
