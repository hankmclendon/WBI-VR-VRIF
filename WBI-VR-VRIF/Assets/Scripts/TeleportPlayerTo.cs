using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sinthetik.Helpers;
public class TeleportPlayerTo : MonoBehaviour
{
    public PlayerTeleport teleport;

    public UnityEvent teleportCompleted = new UnityEvent();

    public void Teleport()
    {
        teleport.TeleportPlayerToTransform(gameObject.transform);
        teleportCompleted?.Invoke();
    }
    
}
