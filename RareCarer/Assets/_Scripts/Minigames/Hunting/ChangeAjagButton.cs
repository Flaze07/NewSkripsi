using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class ChangeAjagButton : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private AjagController ajag;
    [SerializeField]
    private Transform staminaBar;
    public void OnClick()
    {
        if(HuntingManager.instance.IsChanging)
        {
            return;
        }
        HuntingManager.instance.ChangeMainAjag(ajag);
        StartCoroutine(SwitchPosition());
    }

    void Update()
    {
        var scale = staminaBar.localScale;
        scale.x = ajag.CurrentStamina;
        staminaBar.localScale = scale;
    }

    private IEnumerator SwitchPosition()
    {
        var mainBtn = HuntingManager.instance.MainAjagBtn.transform;
        var btn = transform;

        Vector3 mainBtnPos = mainBtn.position;
        Vector3 btnPos = btn.position;

        float t = 0;
        float switchTime = HuntingManager.instance.ChangeAjagTime * 0.7f;
        while(t < switchTime)
        {
            t += Time.deltaTime;
            mainBtn.position = Vector3.Lerp(mainBtnPos, btnPos, t / switchTime);
            btn.position = Vector3.Lerp(btnPos, mainBtnPos, t / switchTime);
            yield return null;
        }

        HuntingManager.instance.MainAjagBtn = this.gameObject;
    }
}

}

