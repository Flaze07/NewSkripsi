using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RC
{
    public class HappinessPanel : MonoBehaviour
    {
        [SerializeField] private Animal komodoAnimal;
        [SerializeField] private Animal orangUtanAnimal;
        [SerializeField] private Animal ajagAnimal;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI unlock100;
        [SerializeField] private TextMeshProUGUI unlock75;
        [SerializeField] private TextMeshProUGUI unlock50;
        [SerializeField] private TextMeshProUGUI unlock25;

        private void OnEnable()
        {
            SetupPanel();
        }

        private void SetupPanel()
        {
            titleText.text = $"Tingkat Kebahagiaan {GameManager.instance.CurrentAnimal.Name}";

            SetTexts(GameManager.instance.CurrentAnimal);
        }

        private void SetTexts(Animal animal)
        {
            unlock25.gameObject.SetActive(true);
            unlock50.gameObject.SetActive(true);
            unlock75.gameObject.SetActive(true);
            unlock100.gameObject.SetActive(true);
            SetUnlocks(animal);
            SetTextsGeneric();

            if (animal == komodoAnimal) SetKomodoTexts();
            else if (animal == orangUtanAnimal) SetOrangUtanTexts();
            else if (animal == ajagAnimal) SetAjagTexts();
        }

        private void CheckUnlocked(TextMeshProUGUI text, float happiness, float target)
        {
            if (happiness >= target) text.color = Color.white;
            else text.color = Color.black;
        }

        private void SetUnlocks(Animal animal)
        {
            CheckUnlocked(unlock25, animal.Happiness, 25f);
            CheckUnlocked(unlock50, animal.Happiness, 50f);
            CheckUnlocked(unlock75, animal.Happiness, 75f);
            CheckUnlocked(unlock100, animal.Happiness, 100f);
        }

        private void SetTextsGeneric()
        {
            unlock25.text = "25% - ";
            unlock50.text = "50% - ";
            unlock75.text = "75% - ";
            unlock100.text = "100% - ";
        }

        private void SetKomodoTexts()
        {
            unlock25.text += "Membuka akses toko.";
            unlock50.text += "Hewan Ajag diperoleh.";
            unlock75.text += "Penghasilan pasif meningkat sebanyak 10 emas setiap menit.";
            unlock100.text += "Mendapatkan achievement untuk Komodo.";
        }

        private void SetAjagTexts()
        {
            unlock25.gameObject.SetActive(false);
            unlock50.text += "Hewan Orang Utan diperoleh.";
            unlock75.text += "Penghasilan pasif meningkat sebanyak 10 emas setiap menit.";
            unlock100.text += "Mendapatkan achievement untuk Ajag.";
        }

        private void SetOrangUtanTexts()
        {
            unlock25.gameObject.SetActive(false);
            unlock50.text += "Penghasilan pasif meningkat sebanyak 5 emas setiap menit.";
            unlock75.text += "Penghasilan pasif meningkat sebanyak 5 emas setiap menit.";
            unlock100.text += "Mendapatkan achievement untuk Orang Utan.";
        }
    }
}