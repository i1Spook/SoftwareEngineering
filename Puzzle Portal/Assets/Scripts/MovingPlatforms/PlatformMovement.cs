using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
  // Moves the platform from posA to posB and back
  // Sets the player as child for better performance

  private Vector3 posA;

  private Vector3 posB;

  private Vector3 nextPos;

  [SerializeField]
  private float speed;

  [SerializeField]
  private Transform childTransform;

  [SerializeField]
  private Transform transformB;

  void Start()
  {
    posA = childTransform.localPosition;

    posB = transformB.localPosition;

    nextPos = posB;
  }

  void Update()
  {
    if (childTransform.localPosition != transformB.localPosition)
    {
      Move();
    }
  }

  private void Move()
  {
    childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

    if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
    {
      ChangeDestination();
    }
  }

  private void ChangeDestination()
  {
    nextPos = nextPos != posA ? posA : posB;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.layer = 8;

      other.transform.SetParent(childTransform);
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    other.transform.SetParent(null);
  }
}
