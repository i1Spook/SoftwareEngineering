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
    public float threshold = 4; //private?
    public float startTimeBetweenShots = 0.05f;
    public float VisionInLightLenght = 100;
    public float VisionInDarknessLength = 3; //Length Private?
    public float SpinupTime = 1;

    // Use this for initialization
    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(Startangle, new Vector3(0, 0, 1));
        startAngle = new Vector2(-transform.right.x, -transform.right.y);
        Debug.Log(startAngle);
        if(startAngle.x > 0) { StartAngleIsRight = true; }
        else { StartAngleIsRight = false; }
        Debug.Log(StartAngleIsRight);
    }
    Vector2 startAngle;
    void Update()
    {
        updateValues();

        if (GetComponent<Renderer>().isVisible) // Raycast only if onscreen
        {
            RaycastVision();
            
        }

        if (TargetSighted)
        {
            rotateToTargetPosition(currentAngleToTarget);
           
            if (targetLocked(rotationtotarget))
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
    
    bool PlayerInLight; //Placeholder Detectionrays
    float rotationtotarget;
    float currentAngleToTarget;
    Vector2 Turrettotargetdirection;
    Vector2 TurretXAxisDirection;
    ContactPoint2D Contact;

    void updateValues()
    {
        PlayerInLight = ItemScript.InLight; // Detect Skript no longer needed
        Turrettotargetdirection = (Target.position - this.transform.position).normalized;
        TurretXAxisDirection = new Vector2(-this.transform.right.x, -this.transform.right.y);
        rotationtotarget = AngleBetweenVector2(Turrettotargetdirection, TurretXAxisDirection);                                                                                                                                                                                                                              //rotationtotarget = (rotationtotarget - (0.25f * Mathf.PI)) % Mathf.PI; //Doesnt work? DOESNT WORK !
        currentAngleToTarget = AngleBetweenVector2(Turrettotargetdirection, new Vector2(-1, 0));

        Debug.Log(rotationtotarget + "ROtTOTArget");
        Debug.Log(currentAngleToTarget + "Currangletotarget");

    }

    //bool detected() { }

    float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
        float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
        return Vector2.Angle(vec1, vec2) * sign;
    }

    void RaycastVision()
    {
        origin = new Vector2(Firepoint.transform.position.x, Firepoint.transform.position.y);
        Target2d = new Vector2(Target.transform.position.x, Target.transform.position.y);
        direction = Target2d - origin; //NEUE IDEE
        //Debug.Log(direction);
        //Raylength Through LightDetection
        if (PlayerInLight)
        {
            reallength = VisionInLightLenght;
        }
        else
        {
            reallength = VisionInDarknessLength;
        }
        //ray2D = new Ray2D(new Vector2(transform.position.x, transform.position.y), Vector3.down); //Not the Firepoint! ??Maybe a Turretcam Ask Arthur
        //hit = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), Vector3.right*reallength);
        RaycastHit2D DetectionRay = Physics2D.Raycast(origin, direction, reallength);
        //Debug.Log(reallength);
        if (DetectionRay.collider != null)
        {
            if (DetectionRay.collider.tag == "Player")
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
    bool TargetSighted = false;

    void rotateToTargetPosition(float AngleToTarget)
    {
        if (!StartAngleIsRight)
        {
            if (rotationtotarget > 0 && Mathf.Abs(AngleToTarget)<AngleCap)
            {
                transform.Rotate(0, 0, -RotationSpeed);
            }
            if (rotationtotarget < 0 && Mathf.Abs(AngleToTarget) < AngleCap)
            {
                transform.Rotate(0, 0, RotationSpeed);
            }
        }
        if (StartAngleIsRight)
        {
            if (rotationtotarget > 0 && Mathf.Abs(AngleToTarget) > AngleCap)
            {
                transform.Rotate(0, 0, -RotationSpeed);
            }
            if (rotationtotarget < 0 && Mathf.Abs(AngleToTarget) > AngleCap)
            {
                transform.Rotate(0, 0, RotationSpeed);
            }
        }

    }
    
    bool targetLocked(float rotationToTarget)
    {
        if (Mathf.Abs(rotationToTarget) < threshold) { return true; }
        else { return false; }
    }
    void fireProjectile()
    {
        if (spinupTime <= 0)
        {
            if (timeBetweenShots <= 0)
            {
                Instantiate(Projectile, this.transform.position, this.transform.rotation);
                timeBetweenShots = startTimeBetweenShots;
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
        angleToStartingVector = AngleBetweenVector2(TurretXAxisDirection, startAngle);
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
