using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkript : MonoBehaviour
{
    public float speed = 50f;
    void Update()
    {
        transform.position += -this.transform.right.normalized * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     Destroy(this.gameObject);
    }
}
