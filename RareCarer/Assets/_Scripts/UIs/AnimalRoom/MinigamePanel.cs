using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RC
{

public class MinigamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject minigameButtonParent;
    [SerializeField]
    private GameObject minigameButtonPrefab;
    [SerializeField]
    private Sprite star;
    void OnEnable()
    {
        var animalMinigame = GameManager.instance.CurrentAnimal.GetComponent<AnimalMinigame>();
        foreach(var minigame in animalMinigame.UnlockedMinigames)
        {
            GameObject minigameButton = Instantiate(minigameButtonPrefab, minigameButtonParent.transform);
            minigameButton.GetComponent<MinigameButton>().setStars(minigame.starAchieved);
            var image = minigameButton.GetComponent<Image>();
            image.sprite = minigame.MinigameIcon;
            Button btn = minigameButton.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                SceneManager.LoadScene(minigame.MinigameSceneName);
            });

            for(int i = 0; i < minigame.starAchieved; ++i)
            {
                var child = minigameButton.transform.GetChild(i).GetComponent<Image>();
                child.sprite = star;
            }
        }
    }

    void OnDisable()
    {
        foreach (Transform child in minigameButtonParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

}

