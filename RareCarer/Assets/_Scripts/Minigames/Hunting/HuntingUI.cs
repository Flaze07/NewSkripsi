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
        private Sprite StarUnlockSprite;
        [SerializeField]
        private Image star1;
        [SerializeField]
        private Image star2;
        [SerializeField]
        private Image star3;


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
    }


}
