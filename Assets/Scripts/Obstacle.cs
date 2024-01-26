using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 moveDir;         // direction to move in
    public float moveSpeed;         // speed to move at along moveDir

    private float aliveTime = 8.0f; // time before object is destroyed

    void Start ()
    {
        Destroy(gameObject, aliveTime);
    }

    void Update ()
    {
        // move obstacle in certain direction over time
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // rotate obstacle
        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime);
    }
}