using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RC
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
    private List<Animal> availableAnimals = new();
    [SerializeField]
    private Animal currentAnimal;
    public Animal CurrentAnimal => currentAnimal;
    [SerializeField]
    private TextMeshProUGUI currencyText;
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
            currencyText.text = "Currency: " + currency.ToString();
        }
    }
    [SerializeField]
    private List<Food> foods;
    public List<Food> Foods => foods;
    [SerializeField]
    private SwitchAnimal switchAnimal;
    public bool IsSwitching { get; set; } = false;
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
        StartCoroutine(LateStart());
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => FindCurrencyText();
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        availableAnimals.Add(AnimalParent.instance.Animals[0]);
        availableAnimals.Add(AnimalParent.instance.Animals[1]);
        currentAnimal = availableAnimals[0];
    }

    private void FindCurrencyText()
    {
        var gb = GameObject.Find("CoinAmount");
        if (gb != null)
        {
            currencyText = gb.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAnimal(int direction)
    {
        if(availableAnimals.Count == 1)
        {
            return;
        }

        IsSwitching = true;

        int idx = availableAnimals.IndexOf(currentAnimal);
        int nextIdx = idx + direction;

        if(nextIdx < 0)
        {
            nextIdx = availableAnimals.Count - 1;
        }
        else if(nextIdx >= availableAnimals.Count)
        {
            nextIdx = 0;
        }

        Animal nextAnimal = availableAnimals[nextIdx];
        switchAnimal.Switch(direction, currentAnimal, nextAnimal);
        currentAnimal = availableAnimals[nextIdx];
    }
}

}

