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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.transform.parent == HuntingManager.instance.MainAjag.transform)
        {
            StartCoroutine(HuntingManager.instance.Deer.MoveForward());
        }
    }
}

}