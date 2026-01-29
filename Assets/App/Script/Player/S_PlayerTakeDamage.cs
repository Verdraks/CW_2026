using UnityEngine;

public class S_PlayerTakeDamage : MonoBehaviour
{
    //[Header("Settings")]
    //[Header("References")]
    [Header("Input")]
    [SerializeField] private RSE_BasicEvent onTakeDamage;
    [Header("Output")]
    [SerializeField] private RSO_Int rso_playerHealth;

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
    }
}