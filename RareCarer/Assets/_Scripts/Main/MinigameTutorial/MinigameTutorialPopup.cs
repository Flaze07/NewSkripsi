using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class MinigameTutorialPopup : MonoBehaviour
    {
        public delegate void PopupClose();
        public PopupClose popupClose;

        [SerializeField] private TextMeshProUGUI popupTitle;
        [SerializeField] private Image popupImage;
        [SerializeField] private TextMeshProUGUI popupDescription;
        [SerializeField] private MinigameTutorialCarouselIndicator carouselIndicator;
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button closeButton;

        private MinigameInfo minigameSelected;
        private int index = 0;

        public void Initialize(MinigameInfo minigameInfo)
        {
            //T will be minigame info later on
            carouselIndicator.Initialize(minigameInfo);
            carouselIndicator.ChangeIndex(0);

            index = 0;
            AddIndex(0);
        }

        public void AddIndex(int indexChange)
        {
            index += indexChange;

            //close if its above the index
            if (index > minigameSelected.description.Count)
            {
                ClosePopup();
            }


            //remove the left button image
            if(index == 0)
            {
                leftButton.gameObject.SetActive(false);
            }
            else
            {
                leftButton.gameObject.SetActive(true);
            }

            //turn on the close button
            if (index == minigameSelected.description.Count)
            {
                closeButton.gameObject.SetActive(true);
            }
            else
            {
                closeButton.gameObject.SetActive(false);
            }

            popupDescription.text = minigameSelected.description[index];
            popupImage.sprite = minigameSelected.images[index];

            carouselIndicator.ChangeIndex(index);
        }

        public void ClosePopup()
        {
            if(popupClose != null)
            {
                popupClose.Invoke();
            }

            Destroy(this.gameObject);
        }

    }

    [CreateAssetMenu(menuName = "ScriptableObject/MinigameInfo")]
    public class MinigameInfo :ScriptableObject
    {
        public string minigameName;

        public List<Sprite> images;
        public List<string> description;
    }
}