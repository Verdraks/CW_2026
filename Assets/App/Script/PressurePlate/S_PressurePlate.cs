using UnityEngine;

public class S_PressurePlate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string playerTag;
    //[Header("References")]
    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSO_Bool rso_PressurePlateActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            rso_PressurePlateActivate.Set(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            rso_PressurePlateActivate.Set(false);
        }
    }
}