using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle_Box : MonoBehaviour
{
    //    Ground goGround = go.GetComponent<Ground>();
    //    GameObject go = Instantiate(gameObject);
    //    BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
    Player player;
    

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
        if (pos.x < -10)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.velocity.x *= 0.7f;
        }
    }
}
