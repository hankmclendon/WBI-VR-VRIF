using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class MissionProfileController : MonoBehaviour
{
    bool profile01Complete = false;
    bool profile02Complete = false;
    public GameObject cartridge01;
    public GameObject cartridge02;
    public VideoPlayer videoPlayer;
    public VideoClip videoClip01;
    public VideoClip videoClip02;
    private GameObject queuedCartridge;
    private GameObject registeredCartridge;
    private SoundPlayer soundPlayer;
    public AudioClip wrong;
    public AudioClip right;
    public AudioClip complete;

    public UnityEvent taskCompleted = new UnityEvent();

    private void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    public void LoadMissionProfile()
    {
        Debug.Log("LoadMissionProfile = " + registeredCartridge);
        if (registeredCartridge == cartridge01)
        {
            videoPlayer.clip = videoClip01;
            videoPlayer.Play();
            soundPlayer.PlayAudio(right);
            profile01Complete = true;
            CheckCompletion();
        }
        if(registeredCartridge == cartridge02)
        {
            videoPlayer.clip = videoClip02;
            videoPlayer.Play();
            soundPlayer.PlayAudio(right);
            profile02Complete = true;
            CheckCompletion();
        }
        if(registeredCartridge == null)
        {
            soundPlayer.PlayAudio(wrong);
        }
    }
    public void StopMissionProfile()
    {
        videoPlayer.Stop();
    }
    public void RegisterCartridge()
    {
        registeredCartridge = queuedCartridge;
        Debug.Log("RegisterCartridge = " + registeredCartridge);
    }
    public void UnregisterCartridge()
    {
        registeredCartridge = null;
    }
    public void QueueCartridge(GameObject cartridge)
    {
        queuedCartridge = cartridge;
        Debug.Log("QueueCartridge = " + cartridge);
    }
    private void CheckCompletion()
    {
        if (profile01Complete && profile02Complete)
        {
            StartCoroutine(DelayCoroutine());
        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(5);
        soundPlayer.PlayAudio(complete);
        taskCompleted?.Invoke();
    }
}
