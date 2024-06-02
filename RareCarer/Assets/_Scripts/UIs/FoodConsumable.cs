using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RC
{
    
public class FoodConsumable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI foodCount;
    [SerializeField]
    private Image foodImage;

    public TextMeshProUGUI FoodCount => foodCount;
    public Image FoodImage => foodImage;

    public void SetFoodCount(int count)
    {
        foodCount.text = count.ToString();
    }

    public void SetFoodImage(Sprite sprite)
    {
        foodImage.sprite = sprite;
    }
}

}

