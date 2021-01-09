using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.UI;
using UnityEngine.Events;

public class HingedWrenchController : MonoBehaviour
{
    public GameObject hinge;
    public Transform bolt;
    public Text torqueText;

    private Lever lever;
    private JointHelper jointHelper;
    private AudioSource audio;

    public bool triggerActive = true;
    private int torque;
    private int startTorque;
    private bool wrenchEnabled;

    public UnityEvent taskCompleted = new UnityEvent();

    void Start()
    {
        lever = hinge.GetComponent<Lever>();
        lever.LeverPercentage = 0;
        audio = GetComponent<AudioSource>();
        jointHelper = GetComponent<JointHelper>();
        wrenchEnabled = true;
    }

    void Update()
    {
        //torqueText.text = hinge.transform.rotation.y.ToString();

        if(wrenchEnabled)
        {
            torque = (int)Mathf.Round(lever.LeverPercentage)/* - startTorque*/;
            torqueText.text = torque.ToString();
            if (torque == 99)
            {
                wrenchEnabled = false;
                bolt.position = new Vector3(bolt.position.x, bolt.position.y - .03f, bolt.position.z);
                DisableLever();
            }
        }
        

    }

    public void DisableLever()
    {
        lever.enabled = false;
        //jointHelper.enabled = true;
        audio.Play();
        StartCoroutine(EndDelayCoroutine());
    }

    IEnumerator EndDelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        taskCompleted?.Invoke();
    }
}
