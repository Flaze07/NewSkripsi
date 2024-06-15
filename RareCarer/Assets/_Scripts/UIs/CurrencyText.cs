using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RC
{
    public class CurrencyText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        // Update is called once per frame
        void Update()
        {
            text.text = "Currency: " + GameManager.instance.Currency.ToString();
        }
    }
}

