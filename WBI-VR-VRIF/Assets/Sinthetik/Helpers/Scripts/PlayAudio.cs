using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.Helpers
{
    public class PlayAudio : MonoBehaviour
    {
        public AudioClip[] audioClips;
        AudioSource sound;

        private void Start()
        {
            sound = GetComponent<AudioSource>();
        }

        public void PlaySound(int index)
        {
            sound.clip = audioClips[index];
            sound.loop = false;
            sound.Play();
        }
        public void LoopSound(int index)
        {
            sound.clip = audioClips[index];
            sound.loop = true;
            sound.Play();
        }
        public void StopSound()
        {
            sound.Stop();
        }
    }
}