using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Sinthetik.MissionControl
{
    public class ChoicePanel : MonoBehaviour
    {   
        public ModuleData defaultData;
        private ModuleData currentData;
        private Text title;
        private Text copy;
        private Text buttonOneText;
        private Text buttonTwoText;
        private Button buttonOne;
        private Button buttonTwo;
        private AudioPanel audioSystem;
        private bool choice;

        public static event Action<bool> choiceClose;

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

        void Awake()
        {
            title = gameObject.transform.Find("Title").GetComponent<Text>();
            copy = gameObject.transform.Find("Copy").GetComponent<Text>();
            buttonOne = gameObject.transform.Find("ButtonOne").GetComponent<Button>();
            buttonOneText = buttonOne.transform.Find("Text").GetComponent<Text>();
            buttonTwo = gameObject.transform.Find("ButtonTwo").GetComponent<Button>();
            buttonTwoText = buttonTwo.transform.Find("Text").GetComponent<Text>();
            audioSystem = (AudioPanel)FindObjectOfType<AudioPanel>();
        }

        public void OpenPanel(ModuleData moduleData = null)
        {;
            if(moduleData != null)
                currentData = moduleData;
            else
                currentData = defaultData;
            title.text = currentData.title;
            copy.text = currentData.copy;
            buttonOneText.text = currentData.buttonOneText;
            buttonTwoText.text = currentData.buttonTwoText;
            gameObject.SetActive(true);

            if(currentData.voiceOver != null)
            {
                buttonOne.interactable = false;
                buttonTwo.interactable = false;
                audioSystem.PlayAudio(currentData.voiceOver);
            }
            else
            {
                EnableChoice();
            }
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if (audioClip == currentData.voiceOver)
                EnableChoice();
        }
        private void EnableChoice()
        {
            buttonOne.interactable = true;
            buttonTwo.interactable = true;
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
            gameObject.SetActive(false);
            choiceClose?.Invoke(choice);
        }
        public void Skip()
        {
            audioSystem.KillAudio();
            choice = true;
            ClosePanel();
        }
    }
}
