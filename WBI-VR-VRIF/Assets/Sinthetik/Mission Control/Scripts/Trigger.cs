using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class Trigger : MonoBehaviour
    {
        public bool isActive;
        public static event System.Action<GameObject> triggerHit;

        void Start()
        {
            isActive = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if(isActive && other.gameObject.tag == "Player")
            {
                triggerHit?.Invoke(gameObject);
                isActive = false;
            }
        }
        public void Skip()
        {
            triggerHit?.Invoke(gameObject);
            isActive = false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(0, 255, 255, 30);
            Gizmos.DrawCube(new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.center.y, GetComponent<Collider>().bounds.center.z), new Vector3(GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y, GetComponent<Collider>().bounds.size.z));
        }
    }
}
