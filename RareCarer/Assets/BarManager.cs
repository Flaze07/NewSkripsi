using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RC.minigameUI;


namespace RC
{
    public class BarManager : MonoBehaviour
    {
        public static BarManager instance;

        //UI Elements
        [SerializeField]
        private Image hungerBar;
        [SerializeField]
        private Image cleanlinessBar;
        [SerializeField]
        private Image PlayBar;
        [SerializeField]
        private MinigameUIBar happinessBar;
        [SerializeField]
        private MinigameUIBar happinessBarInPanel;

        //interacted UI Elements
        [SerializeField]
        private GameObject interactedOption;
        [SerializeField]
        private Image interactedHungerBar;
        [SerializeField]
        private Image interactedCleanlinessBar;
        [SerializeField]
        private Image interactedPlayBar;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


        public void UpdateBar(Animal animal)
        {
            hungerBar.fillAmount = animal.Hunger / 100;
            cleanlinessBar.fillAmount = animal.Cleanliness / 100;
            PlayBar.fillAmount = animal.Play / 100;

            if (interactedOption.activeSelf)
            {
                interactedHungerBar.fillAmount = animal.Hunger / 100;
                interactedCleanlinessBar.fillAmount = animal.Cleanliness / 100;
                interactedPlayBar.fillAmount = animal.Play / 100;
            }

            happinessBar.ChangeProgress(animal.Happiness);
            happinessBarInPanel.ChangeProgress(animal.Happiness);
        }
    }
}