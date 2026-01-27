using System;
using UnityEngine;


public class S_ClickableObjectTracking : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private RSO_Float m_ClickCount;
    
    private int m_ClickCountInternal;

    private void Awake()
    {
        m_ClickCount.Set(0);
    }

    public void Interact()
    {
        m_ClickCountInternal++;
        m_ClickCount.Set(m_ClickCountInternal);
    }

}

