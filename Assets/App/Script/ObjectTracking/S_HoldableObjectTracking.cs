using UnityEngine;

public class S_HoldableObjectTracking : MonoBehaviour, IInteractable
{
    
    [Header("References")]
    [SerializeField] private RSO_Float m_PressDuration;
    [SerializeField] private RSO_Bool m_IsManipulated;
    
    private bool m_IsHolding;
    private float m_HoldStartTime;
    private float m_TotalHoldDuration;

    private void Awake()
    {
        m_IsHolding = false;
        m_HoldStartTime = 0f;
        m_TotalHoldDuration = 0f;
        m_PressDuration.Set(0f);
        m_IsManipulated.Set(false);
    }

    public void UpdateTracking()
    {
        float currentDuration = m_IsHolding 
            ? m_TotalHoldDuration + (Time.time - m_HoldStartTime) 
            : m_TotalHoldDuration;
        m_PressDuration.Set(currentDuration);
    }


    private void Update()
    {
        if (m_IsHolding)
        {
            UpdateTracking();
        }
    }

    public void Interact()
    {
        m_IsHolding = true;
        m_HoldStartTime = Time.time;
        m_IsManipulated .Set(true);
    }

    public void StopInteract()
    {
        if (m_IsHolding)
        {
            m_TotalHoldDuration += Time.time - m_HoldStartTime;
            m_IsHolding = false;
            UpdateTracking();
        }
        m_IsManipulated.Set(false);
    }
}

