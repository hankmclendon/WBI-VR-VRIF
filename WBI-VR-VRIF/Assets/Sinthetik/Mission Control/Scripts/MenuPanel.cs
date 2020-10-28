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

                SpriteState _spriteState = new SpriteState();
                _spriteState.highlightedSprite = subSection.data.buttonRollover;
                _spriteState.selectedSprite = subSection.data.buttonSelected;
                button.spriteState = _spriteState;

                button.onClick.AddListener(() => ItemSelected(subSection));

                //if (!subSection.isComplete && !subSection.deactivated)
                //{
                //    button.onClick.AddListener(() => ItemSelected(subSection));
                //}
                //else if (subSection.deactivated)
                //{
                //    button.interactable = false;
                //    menuItem.transform.GetChild(0).GetComponent<Text>().text = "Deactivated";
                //}
                //else
                //{
                //    button.interactable = false;
                //}
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
