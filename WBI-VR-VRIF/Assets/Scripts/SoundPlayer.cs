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
        audioSource.Play();
    }
}
