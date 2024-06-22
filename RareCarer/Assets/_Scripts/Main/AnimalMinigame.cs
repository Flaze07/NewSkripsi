using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{

[Serializable]
public class AnimalMinigameData
{
    [SerializeField]
    private string minigameSceneName;
    public string MinigameSceneName => minigameSceneName;
    [SerializeField]
    private Sprite minigameIcon;
    public Sprite MinigameIcon => minigameIcon;
    public int starAchieved = 0;
}

public class AnimalMinigame : MonoBehaviour
{
    [SerializeField]
    private List<AnimalMinigameData> minigames;
    private List<AnimalMinigameData> unlockedMinigames = new();
    public List<AnimalMinigameData> UnlockedMinigames => unlockedMinigames;

    void Start()
    {
        unlockedMinigames.Add(minigames[0]);
    }

    public void UnlockSecondMinigame(float before, float current)
    {
        if (minigames.Count <= 1) return;
        if(before < 25)
        {
            if(current >= 25)
            {
                if(!unlockedMinigames.Contains(minigames[1]))
                {
                    unlockedMinigames.Add(minigames[1]);
                }
            }
        }
    }
}

}

