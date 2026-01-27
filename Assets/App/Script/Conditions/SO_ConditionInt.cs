using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SO_ConditionInt", menuName = "Conditions/ConditionInt")]
public class SO_ConditionInt : ScriptableObject, ICondition
{
    [Header(" Settings")]
    [SerializeField] private int m_ReferenceValue;
    [SerializeField] private E_ComparisonOperator m_Comparison;
    
    [Header("Reference")]
    [SerializeField] private RSO_Int m_ObservedValue;

    public bool Evaluate()
    {
        if (!m_ObservedValue) return false;
        
        int currentValue = m_ObservedValue.Get();
        
        return m_Comparison switch
        {
            E_ComparisonOperator.Greater => currentValue > m_ReferenceValue,
            E_ComparisonOperator.GreaterOrEqual => currentValue >= m_ReferenceValue,
            E_ComparisonOperator.Less => currentValue < m_ReferenceValue,
            E_ComparisonOperator.LessOrEqual => currentValue <= m_ReferenceValue,
            _ => false
        };
    }
}

