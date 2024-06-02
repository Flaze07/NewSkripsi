using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RC
{

public class Animal : MonoBehaviour
{
    [SerializeField]
    private string name;
    public string Name => name;
    [SerializeField]
    private List<string> likedFood;
    private float happiness = 0;
    private float hunger = 50;
    private float cleanliness = 50;
    private float play = 50;
    
    [SerializeField]
    private SpriteRenderer sprite;

    /// <summary>
    /// This event will be called when the happiness of the animal changes
    /// Specifically at the value of 25, 50, 75, 100
    /// </summary>
    public UnityEvent<float, float> OnHappinessChange;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHappiness();
        UpdateStats();
    }

    public void Hide()
    {
        sprite.gameObject.SetActive(false);
    }
    public void Show()
    {
        sprite.gameObject.SetActive(true);
    }

    /// <summary>
    /// Feed the animal with the given food type
    /// if the animal likes the food, hunger will be increased by 40
    /// if the animal does not like the food, hunger will be increased by 10
    /// </summary>
    public void Feed(string foodType)
    {
        if(likedFood.Contains(foodType))
        {
            hunger += 40;
        }
        else
        {
            hunger += 10;
        }
    }

    /// <summary>
    /// Happiness will be increemented if all three values are above 50
    /// if all three values are below 25, happiness will be decremented
    /// if at least one value is above 50 while the others are below 25, happiness will not change
    /// if all values are between 25 and 50, happiness will not change
    /// </summary>
    private void UpdateHappiness()
    {
        if(hunger > 50 && cleanliness > 50 && play > 50)
        {
            float before = happiness;
            happiness += 1 * Time.deltaTime;
            OnHappinessChange.Invoke(before, happiness);
        }
        else if(hunger < 25 && cleanliness < 25 && play < 25)
        {
            float before = happiness;
            happiness -= 1 * Time.deltaTime;
            OnHappinessChange.Invoke(before, happiness);
        }
    }

    private void UpdateStats()
    {
        hunger -= Time.deltaTime / 120;
        cleanliness -= Time.deltaTime / 120;
        play -= Time.deltaTime / 120;
    }
}

}

