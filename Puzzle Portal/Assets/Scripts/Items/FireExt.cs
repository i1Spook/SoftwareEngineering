using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExt : MonoBehaviour
{
  const int spread = 10;

  public bool HighVelocity;
  public bool HitWall;
  public bool IsStationary;

  bool veloCheck;
  bool IsUsed;

  public GameObject Smoke1;
  public GameObject Smoke2;
  public GameObject Smoke3;
  public GameObject Smoke4;

  public float FireExtVelocity;

  System.Random Randomizer = new System.Random();

  //Initialize variables on each start
  void Start()
  {
    IsUsed = false;

    HighVelocity = false;

    HitWall = false;

    IsStationary = false;

    veloCheck = false;

    transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    FireExtVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;

    //Test if the fire extinguisher is not moving anymore
    IsStationary = (FireExtVelocity < 1) ? true : false;

    //If the impact force is high enough and the fire extinguisher is stationary it emits the big cloud
    if (HighVelocity && HitWall && IsStationary && !IsUsed)
    {
      FindObjectOfType<AudioManager>().PlayAt("FireExt");

      GameObject[] SmokeClone = new GameObject[32];

      //Creates smoke clouds
      for (int i = 0; i < 32; i++)
      {
        //Give each smoke Cloud a random direction 
        Vector2 RandomUp = new Vector2(Randomizer.Next(-spread, spread), Randomizer.Next(-spread, spread));

        //Randomizes wich smoke cloud gets created
        switch (Randomizer.Next(1, 5))
        {
          case 1:
            SmokeClone[i] = Instantiate(Smoke1);
            break;
          case 2:
            SmokeClone[i] = Instantiate(Smoke2);
            break;
          case 3:
            SmokeClone[i] = Instantiate(Smoke3);
            break;
          case 4:
            SmokeClone[i] = Instantiate(Smoke4);
            break;
        }

        SmokeClone[i].transform.position = transform.position;

        //Gives each cloud a random moving force in the established direction
        SmokeClone[i].GetComponent<Rigidbody2D>().AddForce(RandomUp.normalized * Randomizer.Next(5, 20));

      }

      HighVelocity = false;

      IsUsed = true;
    }

    //If the impact force is high enough and the fire extinguisher is moving it emits a smoke trail
    else if (HighVelocity && HitWall && !IsStationary)
    {
      //Same procedure as above, only less smoke and a lot smaller
      int count = 1;

      GameObject[] SmokeTrail = new GameObject[count];

      //Makes the smoke trail
      for (int i = 0; i < count; i++)
      {

        //Random direction
        Vector2 RandomUp = new Vector2(Randomizer.Next(-spread, spread), Randomizer.Next(-spread, spread));

        //Random cloud
        switch (Randomizer.Next(1, 5))
        {
          case 1:
            SmokeTrail[i] = Instantiate(Smoke1);
            break;
          case 2:
            SmokeTrail[i] = Instantiate(Smoke2);
            break;
          case 3:
            SmokeTrail[i] = Instantiate(Smoke3);
            break;
          case 4:
            SmokeTrail[i] = Instantiate(Smoke4);
            break;
        }
        //Scaling down the smoke and adding random force to the established direction
        SmokeTrail[i].transform.position = transform.position;
        SmokeTrail[i].transform.localScale = new Vector3(0.05f, 0.05f);
        SmokeTrail[i].GetComponent<Rigidbody2D>().AddForce(RandomUp.normalized * Randomizer.Next(5, 15));
      }
    }
  }

  public void EnableFireExtHitbox() //Turns on the Hitbox after the player picked it up
  {
    transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
  }

  void OnCollisionEnter2D(Collision2D CollisionWith) //Detects collision with walls
  {
    //Checks if the velocity was high enough to trigger the smoke
    veloCheck = (FireExtVelocity > 7) ? true : false;

    if (veloCheck)
    {
      HighVelocity = true;
    }

    HitWall = (CollisionWith.gameObject.tag.ToUpper() == "PORTALWALL") || (CollisionWith.gameObject.tag.ToUpper() == "WALL") ? true : false;
  }

}
