using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class AudioPanel : MonoBehaviour
    {
        public GameObject displayPanel;
        public AudioSource audioSource;
        public bool showPanel;

        private IEnumerator audioCoroutine;
        private AudioClip audioClip;

        public static event System.Action<AudioClip> audioComplete;

        public void PlayAudio(AudioClip _audioClip)
        {
            if (showPanel)
                ShowDisplayPanel();
            this.audioClip = _audioClip;
            audioSource.clip = audioClip;
            float clipLength = audioSource.clip.length;
            audioSource.Play();
            audioCoroutine = AudioCallback(clipLength);
            StartCoroutine(audioCoroutine);
        }
        private IEnumerator AudioCallback(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            audioComplete?.Invoke(audioClip);
            if (showPanel)
                HideDisplayPanel();
        }
        public void Skip()
        {
            KillAudio();
            audioComplete?.Invoke(audioClip);
            if (showPanel)
                HideDisplayPanel();
        }
        public void KillAudio()
        {
            if (audioCoroutine != null)
                StopCoroutine(audioCoroutine);
            if (audioSource.isPlaying)
                audioSource.Stop();
        }

        public void ShowDisplayPanel()
        {
            displayPanel.SetActive(true);
        }

        public void HideDisplayPanel()
        {
            displayPanel.SetActive(false);
        }
    }
}