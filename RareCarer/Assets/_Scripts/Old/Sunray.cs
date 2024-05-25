using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;

namespace RC.Old
{

enum Direction
{
    Up = 1,
    Down = -1,
    Left = 2,
    Right = -2,
}

struct DirectionChance
{
    public Direction direction;
    public int chance;
}

public class Sunray : MonoBehaviour
{
    [SerializeField]
    private int chanceBigIncrement = 5;
    [SerializeField]
    private int chanceSmallIncrement = 2;
    [SerializeField]
    private float distance = 10;
    [SerializeField]
    private float time = 1;
    private List<DirectionChance> directionChance = new();
    private bool isMoving = false;
    
    void Start()
    {
        directionChance.Add(new(){
            direction = Direction.Up,
            chance = 25
        });
        directionChance.Add(new(){
            direction = Direction.Down,
            chance = 25
        });
        directionChance.Add(new(){
            direction = Direction.Left,
            chance = 25
        });
        directionChance.Add(new(){
            direction = Direction.Right,
            chance = 25
        });
    }

    void Update()
    {
        if(!isMoving)
        {
            StartCoroutine(Move());
            isMoving = true;
        }
    }
    

    private IEnumerator Move()
    {
        float speed = distance / time;
        float distanceTravelled = 0;
        var direct = GenerateDirection();
        while(distanceTravelled < distance)
        {
            float step = speed * Time.deltaTime;
            switch(direct)
            {
                case Direction.Up:
                    transform.position += new Vector3(0, step, 0);
                    break;
                case Direction.Down:
                    transform.position += new Vector3(0, -step, 0);
                    break;
                case Direction.Left:
                    transform.position += new Vector3(-step, 0, 0);
                    break;
                case Direction.Right:
                    transform.position += new Vector3(step, 0, 0);
                    break;
            }
            distanceTravelled += step;
            yield return new WaitForNextFrameUnit();
        }
        isMoving = false;
    }

    private Direction GenerateDirection()
    {
        int result = Random.Range(1, 101);
        Direction chosenDirection = Direction.Right;
        if(directionChance[0].chance <= result)
        {
            chosenDirection = directionChance[0].direction;
        }
        if(directionChance[1].chance + directionChance[0].chance <= result)
        {
            chosenDirection = directionChance[1].direction;
        }
        if(directionChance[2].chance + directionChance[1].chance + directionChance[0].chance <= result)
        {
            chosenDirection = directionChance[2].direction;
        }

        for(int i = 0; i < directionChance.Count; i++)
        {
            if(directionChance[i].direction == chosenDirection)
            {
                var dirChance = directionChance[i];
                dirChance.chance += chanceBigIncrement;
                directionChance[i] = dirChance;
            }
            else if(directionChance[i].direction != GetOppositeDirection(chosenDirection))
            {
                var dirChance = directionChance[i];
                dirChance.chance += chanceSmallIncrement;
                directionChance[i] = dirChance;
            }
            else
            {
                var dirChance = directionChance[i];
                // if(dirChance.chance < chanceBigIncrement + (chanceSmallIncrement * 2))
                // {
                //     ResetDirectionChance();
                //     break;
                // }
                dirChance.chance -= chanceBigIncrement + (chanceSmallIncrement * 2);
                directionChance[i] = dirChance;
            }
        }

        return chosenDirection;
    }
    
    private void ResetDirectionChance()
    {
        for(int i = 0; i < directionChance.Count; i++)
        {
            var dirChance = directionChance[i];
            dirChance.chance = 25;
            directionChance[i] = dirChance;
        }
    }

    private Direction GetOppositeDirection(Direction direction)
    {
        return (Direction) ((int) direction * -1);
    }

}

}

