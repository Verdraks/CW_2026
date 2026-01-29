using System.Collections.Generic;
using UnityEngine;

public class S_SimonManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float resetDelay = 0f;
    [SerializeField] private List<SO_ButtonCombination> combinations;
    //[Header("References")]
    [Header("Input")]
    [SerializeField] private RSE_OnButtonInteract rse_OnButtonInteract;
    //[Header("Output")]
    private List<string> currentCombination = new();
    private bool isLocked;
    private void OnEnable()
    {
        rse_OnButtonInteract.Action += OnButtonInteracted;
    }
    private void OnDisable()
    {
        rse_OnButtonInteract.Action -= OnButtonInteracted;
    }

    private void OnButtonInteracted(string buttonID)
    {
        if (isLocked) return;

        currentCombination.Add(buttonID);
        Evaluate();
    }
    private void Evaluate()
    {
        foreach (var combo in combinations)
        {
            if (Matches(combo.ButtonCombination))
            {
                combo.OnCombinationMatched.Call();

                if (resetDelay >= 0f)
                    Invoke(nameof(ResetSequence), resetDelay);

                return;
            }
        }
    }

    private bool Matches(List<string> target)
    {
        if (currentCombination.Count != target.Count)
            return false;

        for (int i = 0; i < target.Count; i++)
        {
            if (currentCombination[i] != target[i])
                return false;
        }

        return true;
    }

    public void ResetSequence()
    {
        currentCombination.Clear();
    }
}