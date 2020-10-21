using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BNG;
using System.Threading;
using JetBrains.Annotations;

namespace Sinthetik.MissionControl
{
    public class MissionActivity : MonoBehaviour
    {
        // this event communicates to the Mission who is tracking which activity is currently active
        public static event System.Action<GameObject> activityComplete;

        // these events are exposed on the editor and allow for any method on any object to be called when the active status changes
        // this allows the system to hook into any custom events throughout the game
        public UnityEvent activityStartEvent;
        public UnityEvent activityStopEvent;

        public bool isActive;

        void Start()
        {
            isActive = false;

            if (activityStartEvent == null)
                activityStartEvent = new UnityEvent();

            if (activityStopEvent == null)
                activityStopEvent = new UnityEvent();
        }

        public void Skip()
        {
            ActivityComplete();
        }

        public void ActivityComplete()
        {
            isActive = false;
            activityStopEvent?.Invoke();
            activityComplete?.Invoke(gameObject);
        }

        public void Activate()
        {
            isActive = true;
            activityStartEvent?.Invoke();
        }
    }   
}