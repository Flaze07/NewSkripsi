using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

namespace RC
{

public enum PanelSwitchDirection
{
    Left,
    Right
}

public class PanelSwitchManager : MonoBehaviour
{
    [Header("OTHER UI")]
    [SerializeField]
    private GameObject parentCanvas;
    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button rightButton;
    
    [Header("PANELS")]
    [SerializeField]
    private List<GameObject> panels;

    [Header("CURRENT PANEL PROPERTIES")]
    [SerializeField]
    private GameObject currentPanel;

    [SerializeField]
    private int currentIdx;
    
    [Header("SWITCH PANELS ORIGIN / TARGET")]
    [SerializeField]
    private GameObject fromLeft;

    [SerializeField]
    private GameObject fromRight;

    [SerializeField]
    private float switchDelay;

    void Start()
    {
        // StartCoroutine(Test());
    }

    void Update()
    {
    }

    private IEnumerator Test()
    {
        yield return new WaitForNextFrameUnit();
        SwitchPanel(PanelSwitchDirection.Left);
    }

    public void SwitchPanel(PanelSwitchDirection direction)
    {
        if(direction == PanelSwitchDirection.Left)
        {
            SwitchPanelLeft();
        }
        if(direction == PanelSwitchDirection.Right)
        {
            SwitchPanelRight();
        }
    }

    public void SwitchPanelLeft()
    {
        ChangeButtonState(false);
        int newIdx;
        if(currentIdx == 0)
        {
            newIdx = currentIdx = panels.Count - 1;
        }
        else
        {
            newIdx = currentIdx - 1;
        }

        currentIdx = newIdx;

        var newPanel = Instantiate(panels[newIdx], fromLeft.transform.position, Quaternion.identity, parentCanvas.transform);
        StartCoroutine(AnimateSwitchLeft(newPanel));
    }

    private IEnumerator AnimateSwitchLeft(GameObject newPanel)
    {
        RectTransform newTransform = newPanel.GetComponent<RectTransform>();
        RectTransform currentTransform = currentPanel.GetComponent<RectTransform>();
        Vector2 centerPos = currentTransform.anchoredPosition;
        Vector2 rightPos = fromRight.GetComponent<RectTransform>().anchoredPosition;

        float switchSpeed = (centerPos.x - rightPos.x) / switchDelay;

        while(newTransform.anchoredPosition.x < centerPos.x)
        {
            float moveAmount = switchSpeed * Time.deltaTime;
            
            Vector2 temp1 = newTransform.anchoredPosition;
            Vector2 temp2 = currentTransform.anchoredPosition;

            temp1.x -= moveAmount;
            temp2.x -= moveAmount;

            newTransform.anchoredPosition = temp1;
            currentTransform.anchoredPosition = temp2;

            yield return new WaitForNextFrameUnit();
        }
        newTransform.anchoredPosition = centerPos;
        Destroy(currentPanel);
        currentPanel = newPanel;
        ChangeButtonState(true);
    }

    public void SwitchPanelRight()
    {
        ChangeButtonState(false);
        int newIdx;
        if(currentIdx == panels.Count - 1)
        {
            newIdx = 0;
        }
        else
        {
            newIdx = currentIdx + 1;
        }

        currentIdx = newIdx;

        var newPanel = Instantiate(panels[newIdx], fromRight.transform.position, Quaternion.identity, parentCanvas.transform);
        StartCoroutine(AnimateSwitchRight(newPanel));
    }

    private IEnumerator AnimateSwitchRight(GameObject newPanel)
    {
        RectTransform newTransform = newPanel.GetComponent<RectTransform>();
        RectTransform currentTransform = currentPanel.GetComponent<RectTransform>();
        Vector2 centerPos = currentTransform.anchoredPosition;
        Vector2 rightPos = fromRight.GetComponent<RectTransform>().anchoredPosition;

        float switchSpeed = (centerPos.x - rightPos.x) / switchDelay;

        while(newTransform.anchoredPosition.x > centerPos.x)
        {
            float moveAmount = switchSpeed * Time.deltaTime;
            
            Vector2 temp1 = newTransform.anchoredPosition;
            Vector2 temp2 = currentTransform.anchoredPosition;

            temp1.x += moveAmount;
            temp2.x += moveAmount;

            newTransform.anchoredPosition = temp1;
            currentTransform.anchoredPosition = temp2;

            yield return new WaitForNextFrameUnit();
        }
        newTransform.anchoredPosition = centerPos;
        Destroy(currentPanel);
        currentPanel = newPanel;
        ChangeButtonState(true);
    }

    private void ChangeButtonState(bool state)
    {
        leftButton.interactable = state;
        rightButton.interactable = state;
    }
}
    
}
