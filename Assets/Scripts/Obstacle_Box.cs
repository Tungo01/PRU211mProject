using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle_Box : MonoBehaviour
{
    Player player;
    GameObject boxTemplate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        // Van toc cua obstacles
        pos.x -= player.velocity.x * Time.deltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        // Random obstacles
        int obstacleNum = Random.Range(0, 3);
        for (int i = 0; i < obstacleNum; i++)
        {
            // Tao obstacles
            GameObject box = Instantiate(boxTemplate.gameObject);

            // Position Random
            //float y = goGround.groundHeight;
            //float halfWidth = goCollider.size.x / 2;
            //float left = go.transform.position.x - halfWidth + 1;
            //float right = go.transform.position.x + halfWidth -1;
            //float x = Random.Range(left, right);

            //Vector2 boxPos = new Vector2(x, y);
            //box.transform.position = boxPos;
        }

        transform.position = pos;
    }
}
