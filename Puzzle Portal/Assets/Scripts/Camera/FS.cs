using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS : MonoBehaviour {

    public GameObject player;
    public float offY;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (new Vector3(player.transform.position.x, offY, 5)) + (new Vector3(offset.x, offY, offset.z));
    }
}
