using System.Collections.Generic;
using UnityEngine;

public class S_ConditionListener : MonoBehaviour
{
    [Header("Float Conditions")]
    [SerializeField] private List<ICondition> m_Conditions = new();

    [Header("Outputs")]
    [SerializeField] private RSE_BasicEvent m_ConditionsValidated;
    
    [SerializeField] private float m_RefreshInterval = 0.1f;
    
    [SerializeField] private bool m_DisableAfterValidation = true;

    private float m_LastPollingTime = 0f;
    private bool m_HasTriggered = false;


    private void Update()
    {
        if (m_HasTriggered) return;
        if (!(Time.time - m_LastPollingTime >= m_RefreshInterval)) return;
        m_LastPollingTime = Time.time;
        Evaluate();
    }

    private void Evaluate()
    {
        if (m_HasTriggered && m_DisableAfterValidation)
        {
            return;
        }
        
        if (EvaluateAll())
        {
            TriggerValidation();
        }
    }


    private bool EvaluateAll()
    {
        foreach (var condition in m_Conditions)
        {
            if (!condition.Evaluate()) return false;
        }
        return true;
    }

    private bool EvaluateAny()
    {
        foreach (var condition in m_Conditions)
        {
            if (condition.Evaluate()) return true;
        }
        return false;
    }

    private void TriggerValidation()
    {
        m_ConditionsValidated.Call();

        if (m_DisableAfterValidation)
        {
            m_HasTriggered = true;
        }
    }

    public void ResetLister()
    {
        m_HasTriggered = false;
    }
}

