using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RC.MainMenu
{
    public class AchievementBlock : MonoBehaviour
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private TextMeshProUGUI achievementTitle;
        [SerializeField] private TextMeshProUGUI achievementDescription;

        public Image ImageComponent => imageComponent;
        public TextMeshProUGUI AchievementName => achievementTitle;
        public TextMeshProUGUI AchievementDescription => achievementDescription;


        public void Initalize(string achievementTitle, string achievementDescription, Sprite achievementSprite,bool unlocked)
        {
            if(unlocked)
            {

            }
            else
            {
                
            }
        }


    }
}