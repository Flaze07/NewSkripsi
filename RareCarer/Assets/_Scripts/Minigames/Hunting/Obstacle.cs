using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

        private void OnEnable()
        {
            HuntingManager.OnGameEnd += Stop;
        }

        private void OnDisable()
        {
            HuntingManager.OnGameEnd += Stop;
        }

        private void Stop()
        {
            Initialize(0);
        }


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
        if(HuntingManager.instance.IsDamaged)
        {
            return;
        }
        if(other.CompareTag("Player") && other.transform.parent == HuntingManager.instance.MainAjag.transform)
        {
            HuntingManager.instance.AjagDamaged();
            Debug.Log("TEST");
            StartCoroutine(HuntingManager.instance.Deer.MoveForward());
            HuntingManager.instance.MainAjag.Flash();
        }
    }
}

}