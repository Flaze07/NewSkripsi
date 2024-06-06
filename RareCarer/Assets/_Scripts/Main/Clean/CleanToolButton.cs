using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RC
{

public class CleanToolButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private CleanTool cleanTool;

    public void OnPointerDown(PointerEventData eventData)
    {
        SpawnCleanTool();
    }

        // Start i
    public void SpawnCleanTool()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos);
        CleanTool tool = Instantiate(cleanTool, canvas.transform);
        tool.Initialize(canvas);
        tool.transform.position = canvas.transform.TransformPoint(pos + tool.Offset);
    }
}

}

