using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;
using Sirenix.OdinInspector;

namespace Sinthetik.MissionControl
{
    public class MenuPanel : MonoBehaviour
    {
        //public GameObject menuButtonPrefab;
        public GameObject displayPanel;
        public GameObject locator;
        public Button activateButton;
        public TextMeshProUGUI activateButtonText;
        public TextMeshProUGUI descriptionTextArea;
        public Image titleArea;
        public Sprite completedImage;
        public Sprite failedImage;
        public AudioClip successAudio;
        public AudioClip notBadAudio;
        public AudioClip failedAudio;
        [TextArea]
        public string completedText;
        public List<GameObject> buttonList = new List<GameObject>();
        private Task currentTask;
        private List<Task> currentList;
        public bool isCompleted;
        private int failedMissions = 0;
        private int completedMissions = 0;
        private int totalMissions = 0;
        private SoundPlayer soundPlayer;

        public static event Action<Task> itemSelected;
        public static UnityAction menuComplete;

        void Start()
        {
            soundPlayer = GetComponent<SoundPlayer>();
        }

        public void OpenPanel(List<Task> _currentList, bool _isCompleted)
        {
            currentList = _currentList;
            isCompleted = _isCompleted;
            ShowDisplayPanel();
            if (isCompleted)
            {
                for (int i = 0; i < currentList.Count; i++)
                {
                    totalMissions += 1;
                    if (currentList[i].deactivated)
                        failedMissions += 1;
                    completedMissions = totalMissions - failedMissions;
                }

                if(failedMissions == 0)
                {
                    descriptionTextArea.text = "Congrats! You completed all the missions! You show great potential!";
                    soundPlayer.PlayAudio(successAudio);
                }
                else if(failedMissions <= totalMissions/2)
                {
                    descriptionTextArea.text = "Not bad! You completed " + completedMissions + " out of " + totalMissions + ". Thanks for helping us out.";
                    soundPlayer.PlayAudio(notBadAudio);
                }  
                else
                {
                    descriptionTextArea.text = "Not so great. You completed " + completedMissions + " out of " + totalMissions + ". Better luck next time.";
                    soundPlayer.PlayAudio(failedAudio);
                }                  

                activateButtonText.text = "Finish";
                titleArea.sprite = completedImage; 
            }
                
            BuildMenu();
        }

        private void BuildMenu()
        {
            bool currentlySelected = true;

            //foreach (Transform child in buttonPanel.transform)
            //{
            //    Destroy(child.gameObject);
            //}

            //Debug.Log("CurrentList Count = " + currentList.Count);

            for(int i = 0; i < currentList.Count; i++)
            {
                Task currentTask = currentList[i];
                Button currentButton = buttonList[i].GetComponent<Button>();
                Image currentButtonImage = buttonList[i].GetComponent<Image>();
                //Debug.Log(currentButtonImage.sprite);

                currentButtonImage.sprite = currentTask.menuContent.normal;
                currentButton.transition = Selectable.Transition.SpriteSwap;

                if (!currentTask.isComplete && !currentTask.deactivated)
                {
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.highlightedSprite = currentTask.menuContent.rollover;
                    _spriteState.selectedSprite = currentTask.menuContent.selected;
                    currentButton.spriteState = _spriteState;

                    currentButton.onClick.AddListener(() => ItemSelected(currentTask));

                    if (currentlySelected)
                    {
                        currentButton.Select();
                        ItemSelected(currentTask);
                        currentlySelected = false;
                    }
                }
                else if (currentTask.isComplete && !currentTask.deactivated)
                {
                    currentButton.interactable = false;
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.disabledSprite = currentTask.menuContent.completed;
                    currentButton.spriteState = _spriteState;
                }
                else
                {
                    currentButton.interactable = false;
                    SpriteState _spriteState = new SpriteState();
                    _spriteState.disabledSprite = currentTask.menuContent.failed;
                    currentButton.spriteState = _spriteState;
                }
            }

            if(isCompleted)
            {
                activateButton.onClick.RemoveAllListeners();
                activateButton.onClick.AddListener(() => FinishGame());
            }
            else
            {
                activateButton.onClick.RemoveAllListeners();
                activateButton.onClick.AddListener(() => StartMission(currentTask));
            }
                
        }

        void ItemSelected(Task task)
        {
            titleArea.sprite = task.menuContent.titleContent;
            descriptionTextArea.text = task.menuContent.description;
            locator.transform.localPosition = task.menuContent.location;
            currentTask = task;
        }


        void StartMission(Task task)
        {
            itemSelected?.Invoke(task);
            HideDisplayPanel();
        }

        void FinishGame()
        {
            menuComplete?.Invoke();
            HideDisplayPanel();
        }

        public void Skip()
        {
            FinishGame();
        }

        public void ShowDisplayPanel()
        {
            displayPanel.SetActive(true);
            gameObject.SetActive(true);
        }

        public void HideDisplayPanel()
        {
            gameObject.SetActive(false);
        }
    }
}
