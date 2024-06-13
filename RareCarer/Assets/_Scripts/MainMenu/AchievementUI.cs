using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC;

namespace RC.MainMenu
{
    public class AchievementUI : MonoBehaviour
    {
        public static AchievementUI instance;

        [SerializeField] private AchievementBlock achievementBlockPrefab;
        [SerializeField] private Sprite achievementLockSprite;
        [SerializeField] private Transform achievementRoot;

        private List<AchievementBlock> achievementBlocks = new List<AchievementBlock>();

        public void Start()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }


        //read player achievement 
        public void Initialize()
        {
            if(AchievementManager.instance != null)
            {
                UpdateAchievementList(AchievementManager.instance.AchievementList);
            }
            else
            {
                UpdateAchievementList(null);
            }
        }

        public void UpdateAchievementList(List<AchievementData> achievementList)
        {

            for (int i = 0; i < achievementList.Count; i++)
            {
                AchievementBlock temp = GameObject.Instantiate<AchievementBlock>(achievementBlockPrefab, achievementRoot);
                achievementBlocks.Add(temp);

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

        public void CloseAchievement()
        {
            for(int i = 0; i < achievementBlocks.Count; i ++)
            {
                Destroy(achievementBlocks[i].gameObject);
            }

            achievementBlocks.Clear();

            this.gameObject.SetActive(false);
        }
    }

}