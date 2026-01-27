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
            Debug.Log("Pressure Plate Activated");
            rso_PressurePlateActivate.Set(true);
        }
    }
}