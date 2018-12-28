using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float speed;
    public float distance;

    public bool movingRight = true;
    public bool movingUp = false;
    public bool VerticalMove = false;
    //This is a end loop bool
    public Transform groundDetection;
    public bool groundInfo;
    public bool sideInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        RaycastHit2D sideInfo = Physics2D.Raycast(groundDetection.position, Vector2.zero, distance);


        Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

        Debug.Log(groundInfo.collider);



        if (groundInfo.collider == false &&sideInfo.collider == false && VerticalMove == false)
        {
            MovingLeftRight();
            

        }

        if (groundInfo.collider == true && sideInfo.collider == true && VerticalMove == false)
        {

            MovingUpDown();
            //Debug.Log("hit3");
          }

        if (groundInfo.collider == false && sideInfo.collider == false && VerticalMove == true)
        {
            Debug.Log("test");
            MovingOnUp();

        }


    }

    public void MovingLeftRight ()
    {
        if (movingRight == true)
        {
            //Debug.Log("hit2");
            transform.eulerAngles = new Vector3(0, -180, 0);

            movingRight = false;
            return;

        }
        else
        {
            //Debug.Log("hit");
            transform.eulerAngles = new Vector3(0, 0, 0);

            movingRight = true;
            return;

        }

    }
    //This is where the script no longer adjusts to the terrian.  Can you take a look?
    public void MovingUpDown ()
    {
        if (movingRight == true && VerticalMove == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            //movingUp = true;
            Debug.Log("Up");
            VerticalMove = true;
            //MovingOnUp();
            return;
        }

        if (movingRight == false && VerticalMove == false)
        {
            transform.eulerAngles = new Vector3(0, 180, 90);
           // movingUp = true;
            VerticalMove = true;
            //MovingOnUp();
            return;

        }
    }


    public void MovingOnUp()
    {
        //RaycastHit2D groundInfo2 = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        //RaycastHit2D sideInfo2 = Physics2D.Raycast(groundDetection.position, Vector2.zero, distance);

        
       
          Debug.Log("test2");

           transform.eulerAngles = new Vector3(0, 0, 0);
            VerticalMove = false;
           // movingRight = false;
           // movingUp = false;

      

    }

}
