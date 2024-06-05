using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RC.MainMenu
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private AchievementBlock achievementBlockPrefab;
        [SerializeField] private Sprite achievementLockSprite;
        [SerializeField] private Transform achievementRoot;

        private List<AchievementBlock> achievementBlocks;

        //read player achievement 
        public void Initialize()
        {
            CreateAchievementBlocks(null);
            UpdateAchievementList(null);
        }

        public void CreateAchievementBlocks(List<string> achievementList)
        {
            for(int i = 0; i < achievementList.Count; i ++)
            {
                //instantiate the thing
                AchievementBlock temp = GameObject.Instantiate<AchievementBlock>(achievementBlockPrefab, achievementRoot);
                achievementBlocks.Add(temp);
            }
        }

        public void UpdateAchievementList(List<TemporaryAchievementBlock> achievementList)
        {
            for(int i = 0; i < achievementBlocks.Count; i ++)
            {
                if (achievementList[i].unlocked == true)
                {
                    achievementBlocks[i].ImageComponent.sprite = achievementList[i].achievementIcon;
                }
                else
                {
                    achievementBlocks[i].ImageComponent.sprite = achievementLockSprite;
                }

                achievementBlocks[i].AchievementDescription.text = achievementList[i].achievementDescription;
                achievementBlocks[i].AchievementName.text = achievementList[i].achievementName;
            }
        }
    }

    public class TemporaryAchievementBlock
    {
        public bool unlocked = false;

        public Sprite achievementIcon;
        public string achievementName;
        public string achievementDescription;
    }

}