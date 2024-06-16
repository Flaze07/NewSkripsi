using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

namespace RC.Hunting
{
    public class HuntingManager : MonoBehaviour
    {
        public static HuntingManager instance;
        [SerializeField]
        private HuntingUI huntingUI;
        [SerializeField]
        private GameObject GameEndPanel;

        [SerializeField]
        private float jumpForce;
        public float JumpForce => jumpForce;
        [SerializeField]
        private Deer deer;
        public Deer Deer => deer;

        [SerializeField]
        private AjagController mainAjag;
        public AjagController MainAjag => mainAjag;
        [SerializeField]
        private GameObject mainAjagBtn;
        public GameObject MainAjagBtn { get => mainAjagBtn; set => mainAjagBtn = value; }
        [SerializeField]
        private GameObject ajagParent;
        [SerializeField]
        private float ajagSpeed;
        [SerializeField]
        private int speedValue;
        public int SpeedValue => speedValue;
        [SerializeField]
        private AnimationCurve speedCurve;
        [SerializeField]
        private float delayValue;
        public float DelayValue => delayValue;

        [SerializeField]
        private float changeAjagTime;
        public float ChangeAjagTime => changeAjagTime;
        public bool IsChanging { get; private set; }

        [SerializeField]
        private float staminaIncrement;
        public float StaminaIncrement => staminaIncrement;
        [SerializeField]
        private float staminaDecrement;
        public float StaminaDecrement => staminaDecrement;

        [SerializeField]
        private float flashAmount;
        public float FlashAmount => flashAmount;
        [SerializeField]
        private float damageCooldown;

        [SerializeField]
        private float distance;
        public float Distance => distance;
        [SerializeField]
        private float distancePerUnit;
        [SerializeField]
        private float lowestDistance;

        [SerializeField]
        private float gameTimer;
        public float GameTimer => gameTimer;
        private float currentGameTime;
        public float CurrentGameTime => currentGameTime;
        private int totalStar;
        public int TotalStar => totalStar;

        private bool gameEnded = false;

        public float DamageCooldown => damageCooldown;
        private float damageCooldownCount = 0;
        public bool IsDamaged
        {
            get
            {
                return damageCooldownCount > 0;
            }
        }

        private bool initialized;
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
                deer.transform.position = new Vector3(ajagParent.transform.position.x + (distance/ distancePerUnit),deer.transform.position.y, deer.transform.position.z);
                huntingUI.Initialize(this);
                currentGameTime = gameTimer;
                lowestDistance = distance;
                GameEndPanel.SetActive(false);
                gameEnded = false;
                initialized = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if(initialized)
            {
                if (damageCooldownCount > 0)
                {
                    damageCooldownCount -= Time.deltaTime;
                }


                currentGameTime -= Time.deltaTime;

                CheckEnding();
                ManageSpeed();
                CheckDistance();

            }
        }

        public void CheckDistance()
        {
            distance = (deer.transform.position.x - (ajagParent.transform.position.x + 5)) * distancePerUnit;
            if (distance < lowestDistance) lowestDistance = distance;

            if(lowestDistance <= 0)
            {
                totalStar = 3;
            }
            else if (lowestDistance <= 15)
            {
                totalStar = 2;  
            }
            else if (lowestDistance <= 30)
            {
                totalStar = 1;
            }
        }

        public void ManageSpeed()
        {
            if(mainAjag.CurrentStamina > 0.60f)
            {
                speedValue = 3;
            }
            else if (MainAjag.CurrentStamina > 0.25f )
            {
                speedValue = 2;
            }
            else if (mainAjag.CurrentStamina > 0f)
            {
                speedValue = 1;
            }
            else
            {
                speedValue = 0;
            }
            
            float finalSpeed = (speedCurve.Evaluate(SpeedValue)) * ajagSpeed;

            deer.MoveBackward(finalSpeed / distancePerUnit * Time.deltaTime);
        }

        public void CheckEnding()
        {
            if(lowestDistance <= 0 || currentGameTime <= 0)
            {
                //end game
                GameEnd();
            }
        }

        void OnJump2()
        {
            mainAjag.Jump();
            PublishCommand("jump");
        }

        void OnCrouch(InputValue value)
        {
            if (value.isPressed)
            {
                mainAjag.Crouch();
                PublishCommand("crouch");
            }
            else
            {
                mainAjag.Stand();
                PublishCommand("stand");
            }
        }

        private void PublishCommand(string command)
        {
            foreach (Transform ajag in ajagParent.transform)
            {
                AjagController ajagController = ajag.GetComponent<AjagController>();
                if (ajagController != mainAjag)
                {
                    ajagController.AddCommands(new Command { action = command, position = mainAjag.CurrentPos });
                }
            }
        }

        public void ChangeMainAjag(AjagController ajag)
        {
            IsChanging = true;
            StartCoroutine(AnimateChangeAjag(mainAjag.transform, ajag.transform));
            ajag.UpdatePos(mainAjag.CurrentPos);
            mainAjag = ajag;
        }

        private IEnumerator AnimateChangeAjag(Transform from, Transform to)
        {
            Vector3 fromPos = from.position;
            Vector3 toPos = to.position;

            float t = 0;
            while (t < changeAjagTime)
            {
                t += Time.deltaTime;
                Vector3 fromVec = from.position;
                Vector3 toVec = to.position;
                fromVec.x = Mathf.Lerp(fromPos.x, toPos.x, t / changeAjagTime);
                toVec.x = Mathf.Lerp(toPos.x, fromPos.x, t / changeAjagTime);
                from.position = fromVec;
                to.position = toVec;
                yield return null;
            }
            IsChanging = false;
        }

        public void AjagDamaged()
        {
            damageCooldownCount = damageCooldown;
        }

        public void GameEnd()
        {
            if(!gameEnded)
            {
                GameEndPanel.SetActive(true);

                GameManager.instance.GiveCurrency(totalStar);

                GameManager.instance.CurrentAnimal.Play += 60;
                AnimalMinigame minigameComp = GameManager.instance.CurrentAnimal.gameObject.GetComponent<AnimalMinigame>();
                if (totalStar > minigameComp.UnlockedMinigames[0].starAchieved)
                {
                    minigameComp.UnlockedMinigames[0].starAchieved = totalStar;
                }

                gameEnded = true;

            }
        }

    }

}

