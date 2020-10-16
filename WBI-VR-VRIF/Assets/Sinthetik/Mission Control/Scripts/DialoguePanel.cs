using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class DialoguePanel : MonoBehaviour
    {   
        public ModuleData defaultData;
        private ModuleData currentData;
        private Text title;
        private Text copy;
        private Text buttonText;
        private Button button;
        private AudioPanel audioSystem;
        private IEnumerator audioCoroutine;
        public static UnityAction dialogueClose;

        void Awake()
        {
            title = gameObject.transform.Find("Title").GetComponent<Text>();
            copy = gameObject.transform.Find("Copy").GetComponent<Text>();
            button = gameObject.transform.Find("Button").GetComponent<Button>();
            buttonText = button.transform.Find("Text").GetComponent<Text>();
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
        public void OpenPanel(ModuleData moduleData = null)
        {;
            if(moduleData != null)
                currentData = moduleData;
            else
                currentData = defaultData;
            title.text = currentData.title;
            copy.text = currentData.copy;
            buttonText.text = currentData.buttonOneText;
            gameObject.SetActive(true);

            if(currentData.voiceOver != null)
            {
                button.interactable = false;
                audioSystem.PlayAudio(currentData.voiceOver);
            }
            else
            {
                EnableButton();
            }
        }
        private void EnableButton()
        {
            button.interactable = true;
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if (audioClip == currentData.voiceOver)
                EnableButton();
        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
            dialogueClose?.Invoke();
        }
        public void Skip()
        {
            audioSystem.KillAudio();
            ClosePanel();
        }
    }
}
