using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkriptFinal : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Target;
    public float RotationSpeed;
    public float Angle_min;
    public float Angle_max;
    public float Startangle;
    public float threshold = 4; //private?
    public float startTimeBetweenShots = 0.05f;

    public bool Active;
    // Use this for initialization
    void Start()
    {
        Active = true;
        transform.rotation = Quaternion.AngleAxis(Startangle, new Vector3(0, 0, 1));
    }
    void Update()
    {   if (Active)
        {
            updateValues();
            if (detected)
            {
                rotateToTargetPosition();
                if (targetLocked())
                {
                    fireProjectile();
                }
            }
            else
            {
                Idlemode();
            }
        }
    }
    
    bool detected; //Placeholder Detectionrays
    float rotationtotarget;
    float currentAngleToTarget;
    Vector2 Turrettotargetdirection;
    Vector2 TurretXAxisDirection;
    ContactPoint2D Contact;

    void updateValues()
    {
        detected = DTSCRIPT.Detected; //Placeholder DETECTIONRAYS
        Turrettotargetdirection = (Target.position - this.transform.position).normalized;
        TurretXAxisDirection = new Vector2(-this.transform.right.x, -this.transform.right.y);
        rotationtotarget = AngleBetweenVector2(Turrettotargetdirection, TurretXAxisDirection);                                                                                                                                                                                                                              //rotationtotarget = (rotationtotarget - (0.25f * Mathf.PI)) % Mathf.PI; //Doesnt work? DOESNT WORK !
        currentAngleToTarget = AngleBetweenVector2(Turrettotargetdirection, new Vector2(-1, 0));
    }

    //bool detected() { }

    float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
        float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
        return Vector2.Angle(vec1, vec2) * sign;
    }

    void rotateToTargetPosition()
    {
        if (rotationtotarget > 0 && currentAngleToTarget > Angle_min && currentAngleToTarget < Angle_max)
        {
            transform.Rotate(0, 0, -RotationSpeed);
        }
        if (rotationtotarget < 0 && currentAngleToTarget > Angle_min && currentAngleToTarget < Angle_max)
        {
            transform.Rotate(0, 0, RotationSpeed);
        }
    }
    
    bool targetLocked()
    {
        if (Mathf.Abs(rotationtotarget) < threshold) { return true; }
        else { return false; }
    }
    void fireProjectile()
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
    float timeBetweenShots;

    void Idlemode()
    {
        if (switchdirection)
        {
            transform.Rotate(0, 0, -RotationSpeed/5);
        }
        if (!switchdirection)
        {
            transform.Rotate(0, 0, RotationSpeed/5);
        }
        if(this.transform.rotation.z*Mathf.Rad2Deg*4 < Angle_min || this.transform.rotation.z * Mathf.Rad2Deg*4 > Angle_max)
        {
            switchdirection = !switchdirection;
        }
    }
    bool switchdirection;
}
