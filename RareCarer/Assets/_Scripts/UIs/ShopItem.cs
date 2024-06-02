using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image image;
    
    private void Start()
    {
        // image = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}

}

