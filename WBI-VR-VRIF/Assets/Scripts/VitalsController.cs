using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VitalsController : MonoBehaviour
{
    public Animator wave;
    public Animator heart;
    public Animator pulse;
    public Animator temp;
    public Animator character;
    //public AudioSource audio;
    public AudioClip buttonWrongSound;
    public AudioClip taskCompleteSound;
    public AudioClip instructions;

    private bool isPlaying = false;
    private bool patchAttached = false;
    private SoundPlayer soundPlayer;

    public UnityEvent taskCompleted = new UnityEvent();

    void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    public void ToggleVitals()
    {
        isPlaying = !isPlaying;
        wave.SetBool("isPlaying", isPlaying);
        heart.SetBool("isPlaying", isPlaying);
        pulse.SetBool("isPlaying", isPlaying);
        temp.SetBool("isPlaying", isPlaying);
        StartCoroutine(DelayCoroutine());
    }

    public void ReachOutArm()
    {
        character.SetTrigger("Reach");
    }

    public void PatchAttached()
    {
        character.SetTrigger("Return");
        patchAttached = true;
        soundPlayer.PlayAudio(instructions);
    }

    public void CheckVitals()
    {
        if(patchAttached)
        {
            ToggleVitals();
        }
        else
        {
            soundPlayer.PlayAudio(buttonWrongSound);
        }
    }


    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(4);
        soundPlayer.PlayAudio(taskCompleteSound);
        taskCompleted?.Invoke();
    }
}
