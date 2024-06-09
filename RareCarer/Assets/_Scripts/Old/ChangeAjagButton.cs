using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting.Old
{

public class ChangeAjagButton : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private AjagController ajag;
    public void OnClick()
    {
        HuntingManager.instance.ChangeMainAjag(ajag);
    }

    void Update()
    {
        Vector2 movePos = worldToUISpace();
        var xPos = movePos.x;
        var currentPos = transform.position;
        transform.localPosition = new Vector3(xPos, currentPos.y, currentPos.z);
    }

    private Vector2 worldToUISpace()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(ajag.transform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, 
                                                                screenPos, 
                                                                canvas.worldCamera, 
                                                                out Vector2 movePos
                                                            );
        return movePos;
    }
}

}

