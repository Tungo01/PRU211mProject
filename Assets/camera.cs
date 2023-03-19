using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float speed = 1f;
    public float yOffset = 1f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(25.79f, target.position.y,-20f);
        if (target.position.y < 19.2)
        {
            pos.y = 19.2f;
        }
        transform.position = Vector3.Slerp(transform.position,pos,speed* Time.deltaTime);


    }
}
