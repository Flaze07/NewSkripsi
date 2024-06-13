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
        private GameObject obstacleDuckPrefab;
        [SerializeField]
        private float obstacleSpeed;
        [SerializeField]
        private float maxSpawnDelay;
        [SerializeField]
        private float minSpawnDelay;
        [SerializeField]
        private float spawnDelayDecrement;
        private float spawnDelay;
        private float spawnDelayTimer;

        void Start()
        {
            spawnDelay = maxSpawnDelay;
            // spawnDelayTimer = spawnDelay;
        }

        void Update()
        {
            spawnDelayTimer -= Time.deltaTime;
            if (spawnDelayTimer <= 0)
            {
                GameObject go = null;
                int index = Random.Range(0, 2);
                if(index == 0)
                {
                    go = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    go = Instantiate(obstacleDuckPrefab, transform.position, Quaternion.identity);
                }
                go.GetComponent<Obstacle>().Initialize(obstacleSpeed);

                spawnDelay -= spawnDelayDecrement;
                spawnDelay = Mathf.Clamp(spawnDelay, minSpawnDelay, maxSpawnDelay);

                spawnDelayTimer += spawnDelay;
            }
        }
    }

}

