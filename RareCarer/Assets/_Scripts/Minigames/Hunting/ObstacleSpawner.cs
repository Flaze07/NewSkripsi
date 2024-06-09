using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private float obstacleSpeed;
    [SerializeField]
    private float spawnDelay;
    private float spawnDelayTimer;

    void Start()
    {
        // spawnDelayTimer = spawnDelay;
    }

    void Update()
    {
        spawnDelayTimer -= Time.deltaTime;
        if(spawnDelayTimer <= 0)
        {
            var go = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            go.GetComponent<Obstacle>().Initialize(obstacleSpeed);
            spawnDelayTimer += spawnDelay;
        }
    }
}

}

