using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Swimming
{

public class SwimmingManager : MonoBehaviour
{
    public static SwimmingManager instance;
    private int score;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        score = 0;
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }
}

}

