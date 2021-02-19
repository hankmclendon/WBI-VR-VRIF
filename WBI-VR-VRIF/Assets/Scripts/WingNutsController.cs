using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WingNutsController : MonoBehaviour
{
    private int boltsCompleted = 0;

    public AudioSource audio;

    public UnityEvent taskCompleted = new UnityEvent();

   
    public void BoltComplete()
    {
        boltsCompleted += 1;
        if (boltsCompleted == 3)
        {
            //StartCoroutine(DelayCoroutine());
            audio.Play();
            taskCompleted?.Invoke();
        }
    }

    //IEnumerator DelayCoroutine()
    //{
    //    yield return new WaitForSeconds(2);
    //    audio.Play();
    //    taskCompleted?.Invoke();
    //}
}
