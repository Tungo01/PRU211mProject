using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float maxSpeed = 100;
    public Vector2 velocity;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0f;
    public float jumpThreshold = 1;
    public bool isDeaded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Vector2 pos = transform.position;
        if (isDeaded)
        {
            return;
        }
        if (pos.y < -20)
        {
            isDeaded = true;
        }
        float groundDistance = Mathf.Abs(pos.y-groundHeight); 
        if (isGrounded || groundDistance <= jumpThreshold)
        {
            if (Input.touchCount>0)
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump= true;
                holdJumpTimer = 0f;
            }
        }
        if (Input.touchCount <= 0)
        {
            isHoldingJump= false;
        }
        
        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.deltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.deltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.deltaTime;

            }
            ////
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.deltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if(pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;

                    }
                    
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
            Vector2 wall = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wall, Vector2.right, velocity.x * Time.deltaTime);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if(ground!= null)
                {
                    if(pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }
            }
            ////
            
        }

        distance += velocity.x * Time.deltaTime; // = score
        if (isGrounded)
        {   float velocityRatio = velocity.x / maxSpeed; // speed cang nhanh cang kho tang toc
            acceleration = maxAcceleration * (1- velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.deltaTime;
            
            if (velocity.x >= maxSpeed)
            {
                velocity.x = maxSpeed;
            }

            ////
            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.deltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider == null)
	    {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);

            ////
        }

	    transform.position = pos;



        ////    Obstacles tuong tac voi Player
        Vector2 obstOrigin = new Vector2(pos.x, pos.y);

        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.deltaTime);
        if (obstHitX.collider != null)
        {
            Obstacle_Drone obstacle_Drone = obstHitX.collider.GetComponent<Obstacle_Drone>();
            Obstacle_Spikes obstacle_Spikes = obstHitX.collider.GetComponent<Obstacle_Spikes>();

            if (obstacle_Drone != null)
            {
                hitObstacle_Drone(obstacle_Drone);
            }
            if (obstacle_Spikes != null)
            {
                hitObstacle_Spikes(obstacle_Spikes);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.deltaTime);
        if (obstHitY.collider != null)
        {
            //Obstacle_Box obstacle_Box = obstHitY.collider.GetComponent<Obstacle_Box>();
            Obstacle_Drone obstacle_Drone = obstHitY.collider.GetComponent<Obstacle_Drone>();
            Obstacle_Spikes obstacle_Spikes = obstHitY.collider.GetComponent<Obstacle_Spikes>();
            if (obstacle_Drone != null)
            {
                hitObstacle_Drone(obstacle_Drone);
            }
            if (obstacle_Spikes != null)
            {
                hitObstacle_Spikes(obstacle_Spikes);
            }
        }

    }


    void hitObstacle_Drone(Obstacle_Drone obstacle_Drone)
    {
        Destroy(obstacle_Drone.gameObject);
        velocity.x *= 0.7f;
    }
    void hitObstacle_Spikes(Obstacle_Spikes obstacle_Spikes)
    {
        Destroy(obstacle_Spikes.gameObject);
        velocity.x *= 0.7f;
    }
    
}
