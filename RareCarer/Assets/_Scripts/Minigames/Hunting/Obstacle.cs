using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public void Initialize(float speed)
    {
        this.speed = speed;
    }
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}

}