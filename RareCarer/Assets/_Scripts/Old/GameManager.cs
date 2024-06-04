using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RC.Old
{

[Serializable]
public class Food
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int amount;
    [SerializeField]
    private int price;
    [SerializeField]
    private Sprite sprite;

    public string Type => type;
    public int Amount
    {
        get => amount;
        set => amount = value;
    }
    public int Price => price;
    public Sprite Sprite => sprite;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // [SerializeField]
    // private Animal[] animalsPrefab;
    private Animal[] animals;
    [SerializeField]
    private Animal currentAnimal;
    public Animal CurrentAnimal => currentAnimal;
    [SerializeField]
    private int currency = 100;
    public int Currency
    {
        get 
        {
            return currency;
        }
        set 
        {
            currency = value;
            var uis = GameObject.FindGameObjectsWithTag("UI");
            var currencyText = Array.Find(uis, ui => ui.name == "CurrencyText").GetComponent<TMPro.TextMeshProUGUI>();
            if(currencyText != null)
            {

                currencyText.text = "Currency: " + currency.ToString();
            }
        }
    }
    [SerializeField]
    private List<Food> foods;
    public List<Food> Foods => foods;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}

