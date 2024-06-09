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
    private AjagController mainAjag;
    public AjagController MainAjag => mainAjag;
    [SerializeField]
    private GameObject ajagParent;
    [SerializeField]
    private float delayValue;
    public float DelayValue => delayValue;
    [SerializeField]
    private float changeAjagTime;
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
            from.position = Vector3.Lerp(fromPos, toPos, t / changeAjagTime);
            to.position = Vector3.Lerp(toPos, fromPos, t / changeAjagTime);
            yield return null;
        }
    }
}

}

