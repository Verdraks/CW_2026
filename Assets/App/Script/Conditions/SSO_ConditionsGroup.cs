using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_ConditionsGroup", menuName = "SSO/Conditions/ConditionsGroup")]
public class SSO_ConditionsGroup : ScriptableObject
{
    [Header("Float Conditions")]
    [SerializeField] private List<MVsToolkit.Dev.InterfaceReference<ICondition>> m_Conditions = new();
    
   public bool EvaluateAll()
    {
        foreach (var condition in m_Conditions)
        {
            if (!condition.Value.Evaluate()) return false;
        }
        return true;
    }

   public bool EvaluateAny()
    {
        foreach (var condition in m_Conditions)
        {
            if (condition.Value.Evaluate()) return true;
        }
        return false;
    }
   
}

