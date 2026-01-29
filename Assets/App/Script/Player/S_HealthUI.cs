using UnityEngine;
using UnityEngine.UI;

public class S_HealthUI : MonoBehaviour
{
    //[Header("Settings")]
    [Header("References")]
    [SerializeField] private GameObject groupParent;
    [SerializeField] private GameObject crossImage;
    [Header("Input")]
    [SerializeField] private RSO_Int onSetHealth;
    //[Header("Output")]
    private void OnEnable()
    {
        onSetHealth.OnChanged += AddCross;
    }
    private void OnDisable()
    {
        onSetHealth.OnChanged -= AddCross;
    }
    private void AddCross(int value)
    {
        Instantiate(crossImage, groupParent.transform);
    }
}