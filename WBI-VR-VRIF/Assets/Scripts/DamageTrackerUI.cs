using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTrackerUI : MonoBehaviour
{
    public Image tracker;
    public Camera scanCamera;

    void Update()
    {
        Vector3 trackerPosition = scanCamera.WorldToScreenPoint(this.transform.position);
        tracker.transform.position = trackerPosition;
    }
}
