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
    public Obstacle_Drone droneTemplate;
    public Obstacle_Spikes spikesTemplate;
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

        //      Falling Ground
        GroundFall fall = go.GetComponent<GroundFall>();
        if (fall != null)
        {
            Destroy(fall);
            fall = null;
        }
        if (Random.Range(0, 5) == 0)
        {            
            fall = go.AddComponent<GroundFall>();
            fall.fallSpeed = Random.Range(0.5f, 1.5f);
        }


        //      Random obstacles box
        int obstacleNumBox1 = Random.Range(0, 2);
        for (int i = 0; i < obstacleNumBox1; i++)
        {
            // Tao obstacles left
            GameObject box = Instantiate(boxTemplate.gameObject);

            // Position Random
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2;
            float left = go.transform.position.x - halfWidth + 10;
            float leftHalf = left / 2;
            float x = Random.Range(left, left + leftHalf);

            Vector2 boxPos1 = new Vector2(x, y);
            box.transform.position = boxPos1;

            if (fall != null)
            {
                Obstacle_Box itemBox = box.GetComponent<Obstacle_Box>();
                fall.listBox.Add(itemBox);
            }
        }
        int obstacleNumBox2 = Random.Range(0, 2);
        for (int i = 0; i < obstacleNumBox2; i++)
        {
            // Tao obstacles right
            GameObject box = Instantiate(boxTemplate.gameObject);

            // Position Random
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2;
            float right = go.transform.position.x + halfWidth - 10;
            float rightHalf = right / 2;
            float x = Random.Range(right - rightHalf, right);

            Vector2 boxPos2 = new Vector2(x, y);
            box.transform.position = boxPos2;

            if (fall != null)
            {
                Obstacle_Box itemBox = box.GetComponent<Obstacle_Box>();
                fall.listBox.Add(itemBox);
            }
        }

        //      Random obstacles drone
        int obstacleNumDrone = Random.Range(0, 2);
        for (int i = 0; i < obstacleNumDrone; i++)
        {
            // Tao obstacles
            GameObject drone = Instantiate(droneTemplate.gameObject);

            // Position Random
            float y = goGround.groundHeight + 5;
            float halfWidth = goCollider.size.x / 2;
            float left = go.transform.position.x - halfWidth + 5;
            float right = go.transform.position.x + halfWidth - 5;
            float x = Random.Range(left, right);

            Vector2 boxPos = new Vector2(x, y);
            drone.transform.position = boxPos;
        }

        //      Random obstacles spikes
        int obstacleNumSpikes = Random.Range(0, 2);
        for (int i = 0; i < obstacleNumSpikes; i++)
        {
            // Tao obstacles
            GameObject drone = Instantiate(spikesTemplate.gameObject);

            // Position Random
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2;
            float left = go.transform.position.x - halfWidth + 17;
            float right = go.transform.position.x + halfWidth - 17;
            float x = Random.Range(left, right);

            Vector2 boxPos = new Vector2(x, y);
            drone.transform.position = boxPos;
        }
    }

}
