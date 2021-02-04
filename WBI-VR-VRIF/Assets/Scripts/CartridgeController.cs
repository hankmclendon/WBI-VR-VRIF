using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CartridgeController : MonoBehaviour
{
    public GameObjectEvent cartridgeGrabbed = new GameObjectEvent();

    public void QueueCartridge()
    {
        cartridgeGrabbed?.Invoke(gameObject);
    }
}

// Class declaration
[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }
