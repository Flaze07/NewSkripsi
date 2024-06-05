using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class MinigameTutorialCarouselIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject carouselIndicatorPrefab;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        private List<GameObject> carouselIndicators;

        public void Initialize(MinigameInfo minigameInfo)
        {
            carouselIndicators = new List<GameObject>();

            for (int i =  0; i < minigameInfo.description.Count ; i++)
            {
                GameObject temp = Instantiate(carouselIndicatorPrefab,transform);
                carouselIndicators.Add(temp);
            }
        }

        public void ChangeIndex(int index)
        {
            for(int i = 0; i < carouselIndicators.Count; i ++)
            {
                carouselIndicators[i].GetComponent<Image>().color = unselectedColor;
            }
            carouselIndicators[index].GetComponent<Image>().color = selectedColor;
        }
    }


}