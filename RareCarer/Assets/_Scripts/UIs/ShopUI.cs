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

    void Start()
    {
        GameManager.instance.Foods.ForEach(food =>
        {
            ShopItem shopItem = Instantiate(shopItemPrefab, shopItemParent.transform);
            Button btn = shopItem.GetComponent<Button>();
            btn.onClick.AddListener(() => BuyItem(food));
            Debug.Log(food.Sprite);
            shopItem.SetSprite(food.Sprite);
        });

        currencyText.text = "Currency: " + GameManager.instance.Currency.ToString();
    }

    public void BuyItem(Food food)
    {
        if(GameManager.instance.Currency >= food.Price)
        {
            GameManager.instance.Currency -= food.Price;
            GameManager.instance.Foods.Find(f => f.Type == food.Type).Amount++;
        }
    }
}

}

