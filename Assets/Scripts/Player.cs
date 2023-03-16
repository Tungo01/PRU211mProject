using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float gravity;
    public float acceleration = 10;
    public float maxAcceleration = 5;
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
    public LayerMask GroundLayerMask;
    public LayerMask ObstacleLayerMask;
    GroundFall groundFall; 
    public AudioSource JumpSFX; 
    
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
                JumpSFX.Play();
                isGrounded = false;
                animator.SetBool("Jump", !isGrounded);
                velocity.y = jumpVelocity;
                isHoldingJump= true;
                holdJumpTimer = 0f;

                if (groundFall != null)
                {
                    groundFall.player = null;
                    groundFall = null;
                }
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
            if (groundFall != null)
            {
                rayDistance = -groundFall.fallSpeed * Time.deltaTime;
            }
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance,GroundLayerMask);
            if(hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if(pos.y >= ground.groundHeight -0.5f)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight+0.5f;
                        velocity.y = 0;
                        isGrounded = true;

                    }
                    
                    groundFall = ground.GetComponent<GroundFall>();
                    if (groundFall != null)
                    {
                        groundFall.player = this;
                    }
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);


            //      Va cham dam vao ground
            Vector2 wall = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wall, Vector2.right, velocity.x * Time.deltaTime,GroundLayerMask);
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
            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y-0.5f);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.deltaTime;
            
            if (groundFall != null)
            {
                rayDistance = -groundFall.fallSpeed  * Time.deltaTime;
            }
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, GroundLayerMask);
            if(hit2D.collider == null)
	        {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);

            ////
        }

	    transform.position = pos;

    }

    
}
