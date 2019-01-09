using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiermovement : MonoBehaviour
{
  public bool FacingRight;
  public GameObject[] Patrulie;
  public float Threshold = 0.4f;
  public float Speed = 1;
  public float Wait = 7;
  public GameObject Arm;

  void Start()
  {
    Flip();
    currentTime = Wait;
    currentNumber = 0;
  }

  void Update()
  {

    if (Patrulie != null && !Arm.GetComponent<SoldierArm>().TargetSighted)
    {
      GotoPatrullie();
    }
  }

  void GotoPatrullie()
  {
    item = Patrulie[currentNumber];
    gotoy = item.transform.position.x;
    diff = gotoy - item.transform.position.x;

    if (gotoy - transform.position.x > Threshold / 2)
    {
      if (!FacingRight)
      {
        Flip();
      }
      transform.position += transform.right * Time.deltaTime * Speed;
    }
    else if (gotoy - transform.position.x < -Threshold / 2)
    {
      if (FacingRight)
      {
        Flip();
      }
      transform.position -= transform.right * Time.deltaTime * Speed;
    }

    if (Mathf.Abs(gotoy - transform.position.x) < Threshold)
    {
      currentTime = currentTime - Time.deltaTime;
      if (currentTime <= 0)
      {
        next = true;
      }
    }
    else
    {
      currentTime = Wait;
      next = false;
    }
    if (next)
    {
      currentNumber++;
    }

    if (currentNumber == Patrulie.Length) { currentNumber = 0; }
  }
  float gotoy;
  float diff;
  bool next = false;
  int currentNumber;
  GameObject item;
  float currentTime;


  public void Flip()
  {
    FacingRight = !FacingRight;
    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
  }
}
