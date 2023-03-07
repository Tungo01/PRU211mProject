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
            
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
                
            }
        }
        transform.position = pos;

        distance += velocity.x * Time.deltaTime; // = score
        if (isGrounded)
        {   float velocityRatio = velocity.x / maxSpeed; // speed cang nhanh cang kho tang toc
            acceleration = maxAcceleration * (1- velocityRatio); 

            velocity.x += acceleration * Time.deltaTime;
            
            if (velocity.x >= maxSpeed)
            {
                velocity.x = maxSpeed;
            }
        }
    }
    
}
