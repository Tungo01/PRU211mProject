using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    // Start is called before the first frame update
    protected float timeSpawn = 0f;
    // Update is called once per frame
    float speed = 10;
    void Update()
    {
        // Lấy tọa độ hiện tại của đối tượng
        Vector3 pos = transform.position;

        // Tính toán tọa độ mới của đối tượng
        pos.x -= speed * Time.deltaTime;

        // Đặt tọa độ mới cho đối tượng
        transform.position = pos;
        float angle = Random.Range(0, -10);
        this.timeSpawn += Time.deltaTime;
        if (this.timeSpawn > 10)
        {
            Destroy(gameObject);
        } 
            
    }
}
