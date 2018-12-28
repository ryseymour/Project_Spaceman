using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    //Jetpack vars
    public float jetPower;
    public float jetFuel; //this is the max amount of "fuel"
    public float currentFuel;
    float thrust;
    float jetTimer;
    float jetTap;
    public float tapDelay; //amount of time allowed between taps to enable jetpack
    bool jetOn;
    bool refuel;
    public float refuelDelay;
    public float maxYvel;

    Rigidbody2D rb; 

	// Use this for initialization
	void Start () {
        jetTap = Time.time;
        jetOn = false;
        rb = GetComponent<Rigidbody2D>();
        thrust = jetPower / 4;
        currentFuel = jetFuel;
        refuel = false;
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }
        JetPack(); 


    }

    void FixedUpdate ()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }

    void JetPack(){

        if(Input.GetButtonDown("Jump")){

            if((Time.time - jetTap) < tapDelay){
                Debug.Log("double tap");
                jetOn = true;
            }

            jetTap = Time.time;
        }

        if(Input.GetButton("Jump") && jetOn && currentFuel > 0){
            refuel = false;
            if(thrust < jetPower){
                thrust += 0.5f * Time.deltaTime;
            }
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y + thrust, -0.1f, maxYvel));
            currentFuel -= 1 * Time.deltaTime;
        }

        if(Input.GetButtonUp("Jump")){
            jetOn = false;
            thrust = jetPower/4;
            StartCoroutine("RefuelDelay");
        }

        

        if(currentFuel < jetFuel && refuel){
            currentFuel += 1 * Time.deltaTime;
        }

    }

    IEnumerator RefuelDelay(){
        yield return new WaitForSeconds(refuelDelay);
        refuel = true;
    }


}
