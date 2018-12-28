using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVanish : MonoBehaviour {

    SpriteRenderer rend;

    System.Random Randomizer;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Randomizer = new System.Random();
        StartFading();
    }

    IEnumerator FadeOut()
    {
        if (this.transform.localScale.x >0.15f)
        {
            int fraction = Randomizer.Next(1, 100);

            yield return new WaitForSeconds((float) (10+fraction/100));
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        Destroy(this.gameObject);
    }

    public void StartFading()
    {
        StartCoroutine("FadeOut");
    }
}
