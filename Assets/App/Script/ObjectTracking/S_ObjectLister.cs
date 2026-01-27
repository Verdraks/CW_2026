using UnityEngine;

public class S_ObjectLister : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SSO_ConditionsGroup m_Conditions;

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
        
        if (m_Conditions.EvaluateAll())
        {
            TriggerValidation();
        }
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

