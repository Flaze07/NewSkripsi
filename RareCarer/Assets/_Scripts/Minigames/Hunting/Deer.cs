using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class Deer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}

}

