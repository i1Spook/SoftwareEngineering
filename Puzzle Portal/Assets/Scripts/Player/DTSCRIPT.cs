using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTSCRIPT : MonoBehaviour {

    public static bool Detected { get; set; }
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        Detected = ItemScript.InLight;
    }
}