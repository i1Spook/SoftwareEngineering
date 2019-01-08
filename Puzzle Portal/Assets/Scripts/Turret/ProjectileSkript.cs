using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkript : MonoBehaviour
{
  public float Speed;

  void Start()
  {
    Speed = 50f;
    Destroy(gameObject, 5);
  }
  void FixedUpdate()
  {
    transform.position += -this.transform.right.normalized * Speed * Time.deltaTime;
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
