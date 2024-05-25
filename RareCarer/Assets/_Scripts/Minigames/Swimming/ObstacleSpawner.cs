using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RC.Swimming
{

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private float maxSpawnDelay;
    [SerializeField]
    private float minSpawnDelay;

    private float spawnDelay;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(spawnDelay <= 0)
        {
            SpawnObstacle();
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        else
        {
            spawnDelay -= Time.deltaTime;
        }
    }

    private void SpawnObstacle()
    {
        var obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        var obstacleSprite = obstacle.GetComponent<SpriteRenderer>();
        var obstacleWidth = obstacleSprite.bounds.size.x;
        var spawnX = spriteRenderer.bounds.min.x;
        var spawnY = Random.Range(spriteRenderer.bounds.min.y, spriteRenderer.bounds.max.y);
        obstacle.transform.position = new Vector2(spawnX, spawnY);
    }
}

}

