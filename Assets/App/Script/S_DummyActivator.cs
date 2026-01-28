using System;
using UnityEngine;

public class S_DummyActivator : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private RSE_BasicEvent m_ActivateEvent;
    
    private void OnEnable() => m_ActivateEvent.Action += OnActivate;

    private void OnActivate() => Debug.Log("OnActivate");
    
    private void OnDisable() => m_ActivateEvent.Action -= OnActivate;
}
