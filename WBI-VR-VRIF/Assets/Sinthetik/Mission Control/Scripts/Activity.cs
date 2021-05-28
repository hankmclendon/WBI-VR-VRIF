using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class Activity : MonoBehaviour
    {
        // this event communicates to the Mission who is tracking which activity is currently active
        public static event System.Action<GameObject> activityComplete;

        public bool isActive;

        public UnityEvent activityStartEvent = new UnityEvent();
        public UnityEvent activityStopEvent = new UnityEvent();

        void Start()
        {
            isActive = false;
        }

        public void Skip()
        {
            Debug.Log("Skip Activity");
            ActivityComplete();
        }

        public void ActivityComplete()
        {
            isActive = false;
            activityStopEvent?.Invoke();
            //Debug.Log("Activity Complete: " + gameObject.name);
            activityComplete?.Invoke(gameObject);
        }

        public void Activate()
        {
            isActive = true;
            activityStartEvent?.Invoke();
        }
    }
}
