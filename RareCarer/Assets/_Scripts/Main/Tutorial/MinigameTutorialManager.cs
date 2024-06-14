using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    public class MinigameTutorialManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> minigameGameObjects;
        public void StartGame()
        {
            foreach (GameObject gameObject in minigameGameObjects)
            {
                gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}