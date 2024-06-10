using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class MinigameButton : MonoBehaviour
    {
        [SerializeField] private Sprite starAchievedSprite;
        [SerializeField] private Image[] starObjects;

        public void setStars(int starAmmount)
        {
            for(int i = 0; i < starAmmount; i ++)
            {
                starObjects[i].sprite = starAchievedSprite;
            }
        }
    }

}