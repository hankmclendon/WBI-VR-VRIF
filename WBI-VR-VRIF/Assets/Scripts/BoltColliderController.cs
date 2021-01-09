using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltColliderController : MonoBehaviour
{
    public GameObject wrenchHinged;
    public GameObject standardWrench;
    private Collider standardWrenchCollider;

    private void Start()
    {
        wrenchHinged.SetActive(false);
        standardWrenchCollider = standardWrench.GetComponent<Collider>();
        Debug.Log("Start: " + standardWrenchCollider);
    }

    public void Activate()
    {
        wrenchHinged.SetActive(true);
    }

    public void Deactivate()
    {
        wrenchHinged.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BoltCollider: " + other);
        if (other == standardWrenchCollider)
        {
            Debug.Log("Inside If Statement");
            standardWrench.SetActive(false);
            wrenchHinged.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void TaskComplete()
    {
        standardWrench.SetActive(true);
        wrenchHinged.SetActive(false);
        gameObject.SetActive(false);
    }
}
