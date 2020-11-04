using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

namespace Sinthetik.MissionControl
{
    public class MenuPanel : MonoBehaviour
    {
        public GameObject menuItemPrefab;
        public GameObject menuItemButtonPrefab;
        public GameObject buttonPanel;
        public GameObject locator;
        public Button activateButton;
        public Image titleArea;
        public TextMeshProUGUI descriptionArea;
        private SubSection currentSubSection;
        
        public static event Action<SubSection> itemSelected;

        public void BuildMenu(List<SubSection> currentList)
        {
            bool currentlySelected = true;

            foreach (Transform child in buttonPanel.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (SubSection subSection in currentList)
            {
                GameObject menuItem = Instantiate(menuItemButtonPrefab);
                menuItem.transform.SetParent(buttonPanel.transform, false);
                Debug.Log("add button");
                //menuItem.transform.GetChild(0).GetComponent<Text>().text = subSection.name;
                Button button = menuItem.GetComponent<Button>();
                Image image = menuItem.GetComponent<Image>();
                image.sprite = subSection.data.buttonNormal;

                if (!subSection.isComplete && !subSection.deactivated)
                {
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.highlightedSprite = subSection.data.buttonRollover;
                    _spriteState.selectedSprite = subSection.data.buttonSelected;
                    button.spriteState = _spriteState;

                    button.onClick.AddListener(() => ItemSelected(subSection));
                    if (currentlySelected)
                    {
                        button.Select();
                        ItemSelected(subSection);
                        currentlySelected = false;
                    }
                }
                else if (subSection.isComplete && !subSection.deactivated)
                {
                    button.interactable = false;
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.disabledSprite = subSection.data.buttonCompleted;
                    button.spriteState = _spriteState;
                }
                else 
                {
                    button.interactable = false;
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.disabledSprite = subSection.data.buttonFailed;
                    button.spriteState = _spriteState;
                }
            }
            activateButton.onClick.AddListener(() => StartMission(currentSubSection));
        }

        void ItemSelected(SubSection subSection)
        {
            titleArea.sprite = subSection.data.titleImage;
            descriptionArea.text = subSection.data.descriptionCopy;
            locator.transform.localPosition = subSection.data.locator;
            currentSubSection = subSection;
        }


        void StartMission(SubSection subSection)
        {
            itemSelected?.Invoke(subSection);
            gameObject.SetActive(false);
        }
    }
}
