using System.Collections;
using UnityEngine;

public class S_Timer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timerDuration = 30f;
    //[Header("References")]
    [Header("Input")]
    [SerializeField] private RSE_StartTimer startTimer;
    [SerializeField] private RSE_StopTimer stopTimer;
    [Header("Output")]
    [SerializeField] private RSE_BasicEvent timerFinished;
    [SerializeField] private RSO_Float timerTick;
    private Coroutine timerCoroutine = null;
    private float timer = 0f;

    private void OnEnable()
    {
        startTimer.Action += StartTimer;
        stopTimer.Action += StopTimer;
    }
    private void OnDisable()
    {
        startTimer.Action -= StartTimer;
        stopTimer.Action -= StopTimer;
    }
    private void StartTimer()
    {
        timer = timerDuration;
        timerTick.Set(timer);
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerCoroutine = StartCoroutine(TimerCoroutine(timerDuration));
    }
    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
    private IEnumerator TimerCoroutine(float duration)
    {
        while(timer != 0)
        {
            timer -= Time.deltaTime;
            timerTick.Set(timer);
            yield return null;
        }
        timerFinished.Call();
    }
}