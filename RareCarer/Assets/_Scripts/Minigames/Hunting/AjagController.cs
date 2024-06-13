using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

    public class Command
    {
        public string action;
        public float position;
    }

    public class AjagController : MonoBehaviour
    {
        [SerializeField]
        private GameObject standing;
        [SerializeField]
        private GameObject crouching;
        [SerializeField]
        private GameObject jumping;
        private Rigidbody2D rb;
        public bool crouchState;
        [SerializeField]
        private CheckGround checkGround;
        private float currentPos = 0;
        public float CurrentPos => currentPos;
        public List<Command> commands = new List<Command>();
        private float maxStamina = 1;
        private float currentStamina;
        public float CurrentStamina
        {
            get => currentStamina;
            set
            {
                currentStamina = Mathf.Clamp(value, 0, maxStamina);
            }
        }

        void Start()
        {
            currentStamina = maxStamina;
            rb = GetComponent<Rigidbody2D>();
        }

        public void Jump()
        {
            if (checkGround.IsGrounded)
            {
                rb.AddForce(new Vector2(0, HuntingManager.instance.JumpForce), ForceMode2D.Force);
            }
        }

        void Update()
        {
            HandleBoth();
            if (HuntingManager.instance.MainAjag == this)
            {
                HandleMain();
            }
            else
            {
                NonMain();
            }
        }

        private void HandleMain()
        {
            currentPos += Time.deltaTime;
            CurrentStamina -= HuntingManager.instance.StaminaDecrement * Time.deltaTime;
        }

        private void NonMain()
        {
            var main = HuntingManager.instance.MainAjag;
            CurrentStamina += HuntingManager.instance.StaminaIncrement * Time.deltaTime;
            currentPos = main.CurrentPos - ((main.transform.position.x - transform.position.x) / HuntingManager.instance.DelayValue);
            if (commands.Count == 0)
            {
                return;
            }
            if (currentPos >= commands[0].position)
            {
                if (commands[0].action == "jump")
                {
                    Jump();
                }
                if (commands[0].action == "crouch")
                {
                    Crouch();
                }
                if (commands[0].action == "stand")
                {
                    Stand();
                }
                commands.RemoveAt(0);
            }
        }

        private void HandleBoth()
        {
            if (checkGround.IsGrounded)
            {
                if (crouchState)
                {
                    crouching.SetActive(true);
                    standing.SetActive(false);
                    jumping.SetActive(false);
                }
                else if (checkGround.isGrounded)
                {
                    crouching.SetActive(false);
                    standing.SetActive(true);
                    jumping.SetActive(false);
                }
            }
            else
            {
                crouching.SetActive(false);
                standing.SetActive(false);
                jumping.SetActive(true);
            }
        }

        public void Crouch()
        {
            // standing.SetActive(false);
            // crouching.SetActive(true); 
            crouchState = true;
        }

        public void Stand()
        {
            crouchState = false;
            // standing.SetActive(true);
            // crouching.SetActive(false);
        }

        public void AddCommands(Command newCommand)
        {
            commands.Add(newCommand);
        }

        public void UpdatePos(float newPos)
        {
            currentPos = newPos;
        }

        public void Flash()
        {
            StartCoroutine(FlashRoutine());
        }

        public IEnumerator FlashRoutine()
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            var flashTime = HuntingManager.instance.DamageCooldown;
            var flashAmount = HuntingManager.instance.FlashAmount;
            var t = 0;
            while (t < flashAmount)
            {
                sprite.enabled = !sprite.enabled;
                t += 1;
                yield return new WaitForSeconds(flashTime / flashAmount);
            }
            sprite.enabled = true;
        }
    }

}

