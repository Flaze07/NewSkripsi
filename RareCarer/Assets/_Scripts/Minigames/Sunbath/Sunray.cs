using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace RC.Sunbath
{

enum Direction
{
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4
}

public class Sunray : MonoBehaviour
{
    public delegate void HandleDisappearDelegate(int instanceID);
    public HandleDisappearDelegate HandleDisappear;
    [SerializeField]
    public float speed = 0.2f;
    [SerializeField]
    private float disappearChance = 0.1f;
    private float lifetime = 0;
    private Direction chosenDirection;
    // Start is called before the first frame update
    void Start()
    {
        var chooseDirection = Direction.Right;
        chosenDirection = (Direction)chooseDirection;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime = lifetime + Time.deltaTime;
        if(Random.Range(0f, 1f) < ChanceValue(lifetime))
        {
            HandleDisappear?.Invoke(GetInstanceID());
            Destroy(gameObject);
        }
        Move();
    }

    public void SetDeathChance()
    {
        lifetime = 45;
    }

    private void Move()
    {
        var alteredSpeed = speed * Time.deltaTime;
        switch (chosenDirection)
        {
            case Direction.Up:
                transform.position += Vector3.up * alteredSpeed;
                break;
            case Direction.Down:
                transform.position += Vector3.down * alteredSpeed;
                break;
            case Direction.Left:
                transform.position += Vector3.left * alteredSpeed;
                break;
            case Direction.Right:
                transform.position += Vector3.right * alteredSpeed;
                break;
        }
    }

    private float ChanceValue(float x)
    {
        return Mathf.Pow(1.5f, x / 4) / 10000;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SunbathManager.instance.IncreaseScore(2.5f * Time.deltaTime);
        }
    }
    
}

}
