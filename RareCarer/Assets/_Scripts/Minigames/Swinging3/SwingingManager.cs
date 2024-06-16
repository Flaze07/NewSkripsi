using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace RC.Swinging3
{

    public class SwingingManager : MonoBehaviour
    {
        public static SwingingManager instance;

        [SerializeField]
        private float gravityValue;

        [SerializeField]
        private Camera activeCamera;
        [SerializeField]
        private float targetPlayerLocation;

        [SerializeField]
        private GameObject platformPrefab;
        [SerializeField]
        private float disBetweenPlat;
        [SerializeField]
        private float heightDiffPlat;
        [SerializeField]
        private int platCount;
        [SerializeField]
        private float minYPlatform;
        [SerializeField]
        private float maxYPlatform;

        [SerializeField]
        private Sprite starAchievedSprite;

        [SerializeField]
        private float playerTimer;
        [SerializeField]
        private Image playerTimerUI;
        [SerializeField]
        private OrangUtan playerCharacter;
        
        private Vector2 lastPlatPosition;
        private List<GameObject> activePlatform = new List<GameObject>();
        private float furthestXPosition;
        private int platformIndex;
        private bool gameEnded = false;

        private float currTimer;
        private float score;

        //UIS
        [SerializeField]
        private GameObject star1;
        [SerializeField]
        private GameObject star2;
        [SerializeField]
        private GameObject star3;
        [SerializeField]
        private GameObject gameEndPanel;
        [SerializeField]
        private TextMeshProUGUI scoreCounter;

        public float GravityValue
        {
            get
            {
                return gravityValue;
            }
        }

        private int star = 0;


        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            CreatePlatform(platCount);
            playerCharacter.Attach(activePlatform[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameEnded)
            {
                return;
            }

            PlayerTimer();

            //move platforms
            if(playerCharacter.attachedUnit)
            {
                while (activePlatform.IndexOf(playerCharacter.attachedUnit) != 0)
                {
                    reusePlatform(activePlatform[0]);
                    ChangeScore();
                }
            }

            CheckGameEnd();
        }

        private void PlayerTimer()
        {
            //timer
            currTimer += Time.deltaTime;

            if (currTimer > playerTimer)
            {
                //unattach the player and DIE
                playerCharacter.attached = false;
            }

            //reset the timer
            if (!playerCharacter.Attached)
            {
                currTimer = 0;
            }

            if(playerCharacter.attached)
            {
                GameObject timerParent = playerTimerUI.transform.parent.gameObject;
                timerParent.SetActive(true);

                Vector3 timerOffset;
                if(playerCharacter.attachedUnit.transform.position.y + 2f > maxYPlatform)
                {
                    timerOffset = new Vector3(0.5f, -3f, 0);
                }
                else
                {
                    timerOffset = new Vector3(0.5f, 1, 0);
                }
                Vector3 timerTargetPosition = Camera.main.WorldToScreenPoint(playerCharacter.attachedUnit.transform.position + timerOffset);
                timerParent.transform.position = timerTargetPosition;

                float timerProgress = 1 - currTimer / playerTimer;
                playerTimerUI.fillAmount = timerProgress;
            }
            else
            {
                playerTimerUI.transform.parent.gameObject.SetActive(false);
            }

        }

        private void ChangeScore()
        {
            score++;

            if (score >= 5)
            {
                //star1.SetActive(true);
                star1.GetComponent<UnityEngine.UI.Image>().sprite = starAchievedSprite;
            }
            if (score >= 10)
            {
                //star2.SetActive(true);
                star2.GetComponent<UnityEngine.UI.Image>().sprite = starAchievedSprite;
            }
            if (score >= 15)
            {
                //star3.SetActive(true);
                star3.GetComponent<UnityEngine.UI.Image>().sprite = starAchievedSprite;
            }

            scoreCounter.text = score.ToString();
        }

        private void CheckGameEnd()
        {
            if (playerCharacter.transform.position.y < minYPlatform * 1.5f)
            {
                GameEnd();
            }

            if(score >= 15)
            {
                GameEnd();
            }
        }

        private void GameEnd()
        {
            gameEndPanel.SetActive(true);
            // if (score >= 20)
            // {
            //     GameManager.instance.Currency += 30;
            // }
            // else if (score >= 60)
            // {
            //     GameManager.instance.Currency += 20;
            // }
            // else if (score >= 100)
            // {
            //     GameManager.instance.Currency += 10;
            // }
            if(score >= 15)
            {
                star = 3;
            }
            else if(score >= 10)
            {
                star = 2;
            }
            else if(score >= 5)
            {
                star = 1;
            }

            GameManager.instance.GiveCurrency(star);
            GameManager.instance.CurrentAnimal.Play += 60;
            AnimalMinigame minigameComp = GameManager.instance.CurrentAnimal.gameObject.GetComponent<AnimalMinigame>();
            if(star > minigameComp.UnlockedMinigames[0].starAchieved)
            {
                minigameComp.UnlockedMinigames[0].starAchieved = star;
            }
            gameEnded = true;
        }



        private void FixedUpdate()
        {

            if (playerCharacter.transform.position.x + targetPlayerLocation > furthestXPosition)
            {
                furthestXPosition = playerCharacter.transform.position.x + targetPlayerLocation;
            }

            //camera position
            float targetCameraPosition = Mathf.Lerp(activeCamera.transform.position.x, furthestXPosition, 0.1f);

            activeCamera.transform.position = new Vector3(targetCameraPosition, activeCamera.transform.position.y, activeCamera.transform.position.z);
        }


        //generate more level
        public void reusePlatform(GameObject reusePlatform)
        {
            GameObject platform = reusePlatform;

            float xPosition = lastPlatPosition.x + disBetweenPlat * platformIndex;
            platformIndex++;
            float yPosition = lastPlatPosition.y + UnityEngine.Random.Range(-heightDiffPlat,heightDiffPlat);

            while(yPosition > maxYPlatform || yPosition < minYPlatform)
            {
                yPosition = lastPlatPosition.y + UnityEngine.Random.Range(-heightDiffPlat, heightDiffPlat);
            }


            //create more platform
            if(activePlatform.Contains(platform))
            {
                activePlatform.Remove(platform); 
            }

            platform.transform.position = new Vector3(xPosition, yPosition, 0);
            activePlatform.Add(platform);
        }

        public void CreatePlatform(int ammount)
        {

            for(int i =0; i < ammount; i ++)
            {
                GameObject platform = Instantiate(platformPrefab,new Vector3(0,0,0),quaternion.identity);

                reusePlatform(platform);
            }
        }
    }

}

