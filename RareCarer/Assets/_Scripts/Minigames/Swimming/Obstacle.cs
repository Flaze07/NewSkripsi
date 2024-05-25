using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RC.Swimming
{

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D()
    {
        SwimmingManager.instance.EndGame();
    }
}

}

