using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.UI;
using UnityEngine.Events;

public class GrabController : MonoBehaviour
{
    public GameObject wrench;
    public GameObject hinge;
    public Transform bolt;
    
    public Grabbable grabbable;
    public Collider col;
    public Lever lever;
    public Rigidbody wrenchRigidbody;
    public Rigidbody hingeRigidbody;
    public Transform hand;
    public Transform grabPoint;
    public Text torqueText;
    public JointHelper jointHelper;

    public bool triggerActive = true;
    private bool wrenchAttached = false;
    private int torque;
    private int startTorque;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    public UnityEvent taskCompleted = new UnityEvent();
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision: " + other);
        if(other == col && triggerActive)
        {
            EnableLever();
        }
    }

    public void EnableLever()
    {
        //grabbable.enabled = false;
        //StartCoroutine(StartDelayCoroutine());
        //jointHelper.enabled = false;
        grabbable.GrabPhysics = GrabPhysics.None;
        hand = hinge.transform.parent;
        grabbable.ParentToHands = false;
        hinge.transform.parent = wrench.transform;
        //grabbable.ParentHandModel = true;
        wrench.transform.position = hinge.transform.position = transform.position;
        wrench.transform.rotation = transform.rotation;
        wrenchRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        lever.LeverPercentage = 0;
        lever.enabled = true;
        triggerActive = false;
        wrenchAttached = true;
        startTorque = (int)lever.LeverPercentage;
    }

    //IEnumerator StartDelayCoroutine()
    //{
    //    yield return new WaitForSeconds(1);
    //    grabbable.GrabPhysics = GrabPhysics.None;
    //    hand = hinge.transform.parent;
    //    grabbable.ParentToHands = false;
    //    hinge.transform.parent = wrench.transform;
    //    //grabbable.ParentHandModel = true;
    //    wrench.transform.position = hinge.transform.position = transform.position;
    //    wrench.transform.rotation = transform.rotation;
    //    wrenchRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    //    lever.LeverPercentage = 0;
    //    lever.enabled = true;
    //    triggerActive = false;
    //    wrenchAttached = true;
    //    startTorque = (int)lever.LeverPercentage;
    //    grabbable.enabled = true;
    //}

    public void DisableLever()
    {
        lever.enabled = false;
        jointHelper.enabled = true;
        //grabbable.enabled = false;
        audio.Play();
        StartCoroutine(EndDelayCoroutine());
    }

    IEnumerator EndDelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        jointHelper.enabled = false;
        hingeRigidbody.useGravity = false;
        hingeRigidbody.isKinematic = true;

        //grabbable.ParentHandModel = false;
        hinge.transform.parent = hand;
        hinge.transform.position = hand.transform.position;
        hinge.transform.rotation = hand.transform.rotation;
        grabbable.ParentToHands = true;
        //lever.LeverPercentage = 0;
        //wrenchAttached = false;

        //grabbable.enabled = true;

        taskCompleted?.Invoke();
    }

    void Update()
    {
        if(wrenchAttached)
        {
            torque = (int)Mathf.Round(lever.LeverPercentage)/* - startTorque*/;
            torqueText.text = torque.ToString();
            if (torque == 99)
            {
                wrenchAttached = false;
                bolt.position = new Vector3(bolt.position.x, bolt.position.y - .03f, bolt.position.z);
                DisableLever();
                
            }
        }
        
    }
}
