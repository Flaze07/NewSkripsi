using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC.Hunting
{
public class HuntingManager : MonoBehaviour
{
    public static HuntingManager instance;
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
    public GameObject MainAjagBtn { get => mainAjagBtn; set => mainAjagBtn = value;}
    [SerializeField]
    private GameObject ajagParent;
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
    // Start is called before the first frame update
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
    }

    void OnJump2()
    {
        mainAjag.Jump();
        PublishCommand("jump");
    }

    void OnCrouch(InputValue value)
    {
        if(value.isPressed)
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
        foreach(Transform ajag in ajagParent.transform)
        {
            AjagController ajagController = ajag.GetComponent<AjagController>();
            if(ajagController != mainAjag)
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
        while(t < changeAjagTime)
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
}

}

