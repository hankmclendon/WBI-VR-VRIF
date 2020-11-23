using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sinthetik.MissionControl
{
    public class TriggerGizmo : MonoBehaviour
    {
        public Trigger trigger;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                trigger.TriggerComplete();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(0, 255, 255, 30);
            Gizmos.DrawCube(new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.center.y, GetComponent<Collider>().bounds.center.z), new Vector3(GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y, GetComponent<Collider>().bounds.size.z));
        }
    }
}
