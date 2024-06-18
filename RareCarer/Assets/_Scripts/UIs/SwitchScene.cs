using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RC
{

public class SwitchScene : MonoBehaviour
{
    public void SwitchToScene(string sceneName)
    {
        GameManager.instance.SaveData();
        SceneManager.LoadScene(sceneName);
    }
}

}

