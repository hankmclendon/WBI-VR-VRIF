using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using BNG;
using UnityEngine.UI;
using UnityEngine.Events;

public class WrenchController : MonoBehaviour
{
    public GameObject hinge;
    public Text torqueText;
    public AudioClip boltComplete;
    public AudioClip taskComplete;
    public AudioClip ratchet;
    public Collider[] colliders;

    private Grabbable grabbable;
    private Collider wrenchCollider;
    private Lever lever;
    private Rigidbody wrenchRigidbody;
    private Rigidbody hingeRigidbody;
    private Transform hand;
    private Transform grabPoint;

    private JointHelper jointHelper;

    //public bool triggerActive = true;
    private bool wrenchAttached = false;
    private int torque;
    private int startTorque;
    private int boltsCompleted = 0;
    private Collider currentCollider;

    public UnityEvent taskCompleted = new UnityEvent();

    private AudioSource audio;

    void Start()
    {
        grabbable = hinge.GetComponent<Grabbable>();
        wrenchCollider = GetComponent<Collider>();
        lever = hinge.GetComponent<Lever>();
        wrenchRigidbody = GetComponent<Rigidbody>();
        hingeRigidbody = hinge.GetComponent<Rigidbody>();
        jointHelper = hinge.GetComponent<JointHelper>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        foreach(Collider col in colliders)
        {
            if (other == col)
            {
                Debug.Log("Collision: " + other);
                currentCollider = other;
                EnableLever();
            }
        }
        
    }

    public void EnableLever()
    {
        //grabbable.enabled = false;
        //StartCoroutine(StartDelayCoroutine());
        transform.position = hinge.transform.position = currentCollider.transform.position;
        transform.rotation = currentCollider.transform.rotation;
        currentCollider.enabled = false;
        wrenchRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        //jointHelper.enabled = false;
        grabbable.GrabPhysics = GrabPhysics.None;
        hand = hinge.transform.parent;
        grabbable.ParentToHands = false;
        hinge.transform.parent = transform;
        //grabbable.ParentHandModel = true;
        
        lever.LeverPercentage = 0;
        lever.enabled = true;
        //triggerActive = false;
        
        wrenchAttached = true;
        startTorque = (int)lever.LeverPercentage;
    }

    public void DisableLever()
    {
        lever.enabled = false;
        //jointHelper.enabled = true;
        audio.clip = boltComplete;
        audio.Play();
        StartCoroutine(EndDelayCoroutine());
    }

    IEnumerator EndDelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        //jointHelper.enabled = false;
        hingeRigidbody.useGravity = false;
        hingeRigidbody.isKinematic = true;
        wrenchRigidbody.constraints = RigidbodyConstraints.None;
        grabbable.GrabPhysics = GrabPhysics.Kinematic;

        //grabbable.ParentHandModel = false;
        //hinge.transform.parent = hand.transform;
        //hinge.transform.position = hand.transform.position;
        //hinge.transform.rotation = hand.transform.rotation;
        //lever.LeverPercentage = 0;
        //wrenchAttached = false;
        grabbable.ParentToHands = true;
        //grabbable.enabled = true;

        taskCompleted?.Invoke();
    }

    void Update()
    {
        if (wrenchAttached)
        {
            torque = (int)Mathf.Round(lever.LeverPercentage)/* - startTorque*/;
            torqueText.text = torque.ToString();
            if (torque == 99)
            {
                wrenchAttached = false;
                //bolt.position = new Vector3(bolt.position.x, bolt.position.y - .03f, bolt.position.z);
                DisableLever();

            }
        }

    }
}
