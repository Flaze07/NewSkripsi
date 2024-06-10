using RC.minigameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{

    public class AnimalParent : MonoBehaviour
    {
        public static AnimalParent instance;
        [SerializeField]
        private List<Animal> animals;

        public List<Animal> Animals => animals;

        //UI Elements
        [SerializeField]
        private Image hungerBar;
        [SerializeField]
        private Image cleanlinessBar;
        [SerializeField]
        private Image PlayBar;
        [SerializeField]
        private MinigameUIBar happinessBar;

        //interacted UI Elements
        [SerializeField]
        private GameObject interactedOption;
        [SerializeField]
        private Image interactedHungerBar;
        [SerializeField]
        private Image interactedCleanlinessBar;
        [SerializeField]
        private Image interactedPlayBar;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
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

            if(interactedOption.activeSelf)
            {
                interactedHungerBar.fillAmount = animal.Hunger / 100;
                interactedCleanlinessBar.fillAmount = animal.Cleanliness / 100;
                interactedPlayBar.fillAmount = animal.Play / 100;
            }

            happinessBar.ChangeProgress(animal.Happiness);
        }
    }

}

