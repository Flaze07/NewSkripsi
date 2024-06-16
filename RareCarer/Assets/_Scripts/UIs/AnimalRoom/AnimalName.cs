using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RC
{
    public class AnimalName : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;

        private void OnEnable()
        {
            GameManager.OnAnimalSwitch += ChangeNameToCurrent;
        }

        private void OnDisable()
        {
            GameManager.OnAnimalSwitch -= ChangeNameToCurrent;
        }

        private void ChangeNameToCurrent(Animal animal)
        {
            nameText.text = animal.AnimalName;
        }
    }
}