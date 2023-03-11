using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGround : MonoBehaviour
{
    // Start is called before the first frame update
    protected float timeSpawn = 0f;
    protected float timeDlay = 5f;
    public GameObject WalkPrefab;
    void Start()
    {
        float angle = Random.Range(0, -10);
        GameObject minion = Instantiate(WalkPrefab);
        this.timeSpawn = 0;
        GameObject minion1 = Instantiate(WalkPrefab);
        minion1.transform.position = new Vector3(28, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Random.Range(0,-10);
        int random = Random.Range(28,40);
        this.timeSpawn += Time.deltaTime;
        if (this.timeSpawn < this.timeDlay) return;
        else
        {
            this.timeSpawn = 0;
            GameObject minion = Instantiate(WalkPrefab);
            minion.transform.position = new Vector3(random, angle, 0);
        }

    }
}