using UnityEngine;
using UnityEngine.Events;

public class S_PlayerHealth : MonoBehaviour
{
    //[Header("Settings")]
    //[Header("References")]
    [Header("Input")]
    [SerializeField] private RSE_BasicEvent onTakeDamage;
    [Header("Output")]
    [SerializeField] private RSO_Int rso_playerHealth;
    [SerializeField] private UnityEvent onHealthChanged;

    private int health;
    
    private void OnEnable()
    {
        onTakeDamage.Action += TakeDamage;
    }
    private void OnDisable()
    {
        onTakeDamage.Action -= TakeDamage;
    }
    private void TakeDamage()
    {
        health++;
        rso_playerHealth.Set(health);
        onHealthChanged.Invoke();
    }
}