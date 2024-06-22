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
    [SerializeField]
    private GameObject notEnoughFoodPanel;

        [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip eatingAudioClip;
    [SerializeField]
    private AudioClip notEnoughFoodAudioClip;

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
                    notEnoughFoodPanel.SetActive(true);
                    audioSource.PlayOneShot(notEnoughFoodAudioClip);
                    return;
                }
                GameManager.instance.CurrentAnimal.Feed(food.Type);
                food.Amount--;
                foodConsumable.SetFoodCount(food.Amount);
                audioSource.PlayOneShot(eatingAudioClip);
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

