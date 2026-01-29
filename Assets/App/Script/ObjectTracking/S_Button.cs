using System;
using UnityEngine;


public class S_Button : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private GameObject m_OutlineInteractable;
    [SerializeField] private RSO_Float m_ClickCount;
    [SerializeField] private RSO_ClickHistory m_ClickHistory;

    [Header("Hold References")]
    [SerializeField] private RSO_Float m_PressDuration;
    [SerializeField] private RSO_Bool m_IsManipulated;
    
    private int m_ClickCountInternal;

    private bool m_IsHolding;
    private float m_HoldStartTime;
    private float m_TotalHoldDuration;

    private void Awake()
    {
        m_ClickCount?.Set(0);
        
        m_ClickHistory?.Setup();

        m_PressDuration?.Set(0f);
        m_IsManipulated?.Set(false);

        m_IsHolding = false;
        m_HoldStartTime = 0f;
        m_TotalHoldDuration = 0f;
        
        
        m_OutlineInteractable?.SetActive(false);
    }

    private void Update()
    {
        if (m_IsHolding)
        {
            UpdateTracking();
        }
    }

    private void UpdateTracking()
    {
        float currentDuration = m_IsHolding 
            ? m_TotalHoldDuration + (Time.time - m_HoldStartTime) 
            : m_TotalHoldDuration;
        if (m_PressDuration) m_PressDuration?.Set(currentDuration);
    }

    public void Hover()
    {
        m_OutlineInteractable?.SetActive(true);
    }

    public void Unhover()
    {
        m_OutlineInteractable?.SetActive(false);
    }

    public void Interact()
    {
        m_IsHolding = true;
        m_HoldStartTime = Time.time;
        m_IsManipulated?.Set(true);

        m_ClickCountInternal++;
        m_ClickCount?.Set(m_ClickCountInternal);
        m_ClickHistory?.Get()?.Add(new ClickEvent(Time.time, false));
    }

    public void StopInteract()
    {
        if (m_IsHolding)
        {
            m_TotalHoldDuration += Time.time - m_HoldStartTime;
            m_IsHolding = false;
            UpdateTracking();
        }
        m_IsManipulated?.Set(false);

        m_ClickHistory?.Get()?.Add(new ClickEvent(Time.time,true));
    }
}
