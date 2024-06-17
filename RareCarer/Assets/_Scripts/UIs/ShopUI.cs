using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{

    public class ShopUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject shopItemParent;
        [SerializeField]
        private ShopItem shopItemPrefab;
        [SerializeField]
        private TMPro.TextMeshProUGUI currencyText;

        [SerializeField]
        private GameObject rightShopPanel;
        [SerializeField]
        private Image foodImage;
        [SerializeField]
        private TMPro.TextMeshProUGUI ownedText;
        [SerializeField]
        private TMPro.TextMeshProUGUI foodTitle;
        [SerializeField]
        private TMPro.TextMeshProUGUI descriptionTitle;
        [SerializeField]
        private TMPro.TextMeshProUGUI coinCost;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip buttonAudioClip;

        private Food selectedItem;

        void Start()
        {
            StartCoroutine(LateStart());

            //change
        }

        IEnumerator LateStart()
        {
            yield return new WaitForEndOfFrame();
            GameManager.instance.Foods.ForEach(food =>
            {
                ShopItem shopItem = Instantiate(shopItemPrefab, shopItemParent.transform);
                Button btn = shopItem.GetComponent<Button>();
                btn.onClick.AddListener(() => SelectItem(food));
                Debug.Log(food.Sprite);
                shopItem.SetSprite(food.Sprite);
            });

            currencyText.text = "Currency : " + GameManager.instance.Currency.ToString();

        }

        public void Update()
        {
            if(selectedItem == null)
            {
                rightShopPanel.SetActive(false);
            }
            else
            {
                rightShopPanel.SetActive(true);
            }
        }

        public void BuyItem()
        {
            if (GameManager.instance.Currency >= selectedItem.Price)
            {
                GameManager.instance.Currency -= selectedItem.Price;
                GameManager.instance.Foods.Find(f => f.Type == selectedItem.Type).Amount++;
                ownedText.text = "Owned : " + GameManager.instance.Foods.Find(f => f.Type == selectedItem.Type).Amount;
            }
        }

        public void SelectItem(Food food)
        {
            selectedItem = food;

            foodImage.sprite = food.Sprite;
            foodTitle.text = selectedItem.Type;
            ownedText.text = "Owned : " + GameManager.instance.Foods.Find(f => f.Type == selectedItem.Type).Amount;
            descriptionTitle.text = food.Description;
            coinCost.text = food.Price.ToString();

            audioSource.PlayOneShot(buttonAudioClip);
        }
    }

}

