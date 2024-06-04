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
    [SerializeField]
    private GameObject gameEndPanel;
    private float currentTime = 0f;
    private float score;
    private bool gameEnded = false;

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
        score = 90;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        gameEndPanel.SetActive(false);
    }

    void Update()
    {
        if(gameEnded)
        {
            return;
        }
        currentTime += Time.deltaTime;
        timeProgressBar.value = currentTime / timeLimit;
        scoreText.text = "Score: " + Mathf.RoundToInt(score) + "%";

        if(score >= 100)
        {
            GameEnd();
        }

        if(currentTime >= timeLimit)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        gameEndPanel.SetActive(true);
        if(star3.activeSelf)
        {
            GameManager.instance.Currency += 30;
        }
        else if(star2.activeSelf)
        {
            GameManager.instance.Currency += 20;
        }
        else if(star1.activeSelf)
        {
            GameManager.instance.Currency += 10;
        }
        GameManager.instance.CurrentAnimal.Play += 30;
        gameEnded = true;
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