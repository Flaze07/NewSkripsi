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
            }
        }

        private void AjagUnlocks()
        {

            if (komodoAnimal.Happiness > 50f)
            {

            }
            else
            {

            }

            if (komodoAnimal.Happiness > 75f)
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

            if (komodoAnimal.Happiness > 100f)
            {

            }
            else
            {

            }
        }
    }


}