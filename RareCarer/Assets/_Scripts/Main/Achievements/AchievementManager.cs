using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RC
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager instance;

        [SerializeField]
        private List<AchievementData> achievementList;
        public List<AchievementData> AchievementList => achievementList;


        void Start()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public void UnlockAchievement(string achievementName)
        {
            for(int i = 0; i < achievementList.Count; i ++)
            {
                if (achievementList[i].achievementName == achievementName)
                {
                    achievementList[i].unlocked = true;
                }
            }
        }
    }

    [System.Serializable]
    public class AchievementData
    {
        public bool unlocked;
        public Sprite achievementIcon;
        public string achievementName;
        public string achievementDescription;
    }
}