using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTSCRIPT : MonoBehaviour {

    public static bool Detected { get; set; }
    bool up, down, left, right, ChgnSprite;
    public Sprite UND, PDT;
    private SpriteRenderer SR;
    // Use this for initialization
    void Start()
    {
        SR = this.GetComponent<SpriteRenderer>();
        //kein Vorgegebener Sprite Sprite über SR einladen

        if (SR.sprite == null) { SR.sprite = UND; Detected = false; }
    }

    // Update is called once per frame
    void Update()
    {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        ChgnSprite = Input.GetKeyDown(KeyCode.Space);
        if (up)
        {
            this.transform.Translate(new Vector3(0, 1));
        }
        if (down)
        {
            this.transform.Translate(new Vector3(0, -1));
        }
        if (left)
        {
            this.transform.Translate(new Vector3(-1, 0));
        }
        if (right)
        {
            this.transform.Translate(new Vector3(1, 0));
        }
        if(ChgnSprite)
        {
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        if (SR.sprite == UND) { SR.sprite = PDT; Detected = true; }
        else if (SR.sprite == PDT) { SR.sprite = UND; Detected = false; }
    }
}