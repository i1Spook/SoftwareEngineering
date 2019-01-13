using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVanish : MonoBehaviour
{

  SpriteRenderer smokeRenderer;

  System.Random Randomizer;
  void Start()
  {
    smokeRenderer = GetComponent<SpriteRenderer>();

    Randomizer = new System.Random();

    StartFading();
  }

  IEnumerator FadeOut()
  {
    //Fades out the smoke

    //The fade out depends on the size of the smoke
    if (this.transform.localScale.x > 0.15f)
    {
      int fraction = Randomizer.Next(1, 100);

      yield return new WaitForSeconds((float)(10 + fraction / 100));
    }
    else
    {
      yield return new WaitForSeconds(2f);
    }

    //Fades the sprite
    for (float f = 1f; f >= -0.05f; f -= 0.05f)
    {
      Color spriteColor = smokeRenderer.material.color;

      spriteColor.a = f;

      smokeRenderer.material.color = spriteColor;

      yield return new WaitForSeconds(0.05f);
    }

    //Destroys the object when it faded out
    Destroy(this.gameObject);
  }

  public void StartFading()
  {
    StartCoroutine("FadeOut");
  }
}
