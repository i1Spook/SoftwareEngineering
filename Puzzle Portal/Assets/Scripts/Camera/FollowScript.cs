using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour
{

  public GameObject player;       //Public variable to store a reference to the player game object

  public float MaxX;
  public float MinX;
  public float MaxY;
  public float MinY;

  private Vector3 offset;         //Private variable to store the offset distance between the player and camera
  private Vector3 temp;
  // Use this for initialization
  void Start()
  {
    //Calculate and store the offset value by getting the distance between the player's position and camera's position.
    offset = transform.position - player.transform.position;
  }

  // LateUpdate is called after Update each frame
  void LateUpdate()
  {
    // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
    temp = player.transform.position + offset;

    if (temp.x > MaxX)
    {
      temp.x = MaxX ;
    }
    else if (temp.x < MinX)
    {
      temp.x = MinX;
    }

    if (temp.y > MaxY)
    {
      temp.y = MaxY;
    }
    else if (temp.y < MinY)
    {
      temp.y = MinY;
    }
    transform.position = temp;
  }

}
