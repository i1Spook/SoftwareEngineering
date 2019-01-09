using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkriptFinal : MonoBehaviour
{
  public GameObject Firepoint;
  public GameObject Projectile;

  public Transform Target;
  public float RotationSpeed;
  public float AngleCap = 90;
  public float IdleCap = 30;
  public float Idle_Speed;

  public bool StartAngleIsRight;

  public float Startangle;
  public float Threshold = 4;
  public float StartTimeBetweenShots = 0.05f;
  public float VisionInLightLenght = 100;
  public float VisionInDarknessLength = 3;
  public float SpinupTime = 1.71f;

  public bool Active;

  void Start()
  {
    Target = GameObject.FindGameObjectWithTag("Player").transform;
    transform.rotation = Quaternion.AngleAxis(Startangle, new Vector3(0, 0, 1));
    startAngle = new Vector2(-transform.right.x, -transform.right.y);
    Debug.Log(startAngle);
    if (startAngle.x > 0) { StartAngleIsRight = true; }
    else { StartAngleIsRight = false; }
    Debug.Log(StartAngleIsRight);
    Active = true;
  }
  Vector2 startAngle;

  void Update()
  {
    if (Active)
    {
      updateValues();

      if (GetComponent<Renderer>().isVisible)
      {
        RaycastVision();
      }

      if (targetSighted)
      {
        rotateToTargetPosition(currentAngleToTarget);
        if (targetLocked(rotationToTarget))
        {
          fireProjectile();
        }
        else
        {
          spinupTime = SpinupTime;
        }
      }
      else
      {
        if (isBack)
        {
          Idlemode();
        }
        else
        {
          ReturnToIdlemode();
        }
      }
    }
  }

  bool playerInLight;
  float rotationToTarget;
  float currentAngleToTarget;
  Vector2 turretToTargetDirection;
  Vector2 turretXAxisDirection;
  ContactPoint2D contact;

  void updateValues()
  {
    playerInLight = ItemScript.InLight;
    turretToTargetDirection = (Target.position - this.transform.position).normalized;
    turretXAxisDirection = new Vector2(-this.transform.right.x, -this.transform.right.y);
    rotationToTarget = AngleBetweenVector2(turretToTargetDirection, turretXAxisDirection);                                                                                                                                                                                                                              //rotationtotarget = (rotationtotarget - (0.25f * Mathf.PI)) % Mathf.PI; //Doesnt work? DOESNT WORK !
    currentAngleToTarget = AngleBetweenVector2(turretToTargetDirection, new Vector2(-1, 0));
  }

  float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
  {
    Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
    float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
    return Vector2.Angle(vec1, vec2) * sign;
  }

  void RaycastVision()
  {
    origin = new Vector2(Firepoint.transform.position.x, Firepoint.transform.position.y);
    target2D = new Vector2(Target.transform.position.x, Target.transform.position.y);
    direction = target2D - origin;

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
      if (DetectionRay.collider.tag == "Player")
      {
        targetSighted = true;
      }
      else { targetSighted = false; }
    }
  }
  Vector2 origin;
  Vector2 target2D;
  Vector2 direction;
  float reallength;
  bool targetSighted = false;

  void rotateToTargetPosition(float AngleToTarget)
  {
    if (!StartAngleIsRight)
    {
      if (rotationToTarget > 0 && Mathf.Abs(AngleToTarget) < AngleCap)
      {
        transform.Rotate(0, 0, -RotationSpeed);
      }
      if (rotationToTarget < 0 && Mathf.Abs(AngleToTarget) < AngleCap)
      {
        transform.Rotate(0, 0, RotationSpeed);
      }
    }
    if (StartAngleIsRight)
    {
      if (rotationToTarget > 0 && Mathf.Abs(AngleToTarget) > AngleCap)
      {
        transform.Rotate(0, 0, -RotationSpeed);
      }
      if (rotationToTarget < 0 && Mathf.Abs(AngleToTarget) > AngleCap)
      {
        transform.Rotate(0, 0, RotationSpeed);
      }
    }
  }

  bool targetLocked(float rotationToTarget)
  {
    if (Mathf.Abs(rotationToTarget) < Threshold)
    {
      return true;
    }
    else { return false; }
  }
  void fireProjectile()
  {
    if (spinupTime <= 0)
    {
      if (timeBetweenShots <= 0)
      {
        Instantiate(Projectile, this.transform.position, this.transform.rotation);
        timeBetweenShots = StartTimeBetweenShots;
      }
      else
      {
        timeBetweenShots -= Time.deltaTime;
      }
    }
    else
    {
      spinupTime -= Time.deltaTime;
    }
  }
  float timeBetweenShots;
  float spinupTime;

  void ReturnToIdlemode()
  {
    angleToStartingVector = AngleBetweenVector2(turretXAxisDirection, startAngle);
    Debug.Log(angleToStartingVector + "AngtoStarting");
    rotateToTargetPosition(angleToStartingVector);
    if (targetLocked(angleToStartingVector))
    {
      isBack = true;
    }
    else
    {
      isBack = false;
    }

  }
  bool isBack;
  float angleToStartingVector;

  void Idlemode()
  {
    if (switchdirection)
    {
      transform.Rotate(0, 0, -Idle_Speed);
    }
    if (!switchdirection)
    {
      transform.Rotate(0, 0, Idle_Speed);
    }
    if (!StartAngleIsRight)
    {
      if (Mathf.Abs(this.transform.rotation.z * Mathf.Rad2Deg * 4) > IdleCap)
      {
        switchdirection = !switchdirection;
      }
    }

    else
    {
      if (Mathf.Abs(this.transform.rotation.z * Mathf.Rad2Deg * 4) < IdleCap)
      {
        switchdirection = !switchdirection;
      }
    }
  }
  bool switchdirection;
}
