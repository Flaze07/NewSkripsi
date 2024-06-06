using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RC
{

public class AnimalCleanliness : MonoBehaviour
{
    [SerializeField]
    private Animal animal;
    [SerializeField]
    private SpriteRenderer animalDirtSprite;
    private int internalCleanliness;
    void Start()
    {
        internalCleanliness = Mathf.CeilToInt(animal.Cleanliness) + 1;
    }
    public void UpdateCleanliness()
    {
        if(internalCleanliness != Mathf.CeilToInt(animal.Cleanliness))
        {
            internalCleanliness = Mathf.CeilToInt(animal.Cleanliness);
            animalDirtSprite.color = new Color(1, 1, 1, 1 - animal.Cleanliness / 100);
        }
    }
}

}

