using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public Sinthetik.Helpers.CameraFadeInController cameraFade;
    private SoundPlayer soundPlayer;
    public AudioClip soundtrack;

    void Start()
    {
        cameraFade.FadeInCamera();
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
        StartCoroutine(DelayCoroutine());
    }

    public void FadeSoundtrack()
    {
        soundPlayer.FadeAudio(1, 0);
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        soundPlayer.PlayLoop(soundtrack);
    }
}
