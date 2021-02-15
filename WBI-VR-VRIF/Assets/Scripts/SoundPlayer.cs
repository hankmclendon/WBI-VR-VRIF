using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
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
    public void FadeAudio(float duration, float targetVolume)
    {
        StartCoroutine(StartFade(audioSource, duration, targetVolume));
    }

    public static IEnumerator StartFade(AudioSource source, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
