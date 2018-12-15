using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExt : MonoBehaviour
{

    public bool HighVelocity;
    public bool HitWall;
    public bool IsStationary;
    bool veloCheck;

    bool IsUsed;

    const int spread = 10;

    public GameObject Smoke1;
    public GameObject Smoke2;
    public GameObject Smoke3;
    public GameObject Smoke4;



    public float velo;

    System.Random Randomizer = new System.Random();

    // Use this for initialization
    void Start()
    {
        IsUsed = false;
        HighVelocity = false;
        HitWall = false;
        IsStationary = false;
        veloCheck = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velo = GetComponent<Rigidbody2D>().velocity.magnitude;
        IsStationary = (velo < 1) ? true : false;

        if (HighVelocity && HitWall && IsStationary && !IsUsed)
        {

            GameObject[] SmokeClone = new GameObject[32];

            for (int i = 0; i < 32; i++)
            {
                Vector2 RandomUp = new Vector2(Randomizer.Next(-spread, spread), Randomizer.Next(-spread, spread));

                switch (Randomizer.Next(1, 5))
                {
                    case 1:
                        SmokeClone[i] = Instantiate(Smoke1);
                        break;
                    case 2:
                        SmokeClone[i] = Instantiate(Smoke2);
                        break;
                    case 3:
                        SmokeClone[i] = Instantiate(Smoke3);
                        break;
                    case 4:
                        SmokeClone[i] = Instantiate(Smoke4);
                        break;
                }

                SmokeClone[i].transform.position = transform.position;

                SmokeClone[i].GetComponent<Rigidbody2D>().AddForce(RandomUp.normalized * Randomizer.Next(5, 20));

            }
            HighVelocity = false;
            IsUsed = true;
        }
        else if (HighVelocity && HitWall && !IsStationary /*&& !IsUsed*/)
        {
            int count = 1;
            GameObject[] SmokeTrail = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                Vector2 RandomUp = new Vector2(Randomizer.Next(-spread, spread), Randomizer.Next(-spread, spread));

                switch (Randomizer.Next(1, 5))
                {
                    case 1:
                        SmokeTrail[i] = Instantiate(Smoke1);
                        break;
                    case 2:
                        SmokeTrail[i] = Instantiate(Smoke2);
                        break;
                    case 3:
                        SmokeTrail[i] = Instantiate(Smoke3);
                        break;
                    case 4:
                        SmokeTrail[i] = Instantiate(Smoke4);
                        break;
                }

                SmokeTrail[i].transform.position = transform.position;
                SmokeTrail[i].transform.localScale = new Vector3(0.05f, 0.05f);
                SmokeTrail[i].GetComponent<Rigidbody2D>().AddForce(RandomUp.normalized * Randomizer.Next(5, 15));

            }
        }
    }

    void OnCollisionEnter2D(Collision2D CollisionWith)
    {
        veloCheck = (velo > 7) ? true : false;
        if (veloCheck)
        { HighVelocity = true; }
        HitWall = (CollisionWith.gameObject.tag.ToUpper() == "PORTALWALL") || (CollisionWith.gameObject.tag.ToUpper() == "WALL") ? true : false;
    }

}
