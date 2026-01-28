using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_ConditionClickPattern", menuName = "SSO/Conditions/ConditionClickPattern")]
public class SSO_ConditionClickPattern : ICondition
{
    [Header("Settings")]
    [Tooltip("Liste des éléments du pattern à détecter")]
    [SerializeField] private List<ClickPatternElement> m_Pattern = new();
    
    [Tooltip("Temps maximum pour compléter tout le pattern (en secondes)")]
    [SerializeField] private float m_MaxPatternDuration = 5f;

    [Header("Reference")]
    [SerializeField] private RSO_ClickHistory m_ClickHistory;

    [Header("Debug")]
    [SerializeField] private bool m_Debug = false;

    public override bool Evaluate()
    {
        if (!m_ClickHistory) return false;

        List<ClickEvent> events = m_ClickHistory.Get().ToList();
        if (events == null || events.Count == 0) return false;
        if (m_Pattern == null || m_Pattern.Count == 0) return false;

        List<InteractionUnit> units = BuildInteractionUnits(events);
        if (units.Count == 0) return false;

        if (m_Debug)
        {
            Debug.Log($"[ClickPattern] Events: {events.Count}, Units: {units.Count}");
            for (int i = 0; i < units.Count; i++)
            {
                Debug.Log($"[ClickPattern] Unit {i}: press={units[i].PressTime:F3}, release={units[i].ReleaseTime:F3}, dur={units[i].Duration:F3}");
            }
        }

        bool result = MatchPattern(units);
        if (m_Debug)
        {
            Debug.Log($"[ClickPattern] Match result: {result}");
        }
        return result;
    }

    private struct InteractionUnit
    {
        public float PressTime;
        public float ReleaseTime;
        public float Duration => ReleaseTime - PressTime;
    }

    private List<InteractionUnit> BuildInteractionUnits(List<ClickEvent> events)
    {
        var units = new List<InteractionUnit>();
        var pendingPresses = new List<float>();

        foreach (var ev in events)
        {
            if (!ev.IsRelease)
            {
                // press -> push
                pendingPresses.Add(ev.Time);
            }
            else
            {
                // release -> pair with oldest pending press if any (FIFO)
                if (pendingPresses.Count > 0)
                {
                    float pressTime = pendingPresses[0];
                    pendingPresses.RemoveAt(0);
                    units.Add(new InteractionUnit { PressTime = pressTime, ReleaseTime = ev.Time});
                }
                else
                {
                    // release without press -> ignore
                }
            }
        }

        // Any remaining pending presses are ongoing holds; create virtual releases at Time.time
        if (pendingPresses.Count > 0)
        {
            float now = Time.time;
            for (int i = 0; i < pendingPresses.Count; i++)
            {
                units.Add(new InteractionUnit { PressTime = pendingPresses[i], ReleaseTime = now});
            }
        }

        // Ensure units are in chronological order by press time (important for matching)
        units.Sort((a, b) => a.PressTime.CompareTo(b.PressTime));

        return units;
    }

    private bool MatchPattern(List<InteractionUnit> units)
    {
        int totalUnitsRequired = 0;
        foreach (var element in m_Pattern)
        {
            totalUnitsRequired += element.Type == E_ClickElementType.Clicks ? element.ClickCount : 1;
        }

        if (units.Count < totalUnitsRequired)
        {
            if (m_Debug) Debug.Log($"[ClickPattern] Not enough units: have {units.Count}, need {totalUnitsRequired}");
            return false;
        }

        // Try matching starting from the latest possible window and walk backwards to find any matching subsequence
        for (int startIndex = units.Count - totalUnitsRequired; startIndex >= 0; startIndex--)
        {
            if (m_Debug) Debug.Log($"[ClickPattern] Trying window starting at {startIndex}");

            // allow windows that contain virtual units to bypass the global max-duration check (so ongoing holds can match)

            float wStart = units[startIndex].PressTime;
            float wEnd = units[startIndex + totalUnitsRequired - 1].ReleaseTime;
            if (wEnd - wStart > m_MaxPatternDuration)
            {
                if (m_Debug) Debug.Log($"[ClickPattern] Skipping window {startIndex} because duration too long: {wEnd - wStart:F3}s");
                continue;
            }

            if (TryMatchAt(units, startIndex, totalUnitsRequired))
            {
                if (m_Debug) Debug.Log($"[ClickPattern] Match found at startIndex {startIndex}");
                return true;
            }
        }

        if (m_Debug) Debug.Log("[ClickPattern] No matching window found");
        return false;
    }

    private bool TryMatchAt(List<InteractionUnit> units, int startIndex, int totalUnitsRequired)
    {
        int unitIndex = startIndex;

        float patternStartTime = units[startIndex].PressTime;
        float patternEndTime = units[startIndex + totalUnitsRequired - 1].ReleaseTime;
        if (patternEndTime - patternStartTime > m_MaxPatternDuration)
        {
            if (m_Debug) Debug.Log($"[ClickPattern] Window at {startIndex} exceeds max duration: {patternEndTime - patternStartTime:F3}s");
            return false;
        }

        for (int patternIndex = 0; patternIndex < m_Pattern.Count; patternIndex++)
        {
            var element = m_Pattern[patternIndex];

            if (element.Type == E_ClickElementType.Clicks)
            {
                for (int i = 0; i < element.ClickCount; i++)
                {
                    if (unitIndex >= startIndex + totalUnitsRequired) return false;

                    if (i > 0)
                    {
                        float timeSinceLastPress = units[unitIndex].PressTime - units[unitIndex - 1].PressTime;
                        if (timeSinceLastPress > element.MaxTimeBetweenClicks) return false;
                        if (element.MinTimeBetweenClicks > 0f && timeSinceLastPress < element.MinTimeBetweenClicks) return false;
                    }

                    unitIndex++;
                }

                if (patternIndex < m_Pattern.Count - 1 && element.MinPauseAfter > 0f)
                {
                    if (unitIndex >= startIndex + totalUnitsRequired) return false;
                    float pauseDuration = units[unitIndex].PressTime - units[unitIndex - 1].ReleaseTime;
                    if (pauseDuration < element.MinPauseAfter) return false;
                }
            }
            else // Hold
            {
                if (unitIndex >= startIndex + totalUnitsRequired) return false;
                var unit = units[unitIndex];
                float holdDuration = unit.Duration;

                // If an expected duration is set (>0), use tolerance window check (clamp lower to 0)
                if (element.HoldExpectedDuration > 0f)
                {
                    float lower = Mathf.Max(0f, element.HoldExpectedDuration - element.HoldDurationTolerance);
                    float upper = element.HoldExpectedDuration + element.HoldDurationTolerance;
                    if (holdDuration < lower || holdDuration > upper)
                    {
                        if (m_Debug) Debug.Log($"[ClickPattern] Hold duration {holdDuration:F3}s not in expected range [{lower:F3}, {upper:F3}]");
                        return false;
                    }
                }
                else
                {
                    if (holdDuration < element.MinHoldDuration)
                    {
                        if (m_Debug) Debug.Log($"[ClickPattern] Hold duration too short: {holdDuration:F3}s < {element.MinHoldDuration:F3}s");
                        return false;
                    }
                    if (element.MaxHoldDuration > 0f && holdDuration > element.MaxHoldDuration)
                    {
                        if (m_Debug) Debug.Log($"[ClickPattern] Hold duration too long: {holdDuration:F3}s > {element.MaxHoldDuration:F3}s");
                        return false;
                    }
                }

                if (patternIndex < m_Pattern.Count - 1 && element.MinPauseAfter > 0f)
                {
                    if (unitIndex + 1 >= startIndex + totalUnitsRequired) return false;
                    float pauseDuration = units[unitIndex + 1].PressTime - unit.ReleaseTime;
                    if (pauseDuration < element.MinPauseAfter)
                    {
                        if (m_Debug) Debug.Log($"[ClickPattern] MinPauseAfter after hold failed: {pauseDuration:F3}s < {element.MinPauseAfter:F3}s");
                        return false;
                    }
                }

                unitIndex++;
            }
        }

        return true;
    }
}
