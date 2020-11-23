using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class Trigger : MonoBehaviour
    {
        public bool isActive;
        private GameObject triggerLocator;

        // this event communicates to the Mission who is tracking which trigger is currently active
        public static event System.Action<GameObject> triggerHit;

        void Start()
        {
            isActive = false;
            triggerLocator = transform.Find("Trigger Locator").gameObject;
            triggerLocator.SetActive(false);
        }

        // this is a convenience method for the fast forward function on the Mission
        public void Skip()
        {
            TriggerComplete();
        }

        public void TriggerComplete()
        {
            isActive = false;
            triggerHit?.Invoke(gameObject);
            triggerLocator.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            triggerLocator.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isActive && other.gameObject.tag == "Player")
            {
                TriggerComplete();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(0, 255, 255, 30);
            Gizmos.DrawCube(new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.center.y, GetComponent<Collider>().bounds.center.z), new Vector3(GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y, GetComponent<Collider>().bounds.size.z));
        }
    }
}
