using UnityEngine;

public class S_BombTimerTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    //[Header("References")]
    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSE_OnStartBombTimer onStartBombTimer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            onStartBombTimer.Call();
        }
    }
}