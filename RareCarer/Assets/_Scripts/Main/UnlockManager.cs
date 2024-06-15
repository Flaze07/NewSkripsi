using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class UnlockManager : MonoBehaviour
    {
        [SerializeField] private Animal komodoAnimal;
        [SerializeField] private Animal orangUtanAnimal;
        [SerializeField] private Animal ajagAnimal;

        private bool[] komodoUnlock = new bool[4];
        private bool[] orangUtanUnlock = new bool[4];
        private bool[] ajagUnlock = new bool[4];

        [SerializeField] private Button shopButton;
        [SerializeField] private GameObject animalUnlockPanel;
        [SerializeField] private TMPro.TextMeshProUGUI animalUnlockPanelName;
        [SerializeField] private GameObject shopUnlockPanel;

        private int passiveIncome;

        void Start()
        {

        }

        void Update()
        {
            KomodoUnlocks();
            AjagUnlocks();
            OrangUtanUnlocks();
        }

        public void LoadSave()
        {
            var gameManager = GameManager.instance;
            var availableAnimals = gameManager.AvailableAnimals;
            if(availableAnimals.Count == 3)
            {
                komodoUnlock[1] = true;
                ajagUnlock[1] = true;
            }
            else if(availableAnimals.Count == 2)
            {
                komodoUnlock[1] = true;
            }
        }

        public void CloseShopUnlockPanel()
        {
            shopUnlockPanel.SetActive(false);
        }

        public void CloseAnimalUnlockPanel()
        {
            animalUnlockPanel.SetActive(false);
        }

        public void OpenAnimalunlockPanel(string animal)
        {
            animalUnlockPanel.SetActive(true);
            animalUnlockPanelName.text = animal;
        }

        public void OpenShopUnlockPanel()
        {
            shopUnlockPanel.SetActive(true);
        }

        #region Unlocks

        private void KomodoUnlocks()
        {
            //shop button 
            if (komodoAnimal.Happiness >= 25f)
            {
                shopButton.interactable = true;
                komodoUnlock[0] = true;
            }

            //unlock ajag
            if(komodoAnimal.Happiness >= 50f)
            {
                if (komodoUnlock[1] != true)
                {
                    //unlock ajag
                    ajagAnimal.Unlock();
                    OpenAnimalunlockPanel("Ajag");
                    komodoUnlock[1] = true;
                }
            }

            //passive income
            if(komodoAnimal.Happiness >= 75f)
            {
                if (komodoUnlock[2] != true)
                {
                    GameManager.instance.PassiveIncome += 10;
                    komodoUnlock[2] = true;
                }
            }
            else
            {
                if (komodoUnlock[2] != true)
                {
                    GameManager.instance.PassiveIncome -= 10;
                    komodoUnlock[2] = false;
                }

            }

            //achievement
            if (komodoAnimal.Happiness >= 100f)
            {
                komodoUnlock[3] = true;
                AchievementManager.instance.UnlockAchievement("Komodo's Love");
            }
        }

        private void OrangUtanUnlocks()
        {
            //orang utan unlock
            if (orangUtanAnimal.Happiness > 50f)
            {
                if (orangUtanUnlock[1] != true)
                {
                    orangUtanUnlock[1] = true;
                    GameManager.instance.PassiveIncome += 5;
                }
            }
            else
            {
                if (orangUtanUnlock[1] != false)
                {
                    orangUtanUnlock[1] = false;
                    GameManager.instance.PassiveIncome += 5;
                }
            }

            //passive unlock
            if (orangUtanAnimal.Happiness > 75f)
            {
                if (orangUtanUnlock[2] != true)
                {
                    orangUtanUnlock[2] = true;
                    GameManager.instance.PassiveIncome += 5;
                }
            }
            else
            {
                if (orangUtanUnlock[2] != false)
                {
                    orangUtanUnlock[2] = false;
                    GameManager.instance.PassiveIncome += 5;
                }
            }

            //achievement
            if (orangUtanAnimal.Happiness > 100f)
            {
                orangUtanUnlock[3] = true;
                AchievementManager.instance.UnlockAchievement("Orang Utan's Love");
            }
        }

        private void AjagUnlocks()
        {

            if (ajagAnimal.Happiness > 50f)
            {
                if(ajagUnlock[1] != true)
                {
                    orangUtanAnimal.Unlock();
                    OpenAnimalunlockPanel("Orang Utan");
                    ajagUnlock[1] = true;
                }
            }

            if (ajagAnimal.Happiness > 75f)
            {
                if (ajagUnlock[2] != true)
                {
                    ajagUnlock[2] = true;
                    GameManager.instance.PassiveIncome += 10;
                }
            }
            else
            {
                if (ajagUnlock[2] != false)
                {
                    ajagUnlock[2] = false;
                    GameManager.instance.PassiveIncome += 10;
                }
            }

            if (ajagAnimal.Happiness > 100f)
            {
                ajagUnlock[3] = true;
                AchievementManager.instance.UnlockAchievement("Ajag's Love");
            }
        }
        #endregion
    }


}