using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Spikes : MonoBehaviour
{
    Player player;
    GameObject spikesTemplate;


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
        int obstacleNum = Random.Range(0, 2);
        for (int i = 0; i < obstacleNum; i++)
        {
            // Tao obstacles
            GameObject box = Instantiate(spikesTemplate.gameObject);

            //Vector2 boxPos;
        }

        transform.position = pos;
    }
}
