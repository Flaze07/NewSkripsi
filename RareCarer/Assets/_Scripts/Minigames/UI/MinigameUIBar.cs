using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RC.minigameUI
{
    public class MinigameUIBar : MonoBehaviour
    {
        [SerializeField] private float barWidth;
        [SerializeField] private GameObject barFillObject;

        public void ChangeProgress(float percentage)
        {
            Vector2 targetSize = new Vector2((percentage / 100) * barWidth, barFillObject.GetComponent<RectTransform>().rect.height);

            barFillObject.GetComponent<RectTransform>().sizeDelta = targetSize;
        }
    }
}