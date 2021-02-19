using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfacePositionController : MonoBehaviour
{
    public Transform headTarget;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, headTarget.localRotation.y * 100, 0));
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.localRotation = Quaternion.Euler(new Vector3(0, headTarget.localRotation.y*100, 0));
    //}
}
