using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private float currentGameTime;
        public float CurrentGameTime => currentGameTime;
        private int totalStar;
        public int TotalStar => totalStar;

        public float DamageCooldown => damageCooldown;
        private float damageCooldownCount = 0;
        public bool IsDamaged
        {
            get
            {
                return damageCooldownCount > 0;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
                deer.transform.position = new Vector3(ajagParent.transform.position.x + (distance/ distancePerUnit),deer.transform.position.y, deer.transform.position.z);
                huntingUI.Initialize(this);
                currentGameTime = gameTimer;
                GameEndPanel.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if (damageCooldownCount > 0)
            {
                damageCooldownCount -= Time.deltaTime;
            }

            float currAjagSpeed = 0;
            if(MainAjag.CurrentStamina > 0.6f)
            {
                currAjagSpeed = ajagSpeed;
            }
            else if (mainAjag.CurrentStamina > 0.3f)
            {
                currAjagSpeed = ajagSpeed/2f;
            }
            deer.MoveBackward(currAjagSpeed / distancePerUnit * Time.deltaTime);

            distance = (deer.transform.position.x - (ajagParent.transform.position.x + 1)) * distancePerUnit;
            if(distance < lowestDistance) lowestDistance = distance;

            currentGameTime -= Time.deltaTime;

            CheckEnding();
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
            GameEndPanel.SetActive(true);
        }
    }

}

