using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    public float force;             //Floating point variable to store the player's movement speed.


    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        Vector2 moveVertical = new Vector2
        {
            y = (Input.GetKeyDown(KeyCode.Space)) ? 1 : 0
        };

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2
        {
            x = moveHorizontal
        };

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
        rb2d.AddForce(moveVertical * 50 * force);
    }
}
