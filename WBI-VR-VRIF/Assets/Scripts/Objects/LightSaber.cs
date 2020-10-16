using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public AudioClip beamOnAudio;
    public AudioClip beamOffAudio;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerBeam()
    {
        bool isOn = animator.GetBool("LightSaberOn");
        if (!isOn)
            audioSource.PlayOneShot(beamOnAudio);
        else audioSource.PlayOneShot(beamOffAudio);

        animator.SetBool("LightSaberOn", !isOn);
    }
}
