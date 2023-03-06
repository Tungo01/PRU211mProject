using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump= true;
                holdJumpTimer = 0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
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
    }
    
}
