using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ButtonCombination", menuName = "SSO/Button/ButtonCombination")]
public class SO_ButtonCombination : ScriptableObject
{
    public List<string> ButtonCombination;
    public RSE_BasicEvent OnCombinationMatched;
}