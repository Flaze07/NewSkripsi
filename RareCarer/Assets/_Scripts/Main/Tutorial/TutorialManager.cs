using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image image;
        [SerializeField] private GameObject prevButton;
        [SerializeField] private GameObject nextButton;
        [SerializeField] private GameObject closeButton;
        [SerializeField] private GameObject sectionButtons;

        [SerializeField] private List<TutorialSection> sections;

        private TutorialSection currentSection;
        private int currentSectionIndex;
        private bool firstTime = true;

        private void Start()
        {
            firstTime = PlayerPrefs.GetInt("Animal Count", -1) == -1;
            if (firstTime)
            {
                panel.SetActive(true);
                currentSectionIndex = 0;
                currentSection = sections[currentSectionIndex];
                currentSection.GoToPart(0);
                closeButton.SetActive(false);
                sectionButtons.SetActive(false);
                Setup();
            }
            else
            {
                panel.SetActive(false);
                closeButton.SetActive(true);
                sectionButtons.SetActive(true);
                HideAll();
            }
        }

        public void OpenPanel()
        {
            HideAll();
            closeButton.SetActive(true);
            sectionButtons.SetActive(true);
            panel.SetActive(true);
        }

        private void HideAll()
        {
            prevButton.SetActive(false);
            nextButton.SetActive(false);
            title.text = string.Empty;
            description.text = string.Empty;
            image.gameObject.SetActive(false);
        }

        private void Setup()
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true);
            if (currentSection.IsBeginning() && IsBeginning())
            {
                prevButton.SetActive(false);
            }
            if (currentSection.IsEnd() && IsEnd())
            {
                nextButton.SetActive(false);
            }

            title.text = currentSection.title;
            description.text = currentSection.currentPart.text;
            if(currentSection.currentPart.image == null)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = currentSection.currentPart.image;
                image.gameObject.SetActive(true);
            }
        }

        public void GoToNextPart()
        {
            if (currentSection.IsEnd())
            {
                GoToNextSection();
            }
            else
            {
                currentSection.GoToNextPart();
            }
            Setup();

            if (currentSection.IsEnd())
            {
                if (IsEnd())
                {
                    firstTime = false;
                    closeButton.SetActive(true);
                    nextButton.SetActive(false);
                }
            }
        }

        public void GoToPrevPart()
        {
            if (currentSection.IsBeginning())
            {
                GoToPrevSection();
            }
            else
            {
                currentSection.GoToPrevPart();
            }
            Setup();

            if (currentSection.IsBeginning())
            {
                if (IsBeginning())
                {
                    prevButton.SetActive(false);
                }
            }
        }

        private void GoToNextSection()
        {
            GoToSection(currentSectionIndex + 1);
            currentSection.GoToPart(0);
        }

        private void GoToPrevSection()
        {
            GoToSection(currentSectionIndex - 1);
            currentSection.GoToPart(currentSection.tutorialParts.Count - 1);
        }

        public void GoToSection(int index)
        {
            if (index >= sections.Count || index < 0)
            {
                Debug.LogWarning("WARNING: index out of bounds");
                return;
            }
            currentSection = sections[index];
            currentSectionIndex = index;
        }

        public void OpenSection()
        {
            currentSection.GoToPart(0);
            Setup();
        }

        private bool IsEnd()
        {
            return currentSectionIndex >= sections.Count - 1;
        }

        private bool IsBeginning()
        {
            return currentSectionIndex <= 0;
        }
    }
}