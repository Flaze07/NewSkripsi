using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace RC
{

    public class Animal : MonoBehaviour
    {
        [SerializeField]
        private string name;
        public string Name => name;
        [SerializeField]
        private List<string> likedFood;
        [SerializeField]
        private bool unlocked = false;
        private float happiness = 0;
        public float Happiness => happiness;
        private float hunger = 50;
        public float Hunger
        {
            get => hunger;
            set => hunger = value;
        }
        private float cleanliness = 50;
        public float Cleanliness
        {
            get => cleanliness;
            set => cleanliness = value;
        }
        private float play = 50;
        public float Play
        {
            get => play;
            set => play = value;
        }

        // Animal Parent
        [SerializeField]
        private AnimalParent animalParent;


        [SerializeField]
        private SpriteRenderer sprite;
        public SpriteRenderer Sprite => sprite;
        [SerializeField]
        private AnimalCleanliness animalCleanliness;

        /// <summary>
        /// This event will be called when the happiness of the animal changes
        /// Specifically at the value of 25, 50, 75, 100
        /// </summary>
        public UnityEvent<float, float> OnHappinessChange;

        // Start is called before the first frame update
        void Start()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        public void AnimalUpdate()
        {
            if(unlocked)
            {
                UpdateHappiness();
                UpdateStats();
                animalCleanliness.UpdateCleanliness();

                if(BarManager.instance != null)
                {
                    BarManager.instance.UpdateBar(this);
                }
            }
        }

        public void Hide()
        {
            sprite.gameObject.SetActive(false);
        }
        public void Show()
        {
            sprite.gameObject.SetActive(true);
        }

        /// <summary>
        /// Feed the animal with the given food type
        /// if the animal likes the food, hunger will be increased by 40
        /// if the animal does not like the food, hunger will be increased by 10
        /// </summary>
        public void Feed(string foodType)
        {
            if (likedFood.Contains(foodType))
            {
                hunger += 40;
            }
            else
            {
                hunger += 10;
            }
        }

        /// <summary>
        /// Happiness will be increemented if all three values are above 50
        /// if all three values are below 25, happiness will be decremented
        /// if at least one value is above 50 while the others are below 25, happiness will not change
        /// if all values are between 25 and 50, happiness will not change
        /// </summary>
        private void UpdateHappiness()
        {
            if (!(hunger < 25 && cleanliness < 25 && play < 25))
            {
                float before = happiness;
                happiness += 0.25f * Time.deltaTime;
                OnHappinessChange.Invoke(before, happiness);
            }
            else if (!(hunger > 50 && cleanliness > 50 && play > 50))
            {
                float before = happiness;
                happiness -= 0.15f * Time.deltaTime;
                OnHappinessChange.Invoke(before, happiness);
            }

            happiness = MathF.Min(MathF.Max(happiness, 0), 100f);
        }

        private void UpdateStats()
        {
            float decreaseRatePersec = 0.4f;

            hunger -= Time.deltaTime * decreaseRatePersec;
            cleanliness -= Time.deltaTime * decreaseRatePersec;
            play -= Time.deltaTime * decreaseRatePersec;

            hunger = MathF.Min(MathF.Max(hunger, 0),100f);
            cleanliness = MathF.Min(MathF.Max(cleanliness, 0),100f);
            play = MathF.Min(MathF.Max(play, 0),100f);
        }
    }

}

