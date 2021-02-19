using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class SolderingGun : MonoBehaviour
{
    public List<Collider> contactPoints = new List<Collider>();
    private List<Collider> contactsCompleted = new List<Collider>();
    public ParticleSystem sparks;
    public AudioClip solderComplete;
    public AudioClip solderFail;
    public AudioClip taskComplete;
    public AudioClip sparksLoop;
    public AudioClip alreadyDoneWarning;
    private SoundPlayer sound;
    private IEnumerator solderingCoroutine;
    private Collider currentCollider;
    private Text currentRing;
    private bool currentColliderComplete = false;

    public UnityEvent taskCompleted = new UnityEvent();
    

    void Start()
    {
        sound = GetComponent<SoundPlayer>();
        foreach(Collider contact in contactPoints)
        {
            GameObject first = contact.gameObject.transform.GetChild(0).gameObject;
            Text ring = first.GetComponent<Text>();
            ring.color = new Color(1, 0, 0, .5f);
        }
    }
    public void CheckForContact(Collider collider)
    {
        foreach (Collider contact in contactPoints)
        {
            if (collider == contact)
            {
                StartSparks();
                currentCollider = contact;
                currentRing = contact.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            }
        }
        
    }
    public void CheckForContactExit(Collider collider)
    {
        foreach (Collider contact in contactPoints)
        {
            if (collider == contact)
            {
                StopSparks();
                currentCollider = null;
                currentRing = null;
            }
        }
    }

    public void StopSparks()
    {
        sparks.Stop();
        StopCoroutine(solderingCoroutine);
        sound.StopAudio();
        if(currentColliderComplete == false)
            sound.PlayAudio(solderFail);
    }

    private void StartSparks()
    {
        sparks.Play();
        solderingCoroutine = DelayCoroutine();
        StartCoroutine(solderingCoroutine);
        sound.PlayLoop(sparksLoop);
        currentColliderComplete = false;
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(3);
        currentColliderComplete = true;
        sound.PlayAudio(solderComplete);
        currentRing.color = new Color(0, 1, 0, .5f);
        contactsCompleted.Add(currentCollider);


        bool allContactsCompleted = contactPoints.All(i => contactsCompleted.Contains(i));

        if (allContactsCompleted)
        {
            sound.PlayAudio(taskComplete);
            taskCompleted?.Invoke();
        }
    }

}
