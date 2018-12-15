using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkript : MonoBehaviour
{
    public float speed;

    void Start()
    {
        speed = 15f;
        Destroy(gameObject, 5);
    }
    void FixedUpdate()
    {
        transform.position += -this.transform.right.normalized * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     Destroy(this.gameObject);
    }
    void OnCollisionEnter2D()
    {
        Destroy(this.gameObject);
    }
}
