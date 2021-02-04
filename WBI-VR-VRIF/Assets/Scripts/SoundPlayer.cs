using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip _soundFile)
    {
        audioSource.clip = _soundFile;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void PlayLoop(AudioClip _soundFile)
    {
        audioSource.clip = _soundFile;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
    public void SetVolume(float _volume = 1f)
    {
        audioSource.volume = _volume;
    }
}
