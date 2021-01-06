using BNG;
using UnityEngine;

public class TeleportPlayerTo : MonoBehaviour
{
    public PlayerTeleport teleport;
    public Transform destination;

    public void TeleportPlayerLocation()
    {
        teleport.TeleportPlayerToTransform(destination);
    }
}
