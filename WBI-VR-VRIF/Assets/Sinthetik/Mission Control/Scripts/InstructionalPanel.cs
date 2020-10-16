using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class InstructionalPanel : MonoBehaviour
    {   
        public ModuleData defaultData;
        private ModuleData currentData;
        private Text title;
        private Text copy;
        private AudioPanel audioSystem;
        private GameObject activity;

        public static UnityAction instructionalClose;

        #region Listeners
        private void OnEnable()
        {
            MissionActivity.activityComplete += ActivityComplete;
            AudioPanel.audioComplete += AudioComplete;
        }

        private void OnDisable()
        {
            MissionActivity.activityComplete -= ActivityComplete;
            AudioPanel.audioComplete -= AudioComplete;
        }

        void Awake()
        {
            title = gameObject.transform.Find("Title").GetComponent<Text>();
            copy = gameObject.transform.Find("Copy").GetComponent<Text>();
            audioSystem = (AudioPanel)FindObjectOfType<AudioPanel>();
        }

        #endregion
        public void OpenPanel(GameObject activity, ModuleData moduleData = null)
        {
            this.activity = activity;
            if(moduleData != null)
                currentData = moduleData;
            else
                currentData = defaultData;
            title.text = currentData.title;
            copy.text = currentData.copy;
            gameObject.SetActive(true);

            if(currentData.voiceOver != null)
            {
                Debug.Log("VoiceOver =" + currentData.voiceOver);
                audioSystem.PlayAudio(currentData.voiceOver);
            }
            else
            {
                EnableActivity();
            }
        }
        private void AudioComplete(AudioClip audioClip)
        {
            if (audioClip == currentData.voiceOver)
                EnableActivity();
        }
        private void EnableActivity()
        {
            activity.GetComponent<MissionActivity>().isActive = true;
        }
        public void ClosePanel()
        {
            activity.GetComponent<MissionActivity>().isActive = false;
            gameObject.SetActive(false);
            instructionalClose?.Invoke();
        }

        private void ActivityComplete(GameObject _activity)
        {
            if(_activity == activity)
                ClosePanel();
        }
        public void Skip()
        {
            audioSystem.KillAudio();
            ClosePanel();
        }
    }
}
