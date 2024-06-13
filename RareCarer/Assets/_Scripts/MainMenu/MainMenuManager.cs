using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace RC.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button achievementButton;

        [SerializeField] private AchievementUI achievementManager;

        public void OpenAchievement()
        {
            achievementManager.gameObject.SetActive(true);
            achievementManager.Initialize();
        }
        
        public void CloseAchievement()
        {
            achievementManager.CloseAchievement();
            achievementManager.gameObject.SetActive(false);
        }
        
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
