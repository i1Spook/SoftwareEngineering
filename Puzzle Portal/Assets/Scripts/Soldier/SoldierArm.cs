using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierArm : MonoBehaviour
{
  public GameObject Firepoint;
  public GameObject Projectile;
  public Transform Target;
  public float startTimeBetweenShots = 0.05f;
  public float VisionInLightLenght = 20;
  public float VisionInDarknessLength = 3;
  bool FacingLeftBody;
  public GameObject SoldierBody;
  public float Reaktion = 1;
  public bool TargetSighted = false;

  void Start()
  {
    timeBetweenShots = startTimeBetweenShots;
    facingLeft = false;
  }

  void Update()
  {
    playerInLight = ItemScript.InLight;
    FacingLeftBody = SoldierBody.GetComponent<Soldiermovement>().facingRight;

    if (FacingLeftBody != facingLeft)
    {
      Flip();
      if (FacingLeftBody)
      {
        transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
      }
      if (!FacingLeftBody)
      {
        transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
      }
    }

    if (GetComponent<Renderer>().isVisible)
    {
      RaycastVision();
    }

    if (TargetSighted)
    {
      RotateToTarget();
      fireProjectile();
    }
    else
    {
      reaktion = Reaktion;
    }
  }
  bool playerInLight;

  void RotateToTarget()
  {
    vectorToTarget = Target.transform.position - transform.position;
    angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
    transform.right = -vectorToTarget.normalized;
    if (Mathf.Abs(angle) > 90 && facingLeft)
    {
      Flip();
    }
    else if (Mathf.Abs(angle) < 90 && !facingLeft)
    {
      Flip();
    }
  }
  float angle;
  Vector3 vectorToTarget;

  void RaycastVision()
  {
    origin = new Vector2(Firepoint.transform.position.x, Firepoint.transform.position.y);
    Target2d = new Vector2(Target.transform.position.x, Target.transform.position.y);
    direction = Target2d - origin;

    if (playerInLight)
    {
      reallength = VisionInLightLenght;
    }
    else
    {
      reallength = VisionInDarknessLength;
    }
    RaycastHit2D DetectionRay = Physics2D.Raycast(origin, direction, reallength);

    if (DetectionRay.collider != null)
    {
      Debug.Log(FacingLeftBody);
      if (DetectionRay.transform.position.x <= transform.position.x && !FacingLeftBody)
      {
        targetInFace = true;
      }
      else if (DetectionRay.transform.position.x > transform.position.x && FacingLeftBody)
      {
        targetInFace = true;
      }
      else
      {
        targetInFace = false;
      }
      if (DetectionRay.collider.tag == "Player" && targetInFace)
      {
        TargetSighted = true;
      }
      else { TargetSighted = false; }
    }
  }
  Vector2 origin;
  Vector2 Target2d;
  Vector2 direction;
  float reallength;
  bool targetInFace = false;


  void fireProjectile()
  {
    if (reaktion <= 0)
    {
      if (timeBetweenShots <= 0)
      {
        Instantiate(Projectile, Firepoint.transform.position, this.transform.rotation);
        timeBetweenShots = startTimeBetweenShots;
      }
      else
      {
        timeBetweenShots -= Time.deltaTime;
      }
    }
    else
    {
      reaktion -= Time.deltaTime;
    }
  }
  float timeBetweenShots;
  float reaktion;

  void Flip()
  {
    facingLeft = !facingLeft;
    GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
  }
  bool facingLeft;
}
