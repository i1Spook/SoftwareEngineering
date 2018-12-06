using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkript : MonoBehaviour
{
    public float speed;
    public static bool KillAll;

    void Start()
    {
        KillAll = false;
        speed = 50f;
        Destroy(gameObject, 5);
    }
    void FixedUpdate()
    {
        transform.position += -this.transform.right.normalized * speed * Time.deltaTime;
        if (KillAll)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     Destroy(this.gameObject);
    }
}
