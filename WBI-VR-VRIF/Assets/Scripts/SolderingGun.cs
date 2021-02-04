using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SolderingGun : MonoBehaviour
{
    public List<Collider> contactPoints = new List<Collider>();
    private List<Collider> contactsCompleted = new List<Collider>();
    public ParticleSystem sparks;
    public AudioClip solderComplete;
    public AudioClip solderFail;
    public AudioClip taskComplete;
    public AudioClip sparksLoop;
    private SoundPlayer sound;
    private IEnumerator solderingCoroutine;
    private Collider currentCollider;

    public UnityEvent taskCompleted = new UnityEvent();
    

    void Start()
    {
        sound = GetComponent<SoundPlayer>();    
    }
    public void CheckForContact(Collider collider)
    {
        Debug.Log("COLLISION ENTER DETECTED");
        foreach (Collider contact in contactPoints)
        {
            if (collider == contact)
            {
                Debug.Log("COLLIDED WITH: " + contact);
                StartSparks();
                currentCollider = contact;
            }
        }
        
    }
    public void CheckForContactExit(Collider collider)
    {
        Debug.Log("COLLISION EXIT DETECTED");
        foreach (Collider contact in contactPoints)
        {
            if (collider == contact)
            {
                Debug.Log("EXITED: " + contact);
                StopSparks();
                currentCollider = null;
            }
        }
    }

    public void StopSparks()
    {
        sparks.Stop();
        StopCoroutine(solderingCoroutine);
        sound.StopAudio();
        sound.PlayAudio(solderFail);
    }

    private void StartSparks()
    {
        sparks.Play();
        solderingCoroutine = DelayCoroutine();
        StartCoroutine(solderingCoroutine);
        sound.PlayLoop(sparksLoop);
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(3);
        sound.PlayAudio(solderComplete);
        contactsCompleted.Add(currentCollider);
        bool allContactsCompleted = contactPoints.All(i => contactsCompleted.Contains(i));

        if (allContactsCompleted)
        {
            sound.PlayAudio(taskComplete);
            taskCompleted?.Invoke();
        }
    }
}
