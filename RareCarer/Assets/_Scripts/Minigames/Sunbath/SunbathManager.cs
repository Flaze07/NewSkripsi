using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace RC.Sunbath
{
    
public class SunbathManager : MonoBehaviour
{
    public static SunbathManager instance;
    [SerializeField]
    private Slider timeProgressBar;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject star1;
    [SerializeField]
    private GameObject star2;
    [SerializeField]
    private GameObject star3;
    [SerializeField]
    private float timeLimit = 60f;
    private float currentTime = 0f;
    private float score;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        score = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        timeProgressBar.value = currentTime / timeLimit;
        scoreText.text = "Score: " + Mathf.RoundToInt(score) + "%";

        if(score >= 100)
        {
            // Win
        }
    }

    public void IncreaseScore(float amount)
    {
        score += amount;
        if(score >= 20)
        {
            star1.SetActive(true);
        }
        if(score >= 60)
        {
            star2.SetActive(true);
        }
        if(score >= 100)
        {
            star3.SetActive(true);
        }
    }
}

}