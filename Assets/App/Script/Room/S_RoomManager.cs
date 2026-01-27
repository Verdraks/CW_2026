using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MVsToolkit.Wrappers;

public class S_RoomManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<RoomEvent> m_RoomEvents = new();

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        foreach (RoomEvent roomEvent in m_RoomEvents)
        {
            roomEvent.Rse.Action += roomEvent.UnityEvent.Invoke;
        }
    }

    private void UnsubscribeFromEvents()
    {
        foreach (RoomEvent roomEvent in m_RoomEvents)
        {
            roomEvent.Rse.Action -= roomEvent.UnityEvent.Invoke;
        }
    }
    
    [System.Serializable]
    private struct RoomEvent
    {
        public RuntimeScriptableEvent Rse;
        public UnityEvent UnityEvent;
    }
}

