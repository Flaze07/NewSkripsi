using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRoomUI : MonoBehaviour
{
    [SerializeField]
    private GameObject closeParent;
    [SerializeField]
    private GameObject foodPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFoodPanel()
    {
        foodPanel.SetActive(true);
        closeParent.SetActive(true);
    }

    public void CloseFoodPanel()
    {
        foodPanel.SetActive(false);
        closeParent.SetActive(false);
    }
}
