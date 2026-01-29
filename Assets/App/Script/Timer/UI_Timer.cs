using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshPro m_TimerText;

    [Header("Inputs")]
    [SerializeField] private RSO_Float m_TimerValue;

    private void OnEnable()
    {
        m_TimerValue.OnChanged += UpdateText;
    }
    private void OnDisable()
    {
        m_TimerValue.OnChanged -= UpdateText;
    }
    private void UpdateText(float timer)
    {
        int totalTime = Mathf.FloorToInt(timer);
        int minutes = totalTime / 60;
        int seconds = totalTime % 60;
        m_TimerText.text = $"{minutes:00}:{seconds:00}";
    }
}