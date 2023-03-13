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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Vector2 pos = transform.position;
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
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    velocity.y = 0;
                    isGrounded = true;
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
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
            Obstacle_Box obstacle_Box = obstHitX.collider.GetComponent<Obstacle_Box>();
            if (obstacle_Box != null)
            {
                hitObstacle_Box(obstacle_Box);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.deltaTime);
        if (obstHitY.collider != null)
        {
            Obstacle_Box obstacle_Box = obstHitY.collider.GetComponent<Obstacle_Box>();
            if (obstacle_Box != null)
            {
                hitObstacle_Box(obstacle_Box);
            }
        }

    }


    ////    Destroy Obstacle khi va cham
    void hitObstacle_Box(Obstacle_Box obstacle_Box)
    {
        Destroy(obstacle_Box.gameObject);
        velocity.x *= 0.7f;
    }
    
}
