using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class AudioPanel : MonoBehaviour
    {
        private AudioSource audioSource;
        private IEnumerator audioCoroutine;
        private AudioClip audioClip;

        public static event System.Action<AudioClip> audioComplete;

        void Awake()
        {
            audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        public void PlayAudio(AudioClip audioClip)
        {
            Debug.Log("AudioSource = " + audioSource);
            this.audioClip = audioClip;
            audioSource.clip = audioClip;
            float clipLength = audioSource.clip.length;
            audioSource.Play();
            audioCoroutine = AudioCallback(clipLength);
            gameObject.SetActive(true);
            StartCoroutine(audioCoroutine);
        }
        private IEnumerator AudioCallback(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            audioComplete?.Invoke(audioClip);
            gameObject.SetActive(false);
        }
        public void Skip()
        {
            KillAudio();
            audioComplete?.Invoke(audioClip);
            gameObject.SetActive(false);
        }
        public void KillAudio()
        {
            if (audioCoroutine != null)
                StopCoroutine(audioCoroutine);
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}