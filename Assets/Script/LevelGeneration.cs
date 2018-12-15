﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms;
    public GameObject Player;
    public GameObject SpacePod;
    public Transform[] spriteStart;
    public Transform[] spriteEnd;

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
   public bool stopGeneration;

    public LayerMask room;

    private int downCounter;

    private void Start()
    {
        //Starting position with room generation
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);

        //Starting position with character start
       // int randStartingSpritePos = Random.Range(0, spriteStart.Length);
       // transform.position = spriteStart[randStartingSpritePos].position;
       // Instantiate(Player, transform.position, Quaternion.identity);


    }
    
    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {

            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

    }

    private void Move()
    {

        if (direction == 1 || direction == 2) //Move Right
        {
            if(transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                } else if (direction == 4){
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }

        }

        else if (direction == 3 || direction == 4)//Move Left
        {
        

            if(transform.position.x >minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }

        else if (direction == 5) //Move Down
        {
            downCounter++;
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else{
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }

                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                direction = Random.Range(1, 6);
                

            }
            else
            {
                stopGeneration = true;

                CharacterStart();

            }

        }

        
       

    }

    private void CharacterStart ()
    {
        int randStartingSpritePos = Random.Range(0, spriteStart.Length);
        transform.position = spriteStart[randStartingSpritePos].position;
        Instantiate(Player, transform.position, Quaternion.identity);
        Instantiate(SpacePod, transform.position, Quaternion.identity);
    }
}
