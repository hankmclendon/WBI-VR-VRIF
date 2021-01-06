using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoftwareUpdateController : MonoBehaviour
{
    public Animator software01Anim;
    public Animator software02Anim;
    public Animator software03Anim;
    private bool software01Complete = false;
    private bool software02Complete = false;
    private bool software03Complete = false;
    private int currentSoftware = 1;
    public AudioSource audio;

    public UnityEvent taskCompleted = new UnityEvent();

    public void UpdateSoftware()
    {
        if (currentSoftware == 1 && !software01Complete)
        {
            software01Anim.SetTrigger("Update");
            software01Complete = true;
            CheckCompletion();
        }
        else if (currentSoftware == 2 && !software02Complete)
        {
            software02Anim.SetTrigger("Update");
            software02Complete = true;
            CheckCompletion();
        }
        else if (currentSoftware == 3 && !software03Complete)
        {
            software03Anim.SetTrigger("Update");
            software03Complete = true;
            CheckCompletion();
        }
        
    }
    public void SetSoftware(int number)
    {
        currentSoftware = number;
        Debug.Log("Software Set: " + number);
    }
    private void CheckCompletion()
    {
        if(software01Complete && software02Complete && software03Complete)
        {
            StartCoroutine(DelayCoroutine());
        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(5);
        audio.Play();
        taskCompleted?.Invoke();
    }
}
