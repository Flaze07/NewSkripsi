using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RC.Hunting
{
    public class HuntingUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI distanceText;
        [SerializeField]
        private TextMeshProUGUI timerText;
        [SerializeField]
        private Image barTimer;

        [SerializeField]
        private Sprite StarUnlockSprite;
        [SerializeField]
        private Image star1;
        [SerializeField]
        private Image star2;
        [SerializeField]
        private Image star3;


        [SerializeField]
        private Color speedIndicatorTrue;
        [SerializeField] 
        private Color speedIndicatorFalse;
        [SerializeField]
        private Image speedIndicator1;
        [SerializeField]
        private Image speedIndicator2;
        [SerializeField]
        private Image speedIndicator3;


        private HuntingManager manager;


        private bool initialize;

        public void Initialize(HuntingManager manager)
        {
            this.manager = manager;
            initialize = true;
        }

        public void Update()
        {
            if(initialize)
            {
                distanceText.text = Mathf.CeilToInt( manager.Distance) + " m";
                timerText.text = Mathf.Ceil(manager.CurrentGameTime).ToString();

                StarChecker();
                GameTimer();
                SpeedUI();
            }
        }

        public void SpeedUI()
        {
            if(manager.SpeedValue >= 1)
            {
                speedIndicator1.color = speedIndicatorTrue;
            }
            else
            {
                speedIndicator1.color = speedIndicatorFalse;
            }

            if (manager.SpeedValue >= 2)
            {
                speedIndicator2.color = speedIndicatorTrue;
            }
            else
            {
                speedIndicator2.color = speedIndicatorFalse;
            }

            if (manager.SpeedValue >= 3)
            {
                speedIndicator3.color = speedIndicatorTrue;
            }
            else
            {
                speedIndicator3.color = speedIndicatorFalse;
            }
        }

        public void StarChecker()
        {
            if(manager.TotalStar >= 1)
            {
                star1.sprite = StarUnlockSprite;
            }
            
            if(manager.TotalStar >= 2)
            {
                star2.sprite = StarUnlockSprite;
            }

            if(manager.TotalStar >= 3)
            {
                star3.sprite = StarUnlockSprite;
            }
        }

        public void GameTimer()
        {
            float percentage = manager.CurrentGameTime / manager.GameTimer;

            barTimer.fillAmount = percentage;
            timerText.text = Mathf.CeilToInt( manager.CurrentGameTime ).ToString();
        }
    }


}
