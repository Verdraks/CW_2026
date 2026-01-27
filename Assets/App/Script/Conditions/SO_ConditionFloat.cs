using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SO_ConditionFloat", menuName = "Conditions/ConditionFloat")]
public class SO_ConditionFloat : ScriptableObject, ICondition
{
    [Header("Settings")]
    [SerializeField] private float m_ReferenceValue;
    [SerializeField] private float m_Tolerance = 0f;
    [SerializeField] private E_ComparisonOperator m_Comparison;

    [Header("Reference")]
    [SerializeField] private RSO_Float m_ObservedValue;
    
    public bool Evaluate()
    {
        if (!m_ObservedValue) return false;

        float currentValue = m_ObservedValue.Get();
        
        return m_Comparison switch
        {
            E_ComparisonOperator.Greater => currentValue > (m_ReferenceValue + m_Tolerance),
            E_ComparisonOperator.GreaterOrEqual => currentValue >= (m_ReferenceValue - m_Tolerance),
            E_ComparisonOperator.Less => currentValue < (m_ReferenceValue - m_Tolerance),
            E_ComparisonOperator.LessOrEqual => currentValue <= (m_ReferenceValue + m_Tolerance),
            _ => false
        };
    }
}

