using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShot : MonoBehaviour {

    public GameObject BluePortal;

    public bool Hit = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter2D(Collision2D CollisionWith)
    {
        Hit = true;
        Quaternion Rotation =  CollisionWith.gameObject.transform.rotation;
        //GameObject BluePortalCopy = Instantiate(BluePortal, transform.position ,Rotation);
        BluePortal.transform.position = transform.position;
        BluePortal.transform.rotation = Rotation * Quaternion.Euler(0,0,180f);

        Destroy(gameObject);
    }
}
