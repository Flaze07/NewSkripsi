using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RC.minigameUI;


namespace RC.Sunbath
{

    public class SunbathManager : MonoBehaviour
    {
        public static SunbathManager instance;
        //[SerializeField]
        //private Slider timeProgressBar;
        //[SerializeField]
        //private TextMeshProUGUI scoreText;

        [SerializeField]
        private Sprite starAchievedSprite;
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
        [SerializeField]
        private MinigameUIBar bar;
        [SerializeField]
        private TextMeshProUGUI timeCounter;

        private float currentTime = 0f;
        private float score;
        private bool gameEnded = false;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            //score = 90;
            //star1.SetActive(false);
            //star2.SetActive(false);
            //star3.SetActive(false);
            gameEndPanel.SetActive(false);
        }

        void Update()
        {
            if (gameEnded)
            {
                return;
            }
            currentTime += Time.deltaTime;
            //timeProgressBar.value = currentTime / timeLimit;
            //scoreText.text = "Score: " + Mathf.RoundToInt(score) + "%";


            float tempTime = Mathf.Ceil( Mathf.Max(0, (timeLimit - currentTime)) );
            timeCounter.text = tempTime.ToString();

            if (score >= 100)
            {
                GameEnd();
            }

            if (currentTime >= timeLimit)
            {
                GameEnd();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                IncreaseScore(10);
            }
        }

        private void GameEnd()
        {
            gameEndPanel.SetActive(true);
            
            int star = 0;
            if (score >= 100)
            {
                GameManager.instance.Currency += 30;
                star = 3;
            }
            else if (score >= 60)
            {
                GameManager.instance.Currency += 20;
                star = 2;
            }
            else if (score>=20)
            {
                GameManager.instance.Currency += 10;
                star = 1;
            }
            
            GameManager.instance.CurrentAnimal.Play += 30;
            AnimalMinigame minigameComp = GameManager.instance.CurrentAnimal.gameObject.GetComponent<AnimalMinigame>();
            if(star > minigameComp.UnlockedMinigames[0].starAchieved)
            {
                minigameComp.UnlockedMinigames[0].starAchieved = star;
            }

            gameEnded = true;
        }

        public void IncreaseScore(float amount)
        {
            score += amount;
            if (score >= 20)
            {
                //star1.SetActive(true);
                star1.GetComponent<Image>().sprite = starAchievedSprite;
            }
            if (score >= 60)
            {
                //star2.SetActive(true);
                star2.GetComponent<Image>().sprite = starAchievedSprite;
            }
            if (score >= 99)
            {
                //star3.SetActive(true);
                star3.GetComponent<Image>().sprite = starAchievedSprite;
            }
            score = Mathf.Min(Mathf.Max(score, 0),100);
;
            bar.ChangeProgress(score);
        }
    }

}