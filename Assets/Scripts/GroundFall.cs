using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    bool shouldFall = false;
    public float fallSpeed;

    public Player player;
    public List<Obstacle_Box> listBox = new List<Obstacle_Box>();
    public List<Obstacle_Spikes> listSpikes = new List<Obstacle_Spikes>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFall)
        {
            Vector2 pos = transform.position;
            float falling = fallSpeed * Time.deltaTime;
            pos.y -= falling;

            if (player != null)
            {
                player.groundHeight -= falling;
                Vector2 playerPos = player.transform.position;
                playerPos.y -= falling;

                player.transform.position = playerPos;
            }
            foreach (Obstacle_Box itemBox in listBox)
            {
                if (itemBox != null)
                {
                    Vector2 boxPos = itemBox.transform.position;
                    boxPos.y -= falling;

                    itemBox.transform.position = boxPos;

                }
            }
            foreach (Obstacle_Spikes itemSpikes in listSpikes)
            {
                if (itemSpikes != null)
                {
                    Vector2 spikesPos = itemSpikes.transform.position;
                    spikesPos.y -= falling;

                    itemSpikes.transform.position = spikesPos;

                }
            }
            foreach (Obstacle_Drone itemDrone in listDrone)
            {
                if (itemDrone != null)
                {
                    Vector2 boxPos = itemDrone.transform.position;
                    boxPos.y -= falling;

                    itemDrone.transform.position = boxPos;
                }
            }
            foreach (Obstacle_Spikes itemSpikes in listSpikes)
            {
                if (itemSpikes != null)
                {
                    Vector2 boxPos = itemSpikes.transform.position;
                    boxPos.y -= falling;

                    itemSpikes.transform.position = boxPos;
                }
            }

            transform.position = pos;
        }
        else
        {
            if (player != null)
            {
                shouldFall = true;
            }
        }
    }
}
