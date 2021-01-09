using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWrenchController : MonoBehaviour
{
    public Collider[] colliders;
    private Collider currentCollider;

    private void OnTriggerEnter(Collider other)
    {

        foreach (Collider col in colliders)
        {
            if (other == col)
            {
                gameObject.SetActive(false);
                currentCollider = other;
                //other.GetComponent<BoltColliderController>().Activate();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(true);
        //if (other == currentCollider)
        //{
        //    gameObject.SetActive(true);
        //    //other.GetComponent<BoltColliderController>().Deactivate();
        //}

    }
}
