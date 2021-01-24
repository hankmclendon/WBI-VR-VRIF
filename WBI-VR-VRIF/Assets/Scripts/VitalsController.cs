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
    public AudioSource audio;
    private bool isPlaying = false;

    public UnityEvent taskCompleted = new UnityEvent();

    void Start()
    {
        
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


    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(4);
        audio.Play();
        taskCompleted?.Invoke();
    }
}
