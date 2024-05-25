using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RC.Sunbath
{
    
public class SunbathManager : MonoBehaviour
{
    public static SunbathManager instance;
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

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}

}