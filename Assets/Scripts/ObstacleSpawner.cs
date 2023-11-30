using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;      // array of all the different types of obstacles

    public float minSpawnY;             // minimum height objects can spawn at
    public float maxSpawnY;             // maximum height objects can spawn at
    private float leftSpawnX;           // left hand side of the screen
    private float rightSpawnX;          // right hand side of the screen

    public float spawnRate;             // time in seconds between each spawn
    private float lastSpawn;            // Time.time of the last spawn

    void Start ()
    {
        // setting left and right spawn borders
        // do this by getting camera horizontal borders
        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize) * cam.aspect;

        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;
    }

    void Update ()
    {
        // every 'spawnRate' seconds, spawn a new obstacle
        if(Time.time - spawnRate >= lastSpawn)
        {
            lastSpawn = Time.time;
            SpawnObstacle();
        }
    }

    // spawns a random obstacle at a random spawn point
    void SpawnObstacle ()
    {
        // spawn a random obstacle at a random spawn point
        GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], GetSpawnPosition(), Quaternion.identity);

        // set obstacle's direction to move in
        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }

    // returns a random spawn position for an obstacle
    Vector3 GetSpawnPosition ()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);

        return new Vector3(x, y, 0);
    }
}