using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.BuiltIn;
using UnityEngine;

namespace RC
{

[Serializable]
class StringGameObjectList
{
    [SerializeField]
    private string key;
    [SerializeField]
    private GameObject value;

    public string Key => key;
    public GameObject Value => value;
}

public class AnimalRoomUI : MonoBehaviour
{
    [SerializeField]
    private GameObject interactOptionPanel;
    [SerializeField]
    private GameObject interactOpenedPanel;
    [SerializeField]
    private List<StringGameObjectList> interactPanels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractOptionPanel()
    {
        interactOptionPanel.SetActive(true);
        interactOpenedPanel.SetActive(false);
    }

    public void ShowInteractOpenedPanel(string interactPanelName)
    {
        interactOptionPanel.SetActive(false);
        interactOpenedPanel.SetActive(true);
        // interactPanels[interactPanelName].SetActive(true);
        foreach (var panel in interactPanels)
        {
            if (panel.Key != interactPanelName)
            {
                panel.Value.SetActive(false);
            }
            else
            {
                panel.Value.SetActive(true);
            }
        }
    }

    public void ChangeInteractPanel(string interactPanelName)
    {
        // interactPanels[interactPanelName].SetActive(true);
        foreach (var panel in interactPanels)
        {
            if (panel.Key != interactPanelName)
            {
                panel.Value.SetActive(false);
            }
            else
            {
                panel.Value.SetActive(true);
            }
        }
    }
}

}

