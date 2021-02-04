using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SolderingGunTip : MonoBehaviour
{
    public SinthetikCollisionEvent collisionEnterDetected = new SinthetikCollisionEvent();
    public SinthetikCollisionEvent collisionExitDetected = new SinthetikCollisionEvent();
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("TIP COLLISION ENTER DETECTED: " + collider);
        collisionEnterDetected?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("TIP COLLISION EXIT DETECTED: " + collider);
        collisionExitDetected?.Invoke(collider);
    }
}

[System.Serializable]
public class SinthetikCollisionEvent : UnityEvent<Collider>
{
}
