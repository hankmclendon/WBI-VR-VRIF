using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

namespace Sinthetik.MissionControl
{
    public class DialoguePanel : MonoBehaviour
    {
        private Module currentModule;
        public GameObject displayPanel;
        public TextMeshProUGUI title;
        public TextMeshProUGUI copy;
        public TextMeshProUGUI buttonNextText;
        public TextMeshProUGUI buttonOneText;
        public TextMeshProUGUI buttonTwoText;
        public Button buttonNext;
        public Button buttonOne;
        public Button buttonTwo;
        public Image image;
        private AudioPanel audioSystem;
        private IEnumerator audioCoroutine;
        [HideInInspector]
        public bool hasChoice;
        private bool choice;

        public static event Action<bool> choiceClose;
        public static UnityAction dialogueClose;

        void Awake()
        {
            audioSystem = (AudioPanel)FindObjectOfType<AudioPanel>();
        }

        #region Listeners
        private void OnEnable()
        {
            AudioPanel.audioComplete += AudioComplete;
        }

        private void OnDisable()
        {
            AudioPanel.audioComplete -= AudioComplete;
        }

        #endregion
        public void OpenPanel(Module _currentModule, bool _hasChoice)
        {
            currentModule = _currentModule;
            hasChoice = _hasChoice;

            if (hasChoice)
            {
                buttonNext.gameObject.SetActive(false);
                buttonOne.gameObject.SetActive(true);
                buttonTwo.gameObject.SetActive(true);
                //buttonOne.onClick.AddListener(SelectChoiceOne);
                //buttonTwo.onClick.AddListener(SelectChoiceTwo);
            }   
            else
            {
                buttonNext.gameObject.SetActive(true);
                buttonOne.gameObject.SetActive(false);
                buttonTwo.gameObject.SetActive(false);
                //buttonOne.onClick.AddListener(ClosePanel);
            }

            if(currentModule.data != null)
            {
                if(currentModule.data.title != null)
                    title.text = currentModule.data.title;
                else
                    title.text = currentModule.moduleName;

                if(currentModule.data.copy != null)
                    copy.text = currentModule.data.copy;
                else
                    copy.text = "Data exists but there is no copy data for this module.";

                if(hasChoice)
                {
                    if (currentModule.data.buttonOneText != null)
                        buttonOneText.text = currentModule.data.buttonOneText;
                    else
                        buttonOneText.text = "Choice 1";

                    if (currentModule.data.buttonTwoText != null)
                        buttonTwoText.text = currentModule.data.buttonTwoText;
                    else
                        buttonTwoText.text = "Choice 2";
                }
                else
                {
                    if (currentModule.data.buttonOneText != null)
                        buttonNextText.text = currentModule.data.buttonOneText;
                    else
                        buttonNextText.text = "Next";
                }

                if (currentModule.data.backgroundImage != null)
                {
                    image.color = new Color(1, 1, 1, 1);
                    image.sprite = currentModule.data.backgroundImage;
                }
                else
                {
                    image.color = new Color(1, 1, 1, 0);
                }
            }
            else
            {
                title.text = currentModule.moduleName;
                copy.text = "No data for this module.";
                if(hasChoice)
                {
                    buttonOneText.text = "Choice 1";
                    buttonTwoText.text = "Choice 2";
                }
                else
                {
                    buttonNextText.text = "Next";
                }
                
                image.color = new Color(1, 1, 1, 0);
            }

            ShowDisplayPanel();
            

            if (currentModule.audioClip != null)
            {
                //Debug.Log("Dialogue AudioClip !- null");
                buttonOne.interactable = false;
                if (hasChoice)
                    buttonTwo.interactable = false;
                buttonOne.GetComponent<ButtonTextColorChanger>().isActive = false;
                audioSystem.PlayAudio(currentModule.audioClip);
            }
            else
            {
                
                EnableButton();
            }
        }
        private void EnableButton()
        {
            buttonOne.interactable = true;
            if (hasChoice)
                buttonTwo.interactable = true;
            buttonOne.GetComponent<ButtonTextColorChanger>().isActive = true;
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if (audioClip == currentModule.audioClip)
                EnableButton();
        }

        public void SelectChoiceOne()
        {
            choice = true;
            ClosePanel();
        }
        public void SelectChoiceTwo()
        {
            choice = false;
            ClosePanel();
        }
        public void ClosePanel()
        {

            HideDisplayPanel();
           // audioSystem.KillAudio();

            if (hasChoice)
            {
                //buttonOne.onClick.RemoveAllListeners();
                //buttonTwo.onClick.RemoveAllListeners();
                choiceClose?.Invoke(choice);
            }
            else
            {
                //Debug.Log("DialogueClose Invoked");
                //buttonOne.onClick.RemoveAllListeners();
                dialogueClose?.Invoke();
            }
                
        }
        public void Skip()
        {
            if (hasChoice)
                SelectChoiceOne();
            else
                ClosePanel();
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
