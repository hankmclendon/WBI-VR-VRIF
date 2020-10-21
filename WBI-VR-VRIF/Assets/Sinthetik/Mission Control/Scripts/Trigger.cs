using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class Trigger : MonoBehaviour
    {
        public bool isActive;

        // this event communicates to the Mission who is tracking which trigger is currently active
        public static event System.Action<GameObject> triggerHit;

        // these events are exposed on the editor and allow for any method on any object to be called when the active status changes
        // this allows the system to hook into any custom events throughout the game
        public UnityEvent triggerStartEvent;
        public UnityEvent triggerStopEvent;

        void Start()
        {
            isActive = false;

            if (triggerStartEvent == null)
                triggerStartEvent = new UnityEvent();

            if (triggerStopEvent == null)
                triggerStopEvent = new UnityEvent();
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if(isActive && other.gameObject.tag == "Player")
            {
                TriggerComplete();
            }
        }

        // this is a convenience method for the fast forward function on the Mission
        public void Skip()
        {
            TriggerComplete();
        }

        public void TriggerComplete()
        {
            isActive = false;
            triggerStopEvent?.Invoke();
            triggerHit?.Invoke(gameObject);
        }

        public void Activate()
        {
            isActive = true;
            triggerStartEvent?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(0, 255, 255, 30);
            Gizmos.DrawCube(new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.center.y, GetComponent<Collider>().bounds.center.z), new Vector3(GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y, GetComponent<Collider>().bounds.size.z));
        }
    }
}
