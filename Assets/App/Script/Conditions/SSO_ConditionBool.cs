using UnityEngine;

[CreateAssetMenu(fileName = "SSO_ConditionBool", menuName = "SSO/Conditions/ConditionBool")]
public class SSO_ConditionBool : ScriptableObject, ICondition
{
    [Header("Settings")]
    [SerializeField] private bool m_ExpectedValue = true;
    
    [Header("Reference")]
    [SerializeField] private RSO_Bool m_ObservedValue;

    public bool Evaluate()
    {
        return m_ObservedValue?.Get() == m_ExpectedValue;
    }
}

