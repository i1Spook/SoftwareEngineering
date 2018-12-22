using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiermovement : MonoBehaviour {
    public bool facingRight;
    public GameObject[] Patrulie;
    public float threshold = 0.4f;
    public float Speed = 1;
    public float wait = 7;

	// Use this for initialization
	void Start () {
        Flip();
        currentTime = wait;
        CurrentNumber = 0;
        //Debug.Log(transform.right);
	}
	
	// Update is called once per frame
	void Update () {

		if(Patrulie != null)
        {
            GotoPatrullie();
        }
	}
    
    void GotoPatrullie()
    {
        
        //foreach (GameObject item in Patrulie) //Problem Goes trhough Both must seperate from Point to Point D: Manuel Foreach?? !
        item = Patrulie[CurrentNumber];
        
            Gotoy = item.transform.position.x;
            Diff = Gotoy - item.transform.position.x;
            //Debug.Log(Gotoy + "   Gotoy");
            //Debug.Log(Diff + "Diff");
            if (Gotoy - transform.position.x > threshold/2)
            {
            if (!facingRight)
            {
                Flip();
            }
                transform.position += transform.right * Time.deltaTime * Speed;
            }
            else if (Gotoy - transform.position.x < -threshold/2)
            {
            if (facingRight)
            {
                Flip();
            }
            transform.position -= transform.right * Time.deltaTime * Speed;
            }

        if (Mathf.Abs(Gotoy - transform.position.x) < threshold)
        {
            currentTime = currentTime - Time.deltaTime;
            if(currentTime <= 0)
            {
                Next = true;
            }
        }
        else
        {
            currentTime = wait;
            Next = false;
        }
        if (Next)
        {
            CurrentNumber++;
        }


        //while (Mathf.Abs(Gotoy - transform.position.x) < threshold)
        //{
        //    Debug.Log("while?");
        //    if (Gotoy - transform.position.x > 0)
        //    {
        //        transform.position += transform.right*Time.deltaTime*Speed;
        //    }
        //    else if (Gotoy - transform.position.x < 0)
        //    {
        //        transform.position -= transform.right*Time.deltaTime*Speed;
        //    }
        //}

        if (CurrentNumber == Patrulie.Length) { CurrentNumber = 0; }
    }
    float Gotoy;
    float Diff;
    bool Next = false; // Does not work
    int CurrentNumber;
    GameObject item;
    float currentTime;


    public void Flip()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }
}
