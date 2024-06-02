using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{

public class FoodPanel : MonoBehaviour
{
    [SerializeField]
    private FoodConsumable foodConsumablePrefab;
    [SerializeField]
    private GameObject foodConsumableParent;
    void OnEnable()
    {
        // create a new food consumable for each food item in the food manager
        foreach (var food in GameManager.instance.Foods)
        {
            FoodConsumable foodConsumable = Instantiate(foodConsumablePrefab, foodConsumableParent.transform);
            foodConsumable.SetFoodCount(food.Amount);
            foodConsumable.SetFoodImage(food.Sprite);
            Button btn = foodConsumable.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                if(food.Amount == 0)
                {
                    return;
                }
                GameManager.instance.CurrentAnimal.Feed(food.Type);
                food.Amount--;
            });
        }
    }

    /// <summary>
    /// clean up the food panel and remove all the food consumables
    /// </summary>
    void OnDisable()
    {
        foreach (Transform child in foodConsumableParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

}

