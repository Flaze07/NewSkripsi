using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RC
{

    [Serializable]
    public class Food
    {
        [SerializeField]
        private string type;
        [SerializeField]
        private int amount;
        [SerializeField]
        private int price;
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private string description;

        public string Description => description;
        public string Type => type;
        public int Amount
        {
            get => amount;
            set => amount = value;
        }
        public int Price => price;
        public Sprite Sprite => sprite;
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField]
        private List<Animal> availableAnimals = new();
        [SerializeField]
        private Animal currentAnimal;
        public Animal CurrentAnimal => currentAnimal;
        [SerializeField]
        private TextMeshProUGUI currencyText;
        [SerializeField]
        private int currency = 100;
        public int Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
                currencyText.text = "Currency: " + currency.ToString();
            }
        }
        private int passiveIncome = 0;
        public int PassiveIncome
        {
            get
            {
                return passiveIncome;
            }
            set
            {
                passiveIncome = value;
            }
        }
        private float passiveIncomeTimer;
        [SerializeField]
        private List<Food> foods;
        public List<Food> Foods => foods;
        [SerializeField]
        private SwitchAnimal switchAnimal;
        public bool IsSwitching { get; set; } = false;
        public static event Action<Animal> OnAnimalSwitch;
        // Start is called before the first frame update
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
            StartCoroutine(LateStart());
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => FindCurrencyText();
        }

        IEnumerator LateStart()
        {
            yield return new WaitForEndOfFrame();
            availableAnimals.Add(AnimalParent.instance.Animals[0]);
            currentAnimal = availableAnimals[0];
        }

        private void FindCurrencyText()
        {
            var gb = GameObject.Find("CoinAmount");
            if (gb != null)
            {
                currencyText = gb.GetComponent<TextMeshProUGUI>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < availableAnimals.Count; i++)
            {
                availableAnimals[i].AnimalUpdate();
            }

            passiveIncomeTimer += Time.deltaTime;
            if(passiveIncomeTimer >=60f)
            {
                passiveIncomeTimer -= 60f;
                currency += passiveIncome;
            }
        }

        public void AddAvailableAnimal(Animal animal)
        {
            availableAnimals.Add(animal);
        }

        public void SwitchAnimal(int direction)
        {
            if (availableAnimals.Count == 1)
            {
                return;
            }

            IsSwitching = true;

            int idx = availableAnimals.IndexOf(currentAnimal);
            int nextIdx = idx + direction;

            if (nextIdx < 0)
            {
                nextIdx = availableAnimals.Count - 1;
            }
            else if (nextIdx >= availableAnimals.Count)
            {
                nextIdx = 0;
            }

            Animal nextAnimal = availableAnimals[nextIdx];
            switchAnimal.Switch(direction, currentAnimal, nextAnimal);
            currentAnimal = availableAnimals[nextIdx];
            OnAnimalSwitch?.Invoke(currentAnimal);
        }
        public void SaveData()
        {
            PlayerPrefs.SetInt("Animal Count", availableAnimals.Count);
            PlayerPrefs.SetInt("Currency", currency);
            for(int i = 0; i < availableAnimals.Count; ++i)
            {
                var animal = availableAnimals[i];
                PlayerPrefs.SetFloat($"Animal {i} happiness", animal.Happiness);
                PlayerPrefs.SetFloat($"Animal {i} hunger", animal.Hunger);
                PlayerPrefs.SetFloat($"Animal {i} cleanliness", animal.Cleanliness);
                PlayerPrefs.SetFloat($"Animal {i} play", animal.Play);
                
                var minigames = animal.gameObject.GetComponent<AnimalMinigame>();
                var minigameData = minigames.UnlockedMinigames;
                PlayerPrefs.SetInt($"Animal {i} minigame star", minigameData[0].starAchieved);
            }

            Foods.ForEach(food =>
            {
                PlayerPrefs.SetInt($"Food {food.Type} amount", food.Amount);
            });

            var achievementManager = AchievementManager.instance;
            var achievements = achievementManager.AchievementList;
            for(int i = 0; i < achievements.Count; ++i)
            {
                PlayerPrefs.SetInt($"Achievement {i} unlocked", achievements[i].unlocked ? 1 : 0);
            }
        }
    }
}

