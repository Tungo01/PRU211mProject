using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D collider;
    bool didGenerateGround = false;

    public Obstacle_Box boxTemplate;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }
    
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.deltaTime;
        groundRight = transform.position.x + (collider.size.x / 2);
        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }

        transform.position = pos;
    }

    void generateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;
        
        pos.y = transform.position.y;
        
       pos.x = screenRight + 30;

        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);

        // Random obstacles
        int obstacleNum = Random.Range(0, 3);
        for (int i = 0; i < obstacleNum; i++)
        {
            // Tao obstacles
            GameObject box = Instantiate(boxTemplate.gameObject);

            // Position Random
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2;
            float left = go.transform.position.x - halfWidth + 1;
            float right = go.transform.position.x + halfWidth - 1;
            float x = Random.Range(left, right);

            Vector2 boxPos = new Vector2(x, y);
            box.transform.position = boxPos;
        }
    }

}
