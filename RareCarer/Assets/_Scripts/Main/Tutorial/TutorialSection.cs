using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    [Serializable]
    public class TutorialSection
    {
        [Serializable]
        public struct TutorialPart
        {
            public string text;
            public Sprite image;
        }

        public string title;
        public List<TutorialPart> tutorialParts;
        [HideInInspector] public TutorialPart currentPart;
        [HideInInspector] public int currentPartIndex;

        public void GoToPart(int index)
        {
            if (index >= tutorialParts.Count || index < 0)
            {
                Debug.LogWarning("WARNING: index out of bounds");
                return;
            }
            currentPart = tutorialParts[index];
            currentPartIndex = index;
        }

        public void GoToNextPart()
        {
            GoToPart(currentPartIndex + 1);
        }

        public void GoToPrevPart()
        {
            GoToPart(currentPartIndex - 1);
        }

        public bool IsEnd()
        {
            return currentPartIndex >= tutorialParts.Count - 1;
        }

        public bool IsBeginning()
        {
            return currentPartIndex <= 0;
        }
    }
}