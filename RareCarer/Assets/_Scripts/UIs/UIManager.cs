using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{

public class UIManager : MonoBehaviour
{
    public void LeftSlideButton()
    {
        if(GameManager.instance.IsSwitching) return;
        GameManager.instance.SwitchAnimal(-1);
    }

    public void RightSlideButton()
    {
        if(GameManager.instance.IsSwitching) return;
        GameManager.instance.SwitchAnimal(1);
    }
    
    public void ClosePanel(GameObject go)
    {
        go.SetActive(false);
    }

    public void OpenPanel(GameObject go)
    {
        go.SetActive(true);
    }
}

}
