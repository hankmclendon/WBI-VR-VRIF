using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Sinthetik.MissionControl
{
    public class DialoguePanel : MonoBehaviour
    {   
        public ModuleData defaultData;
        private ModuleData currentData;
        public TextMeshProUGUI title;
        public TextMeshProUGUI copy;
        public TextMeshProUGUI buttonText;
        public Button button;
        public Image image;
        private AudioPanel audioSystem;
        private IEnumerator audioCoroutine;
        public static UnityAction dialogueClose;

        // these events are exposed on the editor and allow for any method on any object to be called when the active status changes
        // this allows the system to hook into any custom events throughout the game
        public UnityEvent panelOpenEvent;
        public UnityEvent panelCloseEvent;

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
        public void OpenPanel(ModuleData moduleData = null)
        {;
            if(moduleData != null)
                currentData = moduleData;
            else
                currentData = defaultData;

            title.text = currentData.title;
            copy.text = currentData.copy;
            buttonText.text = currentData.buttonOneText;
            if (currentData.backgroundImage != null)
            {
                image.color = new Color(1, 1, 1, 1);
                image.sprite = currentData.backgroundImage;
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
                
            gameObject.SetActive(true);

            if(currentData.voiceOver != null)
            {
                button.interactable = false;
                button.GetComponent<ButtonTextColorChanger>().isActive = false;
                audioSystem.PlayAudio(currentData.voiceOver);
            }
            else
            {
                EnableButton();
            }

            panelOpenEvent?.Invoke();
        }
        private void EnableButton()
        {
            button.interactable = true;
            button.GetComponent<ButtonTextColorChanger>().isActive = true;
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
            panelCloseEvent?.Invoke();
        }
        public void Skip()
        {
            audioSystem.KillAudio();
            ClosePanel();
        }
    }
}
