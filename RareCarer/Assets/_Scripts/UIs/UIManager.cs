using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RC
{

    public class UIManager : MonoBehaviour
    {
        public void LeftSlideButton()
        {
            if (GameManager.instance.IsSwitching) return;
            GameManager.instance.SwitchAnimal(-1);
        }

        public void RightSlideButton()
        {
            if (GameManager.instance.IsSwitching) return;
            GameManager.instance.SwitchAnimal(1);
        }

        public void ClosePanel(GameObject go)
        {
            //Debug.Log(go);
            go.SetActive(false);
        }

        public void OpenPanel(GameObject go)
        {
            go.SetActive(true);
        }

        public void LoadScene(string sceneName)
        {
            GameManager.instance.SaveData();
            SceneManager.LoadScene(sceneName);
        }
    }

}

